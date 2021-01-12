using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using MachinePortal.Models;

namespace MachinePortal.Controllers
{
    public class AssetsController : Controller
    {
        private readonly AssetService _assetService;
        IHostingEnvironment _appEnvironment;

        public AssetsController(IHostingEnvironment enviroment, AssetService assetService)
        {
            _assetService = assetService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _assetService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asset asset, IFormFile file)
        {
            long filesSize = file.Length;
            var filePath = Path.GetTempFileName();

            if (file == null || file.Length == 0)
            {
                ViewData["Error"] = "Error: No file selected";
                return View(ViewData);
            }

            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            fileName += file.FileName.Substring(file.FileName.LastIndexOf("."), (file.FileName.Length - file.FileName.LastIndexOf(".")));
            string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Assets\\Images\\" + fileName;
            asset.ImageURL = @"/resources/Assets/Images/" + fileName;

            using (var stream = new FileStream(destinationPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            await _assetService.InsertAsync(asset);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _assetService.FindByIDAsync(ID.Value);
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
            var asset = await _assetService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + asset.ImageURL))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + asset.ImageURL);
                }
            }
            catch
            {

            }
            await _assetService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

    }
}