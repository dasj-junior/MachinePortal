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

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    public class PermissionsModel : PageModel
    {
        public readonly IdentityContext _context;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public List<Permission> permissions { get; set; }

        [BindProperty]
        public Permission permission { get; set; }

        public PermissionsModel(IdentityContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public void OnGet()
        {
            permissions = _context.Permission.ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Permission.AddAsync(permission);
            await _context.SaveChangesAsync();
            return RedirectToPage("Permissions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _context.Permission.AddAsync(permission);
            await _context.SaveChangesAsync();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostRemove()
        {
            _context.Permission.Remove(permission);
            _context.SaveChanges();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostEdit()
        {
            _context.Permission.Update(permission);
            _context.SaveChanges();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostTeste()
        {
            await _emailSender.SendEmailAsync("dasj.junior@gmail.com", "Confirm your email",$"Please confirm your account by");
            return RedirectToPage("/");
        }

    }
}