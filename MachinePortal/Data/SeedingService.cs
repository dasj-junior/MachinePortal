using MachinePortal.Areas.Identity.Data;
using MachinePortal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class SeedingService
    {
        private readonly MachinePortalContext _MPcontext;
        private readonly IdentityContext _Icontext;
        private readonly UserManager<MachinePortalUser> _userManager;

        public SeedingService(MachinePortalContext MPcontext, IdentityContext Icontext, UserManager<MachinePortalUser> userManager)
        {
            _MPcontext = MPcontext;
            _Icontext = Icontext;
            _userManager = userManager;
        }

        public void Seed()
        {
            if (_Icontext.Users.Any() ||
                _Icontext.Department.Any() ||
                _Icontext.Permission.Any() ||
                _Icontext.UserPermission.Any())
            {
                return;
            }

            //Department
            Department department = new Department { ID = 1, Name = "Administration" };

            //Permissions
            Permission perm01 = new Permission { ID = 1, PermissionName = "Administration" };
            Permission perm02 = new Permission { ID = 2, PermissionName = "PageDepartments" };
            Permission perm03 = new Permission { ID = 3, PermissionName = "PageManageUsers" };
            Permission perm04 = new Permission { ID = 4, PermissionName = "PagePermissions" };
            Permission perm05 = new Permission { ID = 5, PermissionName = "PageMachines" };
            Permission perm06 = new Permission { ID = 6, PermissionName = "PageDevices" };
            Permission perm07 = new Permission { ID = 7, PermissionName = "PageResponsibles" };
            Permission perm08 = new Permission { ID = 8, PermissionName = "PageHierarchies" };
            Permission perm09 = new Permission { ID = 9, PermissionName = "PageCategories" };

            //User
            MachinePortalUser user = new MachinePortalUser
            {
                Department = department,
                FirstName = "System",
                LastName = "Administrator",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "xxx@xxx.com.br",
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            _Icontext.Department.Add(department);
            _Icontext.Permission.AddRange(perm01, perm02, perm03, perm04,perm05, perm06, perm07, perm08,perm09);

            var result = _userManager.CreateAsync(user, "VitescoAdmin1*").GetAwaiter().GetResult();
            user = _Icontext.Users.FirstOrDefault();

            //User Permissions
            UserPermission up01 = new UserPermission { MachinePortalUser = user, Permission = perm01, PermissionID = perm01.ID, UserID = user.Id };
            UserPermission up02 = new UserPermission { MachinePortalUser = user, Permission = perm02, PermissionID = perm02.ID, UserID = user.Id };
            UserPermission up03 = new UserPermission { MachinePortalUser = user, Permission = perm03, PermissionID = perm03.ID, UserID = user.Id };
            UserPermission up04 = new UserPermission { MachinePortalUser = user, Permission = perm04, PermissionID = perm04.ID, UserID = user.Id };
            UserPermission up05 = new UserPermission { MachinePortalUser = user, Permission = perm05, PermissionID = perm05.ID, UserID = user.Id };
            UserPermission up06 = new UserPermission { MachinePortalUser = user, Permission = perm06, PermissionID = perm06.ID, UserID = user.Id };
            UserPermission up07 = new UserPermission { MachinePortalUser = user, Permission = perm07, PermissionID = perm07.ID, UserID = user.Id };
            UserPermission up08 = new UserPermission { MachinePortalUser = user, Permission = perm08, PermissionID = perm08.ID, UserID = user.Id };
            UserPermission up09 = new UserPermission { MachinePortalUser = user, Permission = perm09, PermissionID = perm09.ID, UserID = user.Id };

            _Icontext.UserPermission.AddRange(up01, up02, up03, up04, up05, up06, up07, up08, up09);

            //Save
            _Icontext.SaveChangesAsync();
        }

    }
}
