using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MachinePortal.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    public class DepartmentsModel : PageModel
    {
        public readonly IdentityContext _context;
        private readonly UserManager<MachinePortalUser> _userManager;
        public List<string> permissions = new List<string>();

        [BindProperty]
        public List<Department> departments { get; set; }

        [BindProperty]
        public Department department { get; set; }

        public DepartmentsModel(IdentityContext context, UserManager<MachinePortalUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            //Verify Permissions
            var user = await _userManager.GetUserAsync(User);
            if (user != null) { permissions = ((from obj in _context.UserPermission select obj).Include(p => p.Permission).Where(x => x.UserID == user.Id).ToList()).Select(p => p.Permission.PermissionName).ToList(); };
            if (!permissions.Contains("PageDepartments")) 
            { 
               return RedirectToPage(@"./../AccessDenied"); 
            };
            departments = _context.Department.ToList();
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync();
            return RedirectToPage("Departments");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCreateAsync()
        {
            await _context.Department.AddAsync(department);
            await _context.SaveChangesAsync();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostRemove()
        {
            _context.Department.Remove(department);
            _context.SaveChanges();
            return RedirectToPage("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostEdit()
        {
            _context.Department.Update(department);
            _context.SaveChanges();
            return RedirectToPage("/");
        }
    }
}