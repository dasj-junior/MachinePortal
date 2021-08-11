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
    public class PermissionsModel : PageModel
    {
        public readonly IdentityContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<MachinePortalUser> _userManager;
        public List<string> permissions = new List<string>();

        [BindProperty]
        public List<Permission> Permissions { get; set; }

        [BindProperty]
        public Permission Permission { get; set; }

        public PermissionsModel(IdentityContext context, IEmailSender emailSender, UserManager<MachinePortalUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            //Verify Permissions
            var user = await _userManager.GetUserAsync(User);
            if (user != null) { permissions = ((from obj in _context.UserPermission select obj).Include(p => p.Permission).Where(x => x.UserID == user.Id).ToList()).Select(p => p.Permission.PermissionName).ToList(); };
            if (!permissions.Contains("PagePermissions"))
            {
                return RedirectToPage(@"./../AccessDenied");
            };

            Permissions = _context.Permission.ToList();
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Permission.AddAsync(Permission);
            await _context.SaveChangesAsync();
            return RedirectToPage("Permissions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _context.Permission.AddAsync(Permission);
            await _context.SaveChangesAsync();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostRemove()
        {
            _context.Permission.Remove(Permission);
            _context.SaveChanges();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostEdit()
        {
            _context.Permission.Update(Permission);
            _context.SaveChanges();
            return RedirectToPage("/");
        }
    }
}