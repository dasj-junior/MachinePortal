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

namespace MachinePortal.Areas.Identity.Pages.Account
{
    public class LocalUser
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }

    public class ManageUsersModel : PageModel
    {
        private readonly IdentityContext _context;

        public ManageUsersModel(IdentityContext context)
        {
            _context = context;
        }

        public List<LocalUser> Users = new List<LocalUser>();
       
        public void OnGet()
        {
            List<MachinePortalUser> AppUsers = _context.Users.ToList();
            foreach (MachinePortalUser U in AppUsers)
            {
                LocalUser X = new LocalUser
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