using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MachinePortal.Services;
using System.Security.Claims;

namespace MachinePortal.Controllers
{
    public class HierarchiesController : BaseController<HierarchiesController>
    {
        private readonly AreaService _AreaService;
        private readonly SectorService _SectorService;
        private readonly LineService _LineService;
        private readonly MachineService _MachineService;
        IHostingEnvironment _appEnvironment;

        public HierarchiesController(IHostingEnvironment enviroment, AreaService areaService, SectorService sectorService, LineService lineService, MachineService machineService, PermissionsService permissionsService, IdentityContext identityContext)
        {
            _identityContext = identityContext;
            _AreaService = areaService;
            _SectorService = sectorService;
            _LineService = lineService;
            _MachineService = machineService;
            _PermissionsService = permissionsService;
            _appEnvironment = enviroment;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();
            var list = await _AreaService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Sectors(int? ID)
        {
            Permissions();
            var list = await _SectorService.FindByAreaIDAsync(ID.Value);
            return View(list);
        }

        public async Task<IActionResult> Lines(int? ID)
        {
            Permissions();
            var list = await _LineService.FindBySectorIDAsync(ID.Value);
            return View(list);
        }

        public async Task<IActionResult> Machines(int? ID)
        {
            Permissions();
            var list = await _MachineService.FindByLine(ID.Value);
            return View(list);
        }
    }
}