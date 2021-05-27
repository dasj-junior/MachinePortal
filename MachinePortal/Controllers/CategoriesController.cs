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
    public class CategoriesController : BaseController<CategoriesController>
    {
        private readonly CategoryService _CategoryService;
        private readonly IHostingEnvironment _appEnvironment;

        public CategoriesController(IHostingEnvironment enviroment, CategoryService categoryService, IdentityContext identityContext,  PermissionsService permissionsService)
        {
            _CategoryService = categoryService;
            _identityContext = identityContext;
            _PermissionsService = permissionsService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var list = await _CategoryService.FindAllAsync();
            return View(list);
        }

        public IActionResult Create()
        {
            Permissions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category Category)
        {
            Permissions();
            await _CategoryService.InsertAsync(Category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _CategoryService.FindByIDAsync(ID.Value);
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
            var Category = await _CategoryService.FindByIDAsync(ID);
            await _CategoryService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _CategoryService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category Category)
        {
            Permissions();
            await _CategoryService.UpdateAsync(Category);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialEdit(string id)
        {
            int ID = int.Parse(id);
            Category category = await _CategoryService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Edit", category);
            return partial;
        }

        [HttpPost]
        public async Task<PartialViewResult> AddPartialDelete(string id)
        {
            int ID = int.Parse(id);
            Category category = await _CategoryService.FindByIDAsync(ID);
            PartialViewResult partial = PartialView("Delete", category);
            return partial;
        }
    }
}