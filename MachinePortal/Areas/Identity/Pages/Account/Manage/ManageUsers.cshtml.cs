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

        public ManageUsersModel(IdentityContext context)
        {
            _context = context;
        }

        public List<InputModel> Users = new List<InputModel>();
       
        public void OnGet()
        {
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
        }

    }
}