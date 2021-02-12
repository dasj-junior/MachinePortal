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

namespace MachinePortal.Controllers
{
    public class DevicesController : Controller
    {
        private readonly DeviceService _deviceService;
        private readonly DocumentService _documentService;
        IHostingEnvironment _appEnvironment;

        public DevicesController(IHostingEnvironment enviroment, DeviceService deviceService, DocumentService documentService)
        {
            _deviceService = deviceService;
            _documentService = documentService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _deviceService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Device device, IFormFile image, List<IFormFile> documents)
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

            await _deviceService.InsertAsync(device);

            foreach (IFormFile document in documents)
            {
                long filesSize = document.Length;
                var filePath = Path.GetTempFileName();

                if (document == null || document.Length == 0)
                {
                    ViewData["Error"] = "Error: No file selected";
                    return View(ViewData);
                }

                string fileName = document.FileName.Substring(0,document.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Devices\\Documents\\" + fileName;
                DeviceDocument doc = new DeviceDocument();
                doc.Name = fileName;
                doc.Path = @"/resources/Devices/Documents/";
                doc.Extension = document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                doc.Device = device;
                await _documentService.InsertAsync(doc);
                device.AddDocument(doc);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
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
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + device.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + device.ImagePath);
                }
                foreach (DeviceDocument document in device.Documents)
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + document.Path + document.Name);
                    //await _documentService.RemoveAsync(document);
                }
            }
            catch
            {

            }
            await _deviceService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? ID)
        {
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

        public async Task<IActionResult> Edit(int? ID)
        {
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
        public async Task<IActionResult> Edit(Device device, IFormFile image)
        {
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
            
            await _deviceService.UpdateAsync(device);

            return RedirectToAction(nameof(Index));
        }

        public FileResult Download(string filePath, string fileName, string extension)
        {
            IFileProvider provider = new PhysicalFileProvider(_appEnvironment.WebRootPath +  filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimeType = MimeTypes.GetMimeType(extension);
            return File(readStream, mimeType, fileName);
        }
        
    }
}