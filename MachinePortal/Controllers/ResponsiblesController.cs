﻿using System;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Responsible responsible, IFormFile image)
        {
            Permissions();
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Responsibles\\Images\\" + fileName;
                responsible.PhotoPath = @"/resources/Responsibles/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            responsible.FullName = responsible.FirstName + " " + responsible.LastName;

            await _responsibleService.InsertAsync(responsible);

            return RedirectToAction(nameof(Index));
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
            Permissions();
            var responsible = await _responsibleService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + responsible.PhotoPath);
                }
            }
            catch
            {

            }
            await _responsibleService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
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

        public async Task<IActionResult> Edit(int? ID)
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
        public async Task<IActionResult> Edit(Responsible responsible, IFormFile image)
        {
            Permissions();
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
                    ViewData["Error"] = "Error: No file selected";
                    return View(ViewData);
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

            responsible.FullName = responsible.FirstName + " " + responsible.LastName;

            await _responsibleService.UpdateAsync(responsible);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDetails(string id)
        {
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Details", responsible);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", responsible);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            int ID = int.Parse(id);
            Responsible responsible = await _responsibleService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", responsible);
            return partial;
        }
    }
}