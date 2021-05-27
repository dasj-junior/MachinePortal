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
    public class ManageDefaultPermissionsModel : PageModel
    {
        private readonly IdentityContext _context;
        public SelectList SlAvailablePermissions { get; set; }
        public SelectList SlCurrentPermissions { get; set; }
        public int[] SelectPermissions { get; set; }
        public int[] SelectedPermissions { get; set; }
        public string ReturnUrl { get; set; }

        public ManageDefaultPermissionsModel(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult OnGet()
        {
            //Read Depts
            List<Permission> AllPerms = _context.Permission.ToList();
            List<Permission> userPerms = ((from obj in _context.DefaultPermission select obj).Select(d => d.defaultPermission).ToList());
            AllPerms = AllPerms.Where(x => !userPerms.Contains(x)).ToList();
            SlCurrentPermissions = new SelectList(userPerms, "ID", "Name");
            SlAvailablePermissions = new SelectList(AllPerms, "ID", "Name");
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task OnPostAsync(int ID, [FromForm] int[] SelectedRDepartments, [FromForm] int[] SelectedWDepartments)
        {
            //Read Dept
            List<Permission> CurrentPermissions = ((from obj in _context.DefaultPermission select obj).Select(d => d.defaultPermission).ToList());
            List<Permission> SelPermissions = new List<Permission>();
            foreach (int p in SelectedPermissions)
            {
                SelPermissions.Add(_context.Permission.FirstOrDefault(x => x.ID == p));
            }
            List<Permission> AddPerms = SelPermissions.Where(x => !CurrentPermissions.Contains(x)).ToList();
            List<Permission> RemovePerms = CurrentPermissions.Where(x => !SelPermissions.Contains(x)).ToList();
            foreach (Permission dr in AddPerms)
            {
                DefaultPermission DP = new DefaultPermission { defaultPermission = dr };
                await _context.DefaultPermission.AddAsync(DP);
                await _context.SaveChangesAsync();
            }
            foreach (Permission dr in RemovePerms)
            {
                _context.DefaultPermission.Remove(_context.DefaultPermission.FirstOrDefault(x => x.defaultPermission == dr));
                await _context.SaveChangesAsync();
            }

            OnGet();
        }

    }
}
