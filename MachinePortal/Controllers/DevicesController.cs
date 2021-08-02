using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Controllers.AuxiliarClasses;
using MachinePortal.Models;
using MachinePortal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;
using MachinePortal.Areas.Identity.Data;
using Newtonsoft.Json;

namespace MachinePortal.Controllers
{
    public class DevicesController : BaseController<DevicesController>
    {
        private readonly DeviceService _deviceService;
        private readonly DocumentService _documentService;
        private readonly CategoryService _categoryService;
        private readonly IHostingEnvironment _appEnvironment;

        public DevicesController(IHostingEnvironment enviroment, DeviceService deviceService, 
            DocumentService documentService, PermissionsService permissionsService, 
            IdentityContext identityContext, CategoryService categoryService)
        {
            _identityContext = identityContext;
            _deviceService = deviceService;
            _documentService = documentService;
            _PermissionsService = permissionsService;
            _categoryService = categoryService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var list = await _deviceService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            Permissions();

            List<Category> categories = await _categoryService.FindAllAsync();
            ViewBag.ListCategories = categories;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device, IFormFile image)
        {
            //Generate list of documents from the update page
            List<IFormFile> documents = new List<IFormFile>();
            try
            {
                foreach (var file in Request.Form.Files)
                {
                    if (file.Name == "document") { documents.Add(file); }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Erro getting files from page, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }
            
            try
            {
                if (image != null)
                {
                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        ViewData["Error"] = "Error: No file selected";
                        return View(ViewData);
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Devices\\Images\\" + fileName;
                    device.ImagePath = @"/resources/Devices/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error saving device image, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _deviceService.InsertAsync(device);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error adding device, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                foreach (IFormFile document in documents)
                {
                    long filesSize = document.Length;
                    var filePath = Path.GetTempFileName();

                    if (document == null || document.Length == 0)
                    {
                        ViewData["Error"] = "Error: No file selected";
                        return View(ViewData);
                    }

                    string fileName = document.FileName.Substring(0, document.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                    fileName += document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Devices\\Documents\\" + fileName;
                    DeviceDocument doc = new DeviceDocument
                    {
                        Name = fileName,
                        Path = @"/resources/Devices/Documents/",
                        Extension = document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf("."))),
                        Device = device
                    };
                    await _documentService.InsertAsync(doc);
                    device.AddDocument(doc);

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error saving device documents, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Device inserted successfuly";
            TempData["notificationIcon"] = "bi-check-circle";
            TempData["notificationType"] = "success";
            return Content("success");
        }

        public async Task<IActionResult> Edit(int? ID)
        {
            Permissions();

            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _deviceService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Category> categories = await _categoryService.FindAllAsync();
            ViewBag.ListCategories = categories;

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Device device, IFormFile image)
        {
            List<DeviceDocument> RemoveDocumets = new List<DeviceDocument>();
            List<IFormFile> documents = new List<IFormFile>();

            try
            {
                //List of files to delete
                int[] DRemove = JsonConvert.DeserializeObject<int[]>(Request.Form["DRemove"]);
                foreach (int ID in DRemove)
                {
                    RemoveDocumets.Add(await _documentService.FindByIDAsync(ID));
                }

                //Generate list of documents to be added
                foreach (var file in Request.Form.Files)
                {
                    if (file.Name == "document") { documents.Add(file); }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Erro getting data from page, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }
            
            try
            {
                //Update photo
                if (image != null)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + device.ImagePath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + device.ImagePath);
                    }

                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        ViewData["Error"] = "Error: No file selected";
                        return View(ViewData);
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfffffff");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Devices\\Images\\" + fileName;
                    device.ImagePath = @"/resources/Devices/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Erro updating image, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                //Add documents
                foreach (IFormFile document in documents)
                {
                    long filesSize = document.Length;
                    var filePath = Path.GetTempFileName();

                    if (document == null || document.Length == 0)
                    {
                        ViewData["Error"] = "Error: No file selected";
                        return View(ViewData);
                    }

                    string fileName = document.FileName.Substring(0, document.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                    fileName += document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Devices\\Documents\\" + fileName;
                    DeviceDocument doc = new DeviceDocument
                    {
                        Name = fileName,
                        Path = @"/resources/Devices/Documents/",
                        Extension = document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf("."))),
                        Device = device,
                        DeviceID = device.ID
                    };
                    await _documentService.InsertAsync(doc);
                    device.AddDocument(doc);

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await document.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Erro adding documents, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                //Remove documents
                foreach (DeviceDocument document in RemoveDocumets)
                {
                    await _documentService.RemoveAsync(document);
                    device.RemoveDocument(document);
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + document.Path + document.Name))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + document.Path + document.Name);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error removing documents, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _deviceService.UpdateAsync(device);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating device, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Device updated successfuly";
            TempData["notificationIcon"] = "bi-check-circle";
            TempData["notificationType"] = "success";
            return Content("success");
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _deviceService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            var device = await _deviceService.FindByIDAsync(ID);
            if (device == null)
            {
                return Content(@"notify('', 'Erro: ID not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }
            
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + device.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + device.ImagePath);
                }
                foreach (DeviceDocument document in device.Documents)
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + document.Path + document.Name);
                    await _documentService.RemoveAsync(document);
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error deleting device documents, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _deviceService.RemoveAsync(ID);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error deleting device, description: " + e.Message + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Device removed successfuly";
            TempData["notificationIcon"] = "bi-check-circle";
            TempData["notificationType"] = "success";
            return Content("success");
        }
        
        public async Task<IActionResult> Details(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _deviceService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        
        public FileResult Download(string filePath, string fileName, string extension)
        {
            IFileProvider provider = new PhysicalFileProvider(_appEnvironment.WebRootPath +  filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimeType = MimeTypes.GetMimeType(extension);
            return File(readStream, mimeType, fileName);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            int ID = int.Parse(id);
            Device device = await _deviceService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", device);
            return partial;
        }
    }
}