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

        public AreasController(IHostingEnvironment enviroment, AreaService areaService, PasswordService passwordService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _AreaService = areaService;
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
        public async Task<IActionResult> Create(Area area, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', '" + "Error: File corrupted or invalid" + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Areas\\Images\\" + fileName;
                    area.ImagePath = @"/resources/Areas/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }
                }
            }
            catch(Exception e)
            {
                return Content(@"notify('', '" + "Error on saving image, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }
            
            try
            {
                await _AreaService.InsertAsync(area);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating database, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Area add successfuly";
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
            var obj = await _AreaService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Area area, IFormFile image)
        {
            try
            {
                if (image != null)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + area.ImagePath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + area.ImagePath);
                    }

                    long filesSize = image.Length;
                    var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', '" + "Error: File corrupted or invalid" + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
                    }

                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                    string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Areas\\Images\\" + fileName;
                    area.ImagePath = @"/resources/Areas/Images/" + fileName;

                    using (var stream = new FileStream(destinationPath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating image, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _AreaService.UpdateAsync(area);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating area on database, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Area updated successfuly";
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
            var Area = await _AreaService.FindByIDAsync(ID);
            if(Area == null)
            {
                return Content(@"notify('', 'Error: ID not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Area.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Area.ImagePath);
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error deleting image on server, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _AreaService.RemoveAsync(ID);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error deleting area on database, description: " + e.Message + @", 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Area deleted successfuly";
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
            var obj = await _AreaService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            int ID = int.Parse(id);
            Area area = await _AreaService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", area);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            int ID = int.Parse(id);
            Area area = await _AreaService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", area);
            return partial;
        }

    }
}