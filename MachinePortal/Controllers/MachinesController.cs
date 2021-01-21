using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using MachinePortal.Models.ViewModels;
using MachinePortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachinePortal.Controllers
{
    public class MachinesController : Controller
    {

        private readonly AssetService _assetService;
        private readonly ResponsibleService _responsibleService;
        private readonly DeviceService _deviceService;

        private readonly AreaService _areaService;
        private readonly SectorService _sectorService;
        private readonly LineService _lineService;
        
        public MachinesController(AssetService assetService, ResponsibleService responsibleService, DeviceService deviceService,
            AreaService areaService, SectorService sectorService, LineService lineService)
        {
            _assetService = assetService;
            _responsibleService = responsibleService;
            _deviceService = deviceService;

            _areaService = areaService;
            _sectorService = sectorService;
            _lineService = lineService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var assets = await _assetService.FindAllAsync();
            var responsibles = await _responsibleService.FindAllAsync();
            var devices = await _deviceService.FindAllAsync();

            List<Area> areas = await _areaService.FindAllAsync();
            areas.Add(new Area { ID = 0, Name = "Select" });
            ViewBag.ListAreas = areas;

            var viewModel = new MachineFormViewModel { Assets = assets, Responsibles = responsibles, Devices = devices};
            return View(viewModel);

        }

        public async Task<JsonResult> GetSector(int AreaID)
        {
            List<Sector> SectorList = new List<Sector>();
            SectorList = await _sectorService.FindByAreaIDAsync(AreaID);
            return Json(new SelectList(SectorList, "ID", "Name"));
        }

        public async Task<JsonResult> GetLine(int SectorID)
        {
            List<Line> LineList = new List<Line>();
            LineList = await _lineService.FindBySectorIDAsync(SectorID);
            return Json(new SelectList(LineList, "ID", "Name"));
        }

    }
}