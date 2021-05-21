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
    public class ManageDepartmentsModel : PageModel
    {
        private readonly IdentityContext _context;
        public SelectList SlAvailableRDepartments { get; set; }
        public SelectList SlCurrentRDepartments { get; set; }
        public SelectList SlAvailableWDepartments { get; set; }
        public SelectList SlCurrentWDepartments { get; set; }
        public int[] SelectRDepartments { get; set; }
        public int[] SelectedRDepartments { get; set; }
        public int[] SelectWDepartments { get; set; }
        public int[] SelectedWDepartments { get; set; }
        public Department dept = new Department();
        public string ReturnUrl { get; set; }

        public ManageDepartmentsModel(IdentityContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult OnGet(int ID)
        {
            dept = _context.Department.FirstOrDefault(obj => obj.ID == ID);
            if (dept == null)
            {
                return NotFound($"Unable to load department with ID'{dept.ID}'.");
            }
            else
            {
                //Read Depts
                List<Department> AllRDepts = _context.Department.ToList();
                List<Department> userRDepts = ((from obj in _context.DepartmentRead select obj).Where(x => x.Department.ID == ID).Select(d => d.ReadDepartment).ToList());
                AllRDepts = AllRDepts.Where(x => !userRDepts.Contains(x)).ToList();
                SlCurrentRDepartments = new SelectList(userRDepts, "ID", "Name");
                SlAvailableRDepartments = new SelectList(AllRDepts, "ID", "Name");

                //Write Depts
                List<Department> AllWDepts = _context.Department.ToList();
                List<Department> userWDepts = ((from obj in _context.DepartmentWrite select obj).Where(x => x.Department.ID == ID).Select(d => d.WriteDepartment).ToList());
                AllWDepts = AllWDepts.Where(x => !userWDepts.Contains(x)).ToList();
                SlCurrentWDepartments = new SelectList(userWDepts, "ID", "Name");
                SlAvailableWDepartments = new SelectList(AllWDepts, "ID", "Name");
            }
            return Page();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task OnPostAsync(int ID, [FromForm] int[] SelectedRDepartments, [FromForm] int[] SelectedWDepartments)
        {
            Department dpt = _context.Department.FirstOrDefault(x => x.ID == ID);

            //Read Dept
            List<Department> CurrentRDepartments = ((from obj in _context.DepartmentRead select obj).Where(x => x.Department.ID == ID).Select(d => d.ReadDepartment).ToList());
            List<Department> SelRDepartments = new List<Department>();
            foreach (int p in SelectedRDepartments)
            {
                SelRDepartments.Add(_context.Department.FirstOrDefault(x => x.ID == p));
            }
            List<Department> AddRDetps = SelRDepartments.Where(x => !CurrentRDepartments.Contains(x)).ToList();
            List<Department> RemoveRDepts = CurrentRDepartments.Where(x => !SelRDepartments.Contains(x)).ToList();
            foreach (Department dr in AddRDetps)
            {
                DepartmentRead DR = new DepartmentRead { Department = dpt, ReadDepartment = dr };
                await _context.DepartmentRead.AddAsync(DR);
                await _context.SaveChangesAsync();
            }
            foreach (Department dr in RemoveRDepts)
            {
                _context.DepartmentRead.Remove(_context.DepartmentRead.FirstOrDefault(x => x.Department.Name == dpt.Name && x.ReadDepartment.Name == dr.Name));
                await _context.SaveChangesAsync();
            }

            //Write Dept
            List<Department> CurrentWDepartments = ((from obj in _context.DepartmentWrite select obj).Where(x => x.Department.ID == ID).Select(d => d.WriteDepartment).ToList());
            List<Department> SelWDepartments = new List<Department>();
            foreach (int p in SelectedWDepartments)
            {
                SelWDepartments.Add(_context.Department.FirstOrDefault(x => x.ID == p));
            }
            List<Department> AddWDetps = SelWDepartments.Where(x => !CurrentWDepartments.Contains(x)).ToList();
            List<Department> RemoveWDepts = CurrentWDepartments.Where(x => !SelWDepartments.Contains(x)).ToList();
            foreach (Department dw in AddWDetps)
            {
                DepartmentWrite DW = new DepartmentWrite { Department = dpt, WriteDepartment = dw };
                await _context.DepartmentWrite.AddAsync(DW);
                await _context.SaveChangesAsync();
            }
            foreach (Department dw in RemoveWDepts)
            {
                _context.DepartmentWrite.Remove(_context.DepartmentWrite.FirstOrDefault(x => x.Department.Name == dpt.Name && x.WriteDepartment.Name == dw.Name));
                await _context.SaveChangesAsync();
            }

            OnGet(ID);
        }

    }
}
