using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Models;
using MachinePortal.Models.ViewModels;
using MachinePortal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MachinePortal.Controllers
{
    public class MachinesController : Controller
    {
        private readonly MachineService _machineService;
        IHostingEnvironment _appEnvironment;

        private readonly ResponsibleService _responsibleService;
        private readonly DeviceService _deviceService;

        private readonly AreaService _areaService;
        private readonly SectorService _sectorService;
        private readonly LineService _lineService;

        public MachinesController(MachineService machineService, IHostingEnvironment enviroment, ResponsibleService responsibleService, DeviceService deviceService,
            AreaService areaService, SectorService sectorService, LineService lineService)
        {
            _machineService = machineService;
            _appEnvironment = enviroment;

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
            var responsibles = await _responsibleService.FindAllAsync();
            var devices = await _deviceService.FindAllAsync();

            SelectList slResponsibles = new SelectList(responsibles, "ID", "FirstName");
            ViewBag.ListResponsibles = slResponsibles;

            SelectList slDevices = new SelectList(devices, "ID", "Name");
            ViewBag.ListDevices = slDevices;

            List<Area> areas = await _areaService.FindAllAsync();       
            ViewBag.ListAreas = areas;


            var viewModel = new MachineFormViewModel {  Responsibles = responsibles, Devices = devices };
            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Create(MachineFormViewModel machineFVM, IFormFile photo)
        {
            List<IFormFile> files = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                files.Add(file);
            }
            machineFVM.Machine.Area = await _areaService.FindByIDAsync(machineFVM.Machine.AreaID);
            machineFVM.Machine.Sector = await _sectorService.FindByIDAsync(machineFVM.Machine.SectorID);
            machineFVM.Machine.Line = await _lineService.FindByIDAsync(machineFVM.Machine.LineID);
            
            if (photo != null)
            {
                long filesSize = photo.Length;
                var filePath = Path.GetTempFileName();

                if (photo == null || photo.Length == 0)
                {
                    ViewData["Error"] = "Error: No file selected";
                    return View(ViewData);
                }

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                fileName += photo.FileName.Substring(photo.FileName.LastIndexOf("."), (photo.FileName.Length - photo.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Photo\\" + fileName;
                machineFVM.Machine.ImagePath = @"/resources/Machines/Photo/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
            }

            await _machineService.InsertAsync(machineFVM.Machine);

            if(machineFVM.SelectedResponsibles != null)
            {
                foreach (int id in machineFVM.SelectedResponsibles)
                {
                    Responsible responsible = await _responsibleService.FindByIDAsync(id);
                    MachineResponsible machineResponsible = new MachineResponsible { Machine = machineFVM.Machine, MachineID = machineFVM.Machine.ID, Responsible = responsible, ResponsibleID = responsible.ID };
                    await _machineService.InsertMachineResponsibleAsync(machineResponsible);
                    machineFVM.Machine.MachineResponsibles.Add(machineResponsible);
                }
            }

            if (machineFVM.SelectedDevices != null)
            {
                foreach (int id in machineFVM.SelectedDevices)
                {
                    Device device = await _deviceService.FindByIDAsync(id);
                    MachineDevice machineDevice = new MachineDevice { Machine = machineFVM.Machine, MachineID = machineFVM.Machine.ID, Device = device, DeviceID = device.ID };
                    await _machineService.InsertMachineDeviceAsync(machineDevice);
                    machineFVM.Machine.MachineDevices.Add(machineDevice);
                }
            }     

            return RedirectToAction(nameof(Index));
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

        [HttpPost]
        public IActionResult UploadFiles()
        {
            List<IFormFile> files = new List<IFormFile>();
            foreach(var file in Request.Form.Files)
            {
                files.Add(file);
            }
            return View();
        }

    }
}