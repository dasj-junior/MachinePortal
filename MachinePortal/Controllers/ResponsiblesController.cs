using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using MachinePortal.Areas.Identity.Data;
using System.Web;

namespace MachinePortal.Controllers
{
    public class ResponsiblesController : BaseController<ResponsiblesController>
    {
        private readonly ResponsibleService _responsibleService;
        private readonly IHostingEnvironment _appEnvironment;

        public ResponsiblesController(IHostingEnvironment enviroment, ResponsibleService responsibleService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _PermissionsService = permissionsService;
            _responsibleService = responsibleService;
            _appEnvironment = enviroment;
        }


        public async Task<IActionResult> Index()
        {
            Permissions();
            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            var list = await _responsibleService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            Permissions();
            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Responsible responsible, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', 'Error: file corrupted or not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Responsibles\\Images\\" + fileName;
                    responsible.PhotoPath = @"/resources/Responsibles/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error saving image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                responsible.DepartmentID = responsible.Department.ID;
                responsible.Department = null;
                responsible.FullName = responsible.FirstName + " " + responsible.LastName;
                await _responsibleService.InsertAsync(responsible);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error adding responsible, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Responsible created successfuly";
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
            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            var obj = await _responsibleService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Responsible responsible, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath);
                    }

                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', 'Error: No file selected', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Responsibles\\Images\\" + fileName;
                    responsible.PhotoPath = @"/resources/Responsibles/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }
            
            try
            {
                responsible.FullName = responsible.FirstName + " " + responsible.LastName;
                responsible.DepartmentID = responsible.Department.ID;
                responsible.Department = null;
                await _responsibleService.UpdateAsync(responsible);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating responsible, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Responsible updated successfuly";
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
            var obj = await _responsibleService.FindByIDAsync(ID.Value);
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
            var responsible = await _responsibleService.FindByIDAsync(ID);
            if (responsible == null)
            {
                return Content(@"notify('', 'ID not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath);
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error removing image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _responsibleService.RemoveAsync(ID);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error removing responsible, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Responsible removed successfuly";
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
            var obj = await _responsibleService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
     
        //Partials
        [HttpGet]
        public async Task<PartialViewResult> AddPartialDetails(string id)
        {
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Details", responsible);
            return partial;
        }

        [HttpGet]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", responsible);
            return partial;
        }

        [HttpGet]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", responsible);
            return partial;
        }
    }
}