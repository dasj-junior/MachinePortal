using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using MachinePortal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MachinePortal.Controllers
{
    public class PermissionsController : Controller
    {
        private readonly PermissionService _PermissionService;
        IHostingEnvironment _appEnvironment;

        public PermissionsController(IHostingEnvironment enviroment, PermissionService permissionService)
        {
            _PermissionService = permissionService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            var listSector = await _PermissionService.FindAllAsync();
            return View(listSector);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Permission permission)
        {
            await _PermissionService.InsertAsync(permission);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null) return NotFound();
            var obj = await _PermissionService.FindByIDAsync(ID.Value);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            var Sector = await _PermissionService.FindByIDAsync(ID);
            await _PermissionService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? ID)
        {
            if (ID == null) return NotFound();
            var obj = await _PermissionService.FindByIDAsync(ID.Value);
            if (obj == null) return NotFound();
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? ID)
        {
            if (ID == null) return NotFound();
            var obj = await _PermissionService.FindByIDAsync(ID.Value);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Permission permission)
        {        
            await _PermissionService.UpdateAsync(permission);
            return RedirectToAction(nameof(Index));
        }
    }
}