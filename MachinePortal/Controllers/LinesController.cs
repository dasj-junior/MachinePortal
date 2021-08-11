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
using System.Security.Claims;
using MachinePortal.Areas.Identity.Data;
using System.Web;

namespace MachinePortal.Controllers
{
    public class LinesController : BaseController<LinesController>
    {
        private readonly LineService _LineService;
        private readonly SectorService _SectorService;
        private readonly IHostingEnvironment _appEnvironment;

        public LinesController(IHostingEnvironment enviroment, LineService LineService, SectorService SectorService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _SectorService = SectorService;
            _LineService = LineService;
            _PermissionsService = permissionsService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var list = await _LineService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create(int? sectorID)
        {
            Permissions();
            var viewModel = new LineFormViewModel
            {
                Line = new Line()
            };
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

            try
            {
                if (image != null)
                {
                    //long filesSize = image.Length;
                    //var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', 'Error: file corrupted or not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
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
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error saving image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _LineService.InsertAsync(Line);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error adding line, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Line created successfuly";
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
            try
            {
                if (image != null)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Line.ImagePath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Line.ImagePath);
                    }

                    //long filesSize = image.Length;
                    //var filePath = Path.GetTempFileName();

                    if (image == null || image.Length == 0)
                    {
                        return Content(@"notify('', 'Error: No file selected', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
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
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _LineService.UpdateAsync(Line);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error updating line, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Line updated successfuly";
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
            if(Line == null)
            {
                return Content(@"notify('', 'ID not found', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + Line.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + Line.ImagePath);
                }
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error removing image, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            try
            {
                await _LineService.RemoveAsync(ID);
            }
            catch (Exception e)
            {
                return Content(@"notify('', '" + "Error removing line, description: " + HttpUtility.JavaScriptStringEncode(e.Message) + @"', 'top', 'right', 'bi-x-circle', 'error', 'fadeInRight', 'fadeInRight')", "application/javascript");
            }

            TempData["notificationMessage"] = "Line removed successfuly";
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
            var obj = await _LineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            Line line;
            int ID = int.Parse(id);
            line = await _LineService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", line);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            Line line;
            int ID = int.Parse(id);
            line = await _LineService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", line);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDetails(string id)
        {
            Line line;
            int ID = int.Parse(id);
            line = await _LineService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Details", line);
            return partial;
        }
    }
}