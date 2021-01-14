using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using MachinePortal.Models.ViewModels;

namespace MachinePortal.Controllers
{
    public class LinesController : Controller
    {
        private readonly LineService _LineService;
        private readonly SectorService _SectorService;
        IHostingEnvironment _appEnvironment;

        public LinesController(IHostingEnvironment enviroment, LineService LineService, SectorService SectorService)
        {
            _SectorService = SectorService;
            _LineService = LineService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _LineService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create(int? sectorID)
        {
            var viewModel = new LineFormViewModel();
            viewModel.Line = new Line();
            if (sectorID != null)
            {
                var sector = await _SectorService.FindByIDAsync(sectorID.Value);
                viewModel.Line.Sector = sector;
                viewModel.Line.SectorID = sector.ID;
            }   
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Line Line, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new LineFormViewModel { Line = Line};
                return View(viewModel);
            }

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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Lines\\Images\\" + fileName;
                Line.ImagePath = @"/resources/Lines/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _LineService.InsertAsync(Line);

            return RedirectToAction(@"Details/" + Line.SectorID, "Sectors");
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _LineService.FindByIDAsync(ID.Value);
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
            var Line = await _LineService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Line.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Line.ImagePath);
                }
            }
            catch
            {

            }
            await _LineService.RemoveAsync(ID);
            return RedirectToAction(@"Details/" + Line.SectorID, "Sectors");
        }

        public async Task<IActionResult> Details(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _LineService.FindByIDAsync(ID.Value);
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
            var obj = await _LineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Line Line, IFormFile image)
        {
            if (image != null)
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Line.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Line.ImagePath);
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Lines\\Images\\" + fileName;
                Line.ImagePath = @"/resources/Lines/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _LineService.UpdateAsync(Line);

            return RedirectToAction(@"Details/" + Line.SectorID, "Sectors");
        }
    }
}