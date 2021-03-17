using System;
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

namespace MachinePortal.Controllers
{

    public class HomeController : Controller
    {
        private readonly PermissionsService _PermissionsService;

        public HomeController(PermissionsService permissionsService)
        {
            _PermissionsService = permissionsService;
        }

        private void Permissions()
        {
            string userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                ViewData["Permissions"] = _PermissionsService.GetUserPermissions(userID);
            }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
