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
            Department department01 = new Department { ID = 1, Name = "Administration" };
            Department department02 = new Department { ID = 2, Name = "DefaultUser" };

            //Permissions
            Permission perm01 = new Permission { ID = 1, PermissionName = "Administration" };
            
            Permission perm02 = new Permission { ID = 2, PermissionName = "PageDepartments" };
            Permission perm03 = new Permission { ID = 3, PermissionName = "DepartmentsCreate" };
            Permission perm04 = new Permission { ID = 4, PermissionName = "DepartmentsEdit" };
            Permission perm05 = new Permission { ID = 5, PermissionName = "DepartmentsDelete" };

            Permission perm06 = new Permission { ID = 6, PermissionName = "PageManagePermissions" };

            Permission perm07 = new Permission { ID = 7, PermissionName = "PageManageUsers" };
            Permission perm08 = new Permission { ID = 8, PermissionName = "ManageUsersEdit" };

            Permission perm09 = new Permission { ID = 9, PermissionName = "PagePermissions" };
            Permission perm10 = new Permission { ID = 10, PermissionName = "PermissionsCreate" };
            Permission perm11 = new Permission { ID = 11, PermissionName = "PermissionsEdit" };
            Permission perm12 = new Permission { ID = 12, PermissionName = "PermissionsDelete" };

            Permission perm13 = new Permission { ID = 13, PermissionName = "PageHierarchies" };
            Permission perm14 = new Permission { ID = 14, PermissionName = "HierarchiesCreate" };
            Permission perm15 = new Permission { ID = 15, PermissionName = "HierarchiesEdit" };
            Permission perm16 = new Permission { ID = 16, PermissionName = "HierarchiesDelete" };

            Permission perm17 = new Permission { ID = 17, PermissionName = "PageCategories" };
            Permission perm18 = new Permission { ID = 18, PermissionName = "CategoriesCreate" };
            Permission perm19 = new Permission { ID = 19, PermissionName = "CategoriesEdit" };
            Permission perm20 = new Permission { ID = 20, PermissionName = "CategoriesDelete" };

            Permission perm21 = new Permission { ID = 21, PermissionName = "PageDevices" };
            Permission perm22 = new Permission { ID = 22, PermissionName = "DevicesCreate" };
            Permission perm23 = new Permission { ID = 23, PermissionName = "DevicesEdit" };
            Permission perm24 = new Permission { ID = 24, PermissionName = "DevicesDelete" };

            Permission perm25 = new Permission { ID = 25, PermissionName = "PageMachines" };
            Permission perm26 = new Permission { ID = 26, PermissionName = "MachinesCreate" };
            Permission perm27 = new Permission { ID = 27, PermissionName = "MachinesEdit" };
            Permission perm28 = new Permission { ID = 28, PermissionName = "MachinesDelete" };
            Permission perm29 = new Permission { ID = 29, PermissionName = "MachinesAddComments" };
            Permission perm30 = new Permission { ID = 30, PermissionName = "MachinesRemoveOwnComments" };
            Permission perm31 = new Permission { ID = 31, PermissionName = "MachinesRemoveOtherComments" };
            Permission perm32 = new Permission { ID = 32, PermissionName = "MachinesAddPasswords" };
            Permission perm33 = new Permission { ID = 33, PermissionName = "MachinesViewOwnDepartmentPassword" };
            Permission perm34 = new Permission { ID = 34, PermissionName = "MachinesViewOtherDepartmentPassword" };
            Permission perm35 = new Permission { ID = 35, PermissionName = "MachinesEditOwnDepartmentPassword" };
            Permission perm36 = new Permission { ID = 36, PermissionName = "MachinesEditOtherDepartmentPassword" };
            Permission perm37 = new Permission { ID = 37, PermissionName = "MachinesDeleteOwnDepartmentPassword" };
            Permission perm38 = new Permission { ID = 38, PermissionName = "MachinesDeleteOtherDepartmentPassword" };

            Permission perm39 = new Permission { ID = 39, PermissionName = "PageResponsibles" };
            Permission perm40 = new Permission { ID = 40, PermissionName = "ResponsiblesCreate" };
            Permission perm41 = new Permission { ID = 41, PermissionName = "ResponsiblesEdit" };
            Permission perm42 = new Permission { ID = 42, PermissionName = "ResponsiblesDelete" };

            Permission perm43 = new Permission { ID = 43, PermissionName = "ResponsiblesCreate" };
            Permission perm44 = new Permission { ID = 44, PermissionName = "ResponsiblesEdit" };
            Permission perm45 = new Permission { ID = 45, PermissionName = "ResponsiblesDelete" };

            //User
            MachinePortalUser user01 = new MachinePortalUser
            {
                Department = department01,
                FirstName = "System",
                LastName = "Administrator",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "xxx@xxx.com.br",
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            //User
            MachinePortalUser user02 = new MachinePortalUser
            {
                Department = department02,
                FirstName = "Default",
                LastName = "User",
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "xxx@xxx.com.br",
                EmailConfirmed = true,
                TwoFactorEnabled = false
            };

            _Icontext.Department.Add(department01);
            _Icontext.Department.Add(department02);
            _Icontext.Permission.AddRange(perm01, perm02, perm03, perm04, perm05, perm06, perm07, perm08, perm09, perm10,
                                          perm11, perm12, perm13, perm14, perm15, perm16, perm17, perm18, perm19, perm20,
                                          perm21, perm22, perm23, perm24, perm25, perm26, perm27, perm28, perm29, perm30,
                                          perm31, perm32, perm33, perm34, perm35, perm36, perm37, perm38, perm39, perm40,
                                          perm41, perm42, perm43, perm44, perm45);

            var result = _userManager.CreateAsync(user01, "VitescoAdmin1*").GetAwaiter().GetResult();
            user01 = _Icontext.Users.FirstOrDefault();

            //User Permissions User 01
            UserPermission up01 = new UserPermission { MachinePortalUser = user01, Permission = perm01, PermissionID = perm01.ID, UserID = user01.Id };
            UserPermission up02 = new UserPermission { MachinePortalUser = user01, Permission = perm02, PermissionID = perm02.ID, UserID = user01.Id };
            UserPermission up03 = new UserPermission { MachinePortalUser = user01, Permission = perm03, PermissionID = perm03.ID, UserID = user01.Id };
            UserPermission up04 = new UserPermission { MachinePortalUser = user01, Permission = perm04, PermissionID = perm04.ID, UserID = user01.Id };
            UserPermission up05 = new UserPermission { MachinePortalUser = user01, Permission = perm05, PermissionID = perm05.ID, UserID = user01.Id };
            UserPermission up06 = new UserPermission { MachinePortalUser = user01, Permission = perm06, PermissionID = perm06.ID, UserID = user01.Id };
            UserPermission up07 = new UserPermission { MachinePortalUser = user01, Permission = perm07, PermissionID = perm07.ID, UserID = user01.Id };
            UserPermission up08 = new UserPermission { MachinePortalUser = user01, Permission = perm08, PermissionID = perm08.ID, UserID = user01.Id };
            UserPermission up09 = new UserPermission { MachinePortalUser = user01, Permission = perm09, PermissionID = perm09.ID, UserID = user01.Id };
            UserPermission up10 = new UserPermission { MachinePortalUser = user01, Permission = perm10, PermissionID = perm10.ID, UserID = user01.Id };
            UserPermission up11 = new UserPermission { MachinePortalUser = user01, Permission = perm11, PermissionID = perm11.ID, UserID = user01.Id };
            UserPermission up12 = new UserPermission { MachinePortalUser = user01, Permission = perm12, PermissionID = perm12.ID, UserID = user01.Id };
            UserPermission up13 = new UserPermission { MachinePortalUser = user01, Permission = perm13, PermissionID = perm13.ID, UserID = user01.Id };
            UserPermission up14 = new UserPermission { MachinePortalUser = user01, Permission = perm14, PermissionID = perm14.ID, UserID = user01.Id };
            UserPermission up15 = new UserPermission { MachinePortalUser = user01, Permission = perm15, PermissionID = perm15.ID, UserID = user01.Id };
            UserPermission up16 = new UserPermission { MachinePortalUser = user01, Permission = perm16, PermissionID = perm16.ID, UserID = user01.Id };
            UserPermission up17 = new UserPermission { MachinePortalUser = user01, Permission = perm17, PermissionID = perm17.ID, UserID = user01.Id };
            UserPermission up18 = new UserPermission { MachinePortalUser = user01, Permission = perm18, PermissionID = perm18.ID, UserID = user01.Id };
            UserPermission up19 = new UserPermission { MachinePortalUser = user01, Permission = perm19, PermissionID = perm19.ID, UserID = user01.Id };
            UserPermission up20 = new UserPermission { MachinePortalUser = user01, Permission = perm20, PermissionID = perm20.ID, UserID = user01.Id };
            UserPermission up21 = new UserPermission { MachinePortalUser = user01, Permission = perm21, PermissionID = perm21.ID, UserID = user01.Id };
            UserPermission up22 = new UserPermission { MachinePortalUser = user01, Permission = perm22, PermissionID = perm22.ID, UserID = user01.Id };
            UserPermission up23 = new UserPermission { MachinePortalUser = user01, Permission = perm23, PermissionID = perm23.ID, UserID = user01.Id };
            UserPermission up24 = new UserPermission { MachinePortalUser = user01, Permission = perm24, PermissionID = perm24.ID, UserID = user01.Id };
            UserPermission up25 = new UserPermission { MachinePortalUser = user01, Permission = perm25, PermissionID = perm25.ID, UserID = user01.Id };
            UserPermission up26 = new UserPermission { MachinePortalUser = user01, Permission = perm26, PermissionID = perm26.ID, UserID = user01.Id };
            UserPermission up27 = new UserPermission { MachinePortalUser = user01, Permission = perm27, PermissionID = perm27.ID, UserID = user01.Id };
            UserPermission up28 = new UserPermission { MachinePortalUser = user01, Permission = perm28, PermissionID = perm28.ID, UserID = user01.Id };
            UserPermission up29 = new UserPermission { MachinePortalUser = user01, Permission = perm29, PermissionID = perm29.ID, UserID = user01.Id };
            UserPermission up30 = new UserPermission { MachinePortalUser = user01, Permission = perm30, PermissionID = perm30.ID, UserID = user01.Id };
            UserPermission up31 = new UserPermission { MachinePortalUser = user01, Permission = perm31, PermissionID = perm31.ID, UserID = user01.Id };
            UserPermission up32 = new UserPermission { MachinePortalUser = user01, Permission = perm32, PermissionID = perm32.ID, UserID = user01.Id };
            UserPermission up33 = new UserPermission { MachinePortalUser = user01, Permission = perm33, PermissionID = perm33.ID, UserID = user01.Id };
            UserPermission up34 = new UserPermission { MachinePortalUser = user01, Permission = perm34, PermissionID = perm34.ID, UserID = user01.Id };
            UserPermission up35 = new UserPermission { MachinePortalUser = user01, Permission = perm35, PermissionID = perm35.ID, UserID = user01.Id };
            UserPermission up36 = new UserPermission { MachinePortalUser = user01, Permission = perm36, PermissionID = perm36.ID, UserID = user01.Id };
            UserPermission up37 = new UserPermission { MachinePortalUser = user01, Permission = perm37, PermissionID = perm37.ID, UserID = user01.Id };
            UserPermission up38 = new UserPermission { MachinePortalUser = user01, Permission = perm38, PermissionID = perm38.ID, UserID = user01.Id };
            UserPermission up39 = new UserPermission { MachinePortalUser = user01, Permission = perm39, PermissionID = perm39.ID, UserID = user01.Id };
            UserPermission up40 = new UserPermission { MachinePortalUser = user01, Permission = perm40, PermissionID = perm40.ID, UserID = user01.Id };
            UserPermission up41 = new UserPermission { MachinePortalUser = user01, Permission = perm41, PermissionID = perm41.ID, UserID = user01.Id };
            UserPermission up42 = new UserPermission { MachinePortalUser = user01, Permission = perm42, PermissionID = perm42.ID, UserID = user01.Id };
            UserPermission up43 = new UserPermission { MachinePortalUser = user01, Permission = perm43, PermissionID = perm43.ID, UserID = user01.Id };
            UserPermission up44 = new UserPermission { MachinePortalUser = user01, Permission = perm44, PermissionID = perm44.ID, UserID = user01.Id };
            UserPermission up45 = new UserPermission { MachinePortalUser = user01, Permission = perm45, PermissionID = perm45.ID, UserID = user01.Id };

            UserPermission up46 = new UserPermission { MachinePortalUser = user02, Permission = perm43, PermissionID = perm43.ID, UserID = user02.Id };
            UserPermission up47 = new UserPermission { MachinePortalUser = user02, Permission = perm44, PermissionID = perm44.ID, UserID = user02.Id };
            UserPermission up48 = new UserPermission { MachinePortalUser = user02, Permission = perm45, PermissionID = perm45.ID, UserID = user02.Id };

            _Icontext.UserPermission.AddRange(up01, up02, up03, up04, up05, up06, up07, up08, up09, up10,
                                              up11, up12, up13, up14, up15, up16, up17, up18, up19, up20,
                                              up21, up22, up23, up24, up25, up26, up27, up28, up29, up30,
                                              up31, up32, up33, up34, up35, up36, up37, up38, up39, up40,
                                              up41, up42, up43, up44, up45, up46, up47, up48);

            //Save
            _Icontext.SaveChangesAsync();
        }

    }
}
