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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    public class ManagePermissionsModel : PageModel
    {
        private readonly UserManager<MachinePortalUser> _userManager;
        private readonly IdentityContext _context;
        public SelectList slAvailablePermissions { get; set; }
        public SelectList slCurrentPermissions { get; set; }
        public int[] SelectPermissions { get; set; }
        public int[] SelectedPermissions { get; set; }
        public MachinePortalUser user = new MachinePortalUser();
        public string ReturnUrl { get; set; }

        public ManagePermissionsModel(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult OnGet(string ID)
        {
            user = _context.Users.FirstOrDefault(obj => obj.Id == ID);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            else
            {
                List<Permission> AllPerms = new List<Permission>();
                AllPerms = _context.Permission.ToList();
                List<Permission> userPermissions = new List<Permission>();
                userPermissions = ((from obj in _context.UserPermission select obj).Where(x => x.UserID == ID).ToList()).Select(p => p.Permission).ToList();
                AllPerms = AllPerms.Where(x => !userPermissions.Contains(x)).ToList();
                slCurrentPermissions = new SelectList(userPermissions, "ID", "PermissionName");
                slAvailablePermissions = new SelectList(AllPerms, "ID", "PermissionName");
            }
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task OnPostAsync(string ID,[FromForm] int[] SelectPermissions, [FromForm] int[] SelectedPermissions)
        {
            MachinePortalUser user = _context.Users.FirstOrDefault(u => u.Id == ID);
            List<Permission> CurrentPermissions = ((from obj in _context.UserPermission select obj).Include(p => p.Permission).Where(x => x.UserID == ID).ToList()).Select(p => p.Permission).ToList();

            List<Permission> SelPermissions = new List<Permission>();
            foreach (int p in SelectedPermissions)
            {
                SelPermissions.Add(_context.Permission.FirstOrDefault(x => x.ID == p));
            }
            List<Permission> AddPerms = new List<Permission>();
            AddPerms = SelPermissions.Where(x => !CurrentPermissions.Contains(x)).ToList();
            List<Permission> RemovePerms = new List<Permission>();
            RemovePerms = CurrentPermissions.Where(x => !SelPermissions.Contains(x)).ToList();
            foreach (Permission p in AddPerms)
            {
                UserPermission UP = new UserPermission { UserID = ID, Permission = p, PermissionID = p.ID, MachinePortalUser = user };
                await _context.UserPermission.AddAsync(UP);
                await _context.SaveChangesAsync();
            }

            foreach (Permission p in RemovePerms)
            {
                UserPermission UP = _context.UserPermission.FirstOrDefault(up => up.PermissionID == p.ID && up.UserID == ID);
                _context.UserPermission.Remove(UP);
                await _context.SaveChangesAsync();
            }

            OnGet(ID);
        }

    }
}
