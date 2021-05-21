using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MachinePortal.Areas.Identity.Data;

namespace MachinePortal.Areas.Identity.Pages.Account.Manage
{
    public class DepartmentsModel : PageModel
    {
        public readonly IdentityContext _context;

        [BindProperty]
        public List<Department> departments { get; set; }

        [BindProperty]
        public Department department { get; set; }

        public DepartmentsModel(IdentityContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            departments = _context.Department.ToList();
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