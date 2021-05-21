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
using System.Security.Claims;
using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Controllers
{
    public class AreasController : BaseController<AreasController>
    {
        private readonly AreaService _AreaService;
        private readonly IHostingEnvironment _appEnvironment;

        public AreasController(IHostingEnvironment enviroment, AreaService AreaService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _AreaService = AreaService;
            _PermissionsService = permissionsService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var list = await _AreaService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            Permissions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Area Area, IFormFile image)
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Areas\\Images\\" + fileName;
                Area.ImagePath = @"/resources/Areas/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _AreaService.InsertAsync(Area);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _AreaService.FindByIDAsync(ID.Value);
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
            var Area = await _AreaService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Area.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Area.ImagePath);
                }
            }
            catch
            {

            }
            await _AreaService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _AreaService.FindByIDAsync(ID.Value);
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
            var obj = await _AreaService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Area Area, IFormFile image)
        {
            Permissions();
            if (image != null)
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Area.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Area.ImagePath);
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
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Areas\\Images\\" + fileName;
                Area.ImagePath = @"/resources/Areas/Images/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            await _AreaService.UpdateAsync(Area);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            Area area;
            int ID = int.Parse(id);
            area = await _AreaService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", area);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            Area area;
            int ID = int.Parse(id);
            area = await _AreaService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", area);
            return partial;
        }
    }
}