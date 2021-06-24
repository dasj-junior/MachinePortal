using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MachinePortal.Areas.Identity.Data;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    
    public class ManageUsersModel : PageModel
    {
        public class InputModel
        {
            [Display(Name = "ID")]
            public string ID { get; set; }
            [Display(Name = "User Name")]
            public string UserName { get; set; }
            [Display(Name = "Full Name")]
            public string FullName { get; set; }
        }

        private readonly IdentityContext _context;
        private readonly UserManager<MachinePortalUser> _userManager;
        public List<string> permissions = new List<string>();

        public ManageUsersModel(IdentityContext context, UserManager<MachinePortalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<InputModel> Users = new List<InputModel>();
       
        public async Task<IActionResult> OnGet()
        {
            //Verify Permissions
            var user = await _userManager.GetUserAsync(User);
            if (user != null) { permissions = ((from obj in _context.UserPermission select obj).Include(p => p.Permission).Where(x => x.UserID == user.Id).ToList()).Select(p => p.Permission.PermissionName).ToList(); };
            if (!permissions.Contains("PageManageUsers"))
            {
                return RedirectToPage(@"./../AccessDenied");
            };

            List<MachinePortalUser> AppUsers = _context.Users.ToList();
            foreach (MachinePortalUser U in AppUsers)
            {
                InputModel X = new InputModel
                {
                    ID = U.Id,
                    UserName = U.UserName,
                    FullName = U.FirstName + " " + U.LastName
                };
                Users.Add(X);
            }
            return Page();
        }

    }
}