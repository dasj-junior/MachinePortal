using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using MachinePortal.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using System.Security.Claims;

namespace MachinePortal.Controllers
{
    public class SectorsController : BaseController<SectorsController>
    {
        private readonly AreaService _AreaService;
        private readonly SectorService _SectorService;
        private readonly LineService _LineService;
        IHostingEnvironment _appEnvironment;

        public SectorsController(IHostingEnvironment enviroment, SectorService sectorService, LineService lineService, AreaService areaService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _PermissionsService = permissionsService;
            _AreaService = areaService;
            _SectorService = sectorService;
            _LineService = lineService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var listSector = await _SectorService.FindAllAsync();
            return View(listSector);
        }

        public async Task<IActionResult> Create(int? areaID)
        {
            Permissions();
            var viewModel = new SectorFormViewModel();
            viewModel.Sector = new Sector();
            if (areaID != null)
            {
                var area = await _AreaService.FindByIDAsync(areaID.Value);
                viewModel.Sector.Area = area;
                viewModel.Sector.AreaID = area.ID;
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sector Sector, IFormFile image)
        {
            Permissions();
            if (!ModelState.IsValid)
            {
                var viewModel = new SectorFormViewModel { Sector = Sector };
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Sectors\\Images\\" + fileName;
                Sector.ImagePath = @"/resources/Sectors/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _SectorService.InsertAsync(Sector);

            return RedirectToAction(@"Details/" + Sector.AreaID, "Areas");
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _SectorService.FindByIDAsync(ID.Value);
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
            var Sector = await _SectorService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Sector.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Sector.ImagePath);
                }
            }
            catch
            {

            }
            await _SectorService.RemoveAsync(ID);
            return RedirectToAction(@"Details/" + Sector.AreaID, "Areas");
        }

        public async Task<IActionResult> Details(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _SectorService.FindByIDAsync(ID.Value);
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
            var obj = await _SectorService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sector Sector, IFormFile image)
        {
            Permissions();
            if (image != null)
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Sector.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Sector.ImagePath);
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Sectors\\Images\\" + fileName;
                Sector.ImagePath = @"/resources/Sectors/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _SectorService.UpdateAsync(Sector);

            return RedirectToAction(@"Details/" + Sector.AreaID, "Areas");
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            Sector sector = new Sector();
            int ID = int.Parse(id);
            sector = await _SectorService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", sector);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            Sector sector = new Sector();
            int ID = int.Parse(id);
            sector = await _SectorService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", sector);
            return partial;
        }
    }
}