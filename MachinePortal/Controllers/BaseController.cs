using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Controllers
{
        public abstract class BaseController<T> : Controller where T: BaseController<T>
    {
        public PermissionsService _PermissionsService;
        public IdentityContext _identityContext;

        public void Permissions()
        {
            string userID = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                MachinePortalUser user = _identityContext.Users.FirstOrDefault(x => x.Id == userID);
                ViewData["Permissions"] = _PermissionsService.GetUserPermissions(userID);
                ViewData["UserName"] = user.FirstName + " " + user.LastName;
                ViewData["UserID"] = user.Id;
            }
        }

    }
}