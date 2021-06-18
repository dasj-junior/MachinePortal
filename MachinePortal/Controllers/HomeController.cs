﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Models;
using System.Security.Claims;
using MachinePortal.Areas.Identity;
using MachinePortal.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MachinePortal.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Threading;

namespace MachinePortal.Controllers
{

    public class HomeController : BaseController<HomeController>
    {
        private readonly MachineService _MachineService;

        public HomeController(PermissionsService permissionsService, IdentityContext identityContext, MachineService machineService)
        {
            _MachineService = machineService;
            _PermissionsService = permissionsService;
            _identityContext = identityContext;
        }

        public IActionResult Index()
        {
            Permissions();
            return View();
        }

        public IActionResult About()
        {
            Permissions();
            return View();
        }

        public IActionResult Contact()
        {
            Permissions();
            return View();
        }

        public IActionResult Privacy()
        {
            Permissions();
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> ValidateMachineID(int MachineID)
        {
            Machine mac = await _MachineService.FindByIDAsync(MachineID);
            if (mac != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
                Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

    }
}
