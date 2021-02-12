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

namespace MachinePortal.Controllers
{
    public class ResponsiblesController : Controller
    {
        private readonly ResponsibleService _responsibleService;
        IHostingEnvironment _appEnvironment;

        public ResponsiblesController(IHostingEnvironment enviroment, ResponsibleService responsibleService)
        {
            _responsibleService = responsibleService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _responsibleService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Responsible responsible, IFormFile image)
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
    }
}