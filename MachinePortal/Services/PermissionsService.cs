using MachinePortal.Areas.Identity.Data;
using MachinePortal.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using MachinePortal.Services.Exceptions;

namespace MachinePortal.Services
{
    public class PermissionsService : Controller
    {
        private readonly IdentityContext _context;

        private List<string> permissions = new List<string>();

        public PermissionsService(IdentityContext context)
        {
            _context = context;
        }

        public List<string> GetUserPermissions(string userID)
        {
            try
            {
                if (userID != null)
                {
                    List<Permission> perms = ((from obj in _context.UserPermission select obj).Include(p => p.Permission).Where(x => x.UserID == userID).ToList()).Select(p => p.Permission).ToList();
                    foreach (Permission perm in perms)
                    {
                        permissions.Add(perm.PermissionName);
                    }
                }
                else
                {
                    throw new NotFoundException("ID not found");
                }
                return permissions;
            }
            catch (Exception e) { throw new Exception(e.Message); };  
        }

    }
}
