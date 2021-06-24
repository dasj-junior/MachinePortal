using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MachinePortal.Controllers.AuxiliarClasses;
using MachinePortal.Models;
using MachinePortal.Models.ViewModels;
using MachinePortal.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Security.Claims;
using MachinePortal.Areas.Identity.Data;
using SimpleImpersonation;

namespace MachinePortal.Controllers

{
    public class MachineFile
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string ID { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string Location { get; set; }
    }

    public class MachinesController : BaseController<MachinesController>
    {
        private readonly MachineService _machineService;
        private readonly IHostingEnvironment _appEnvironment;

        private readonly ResponsibleService _responsibleService;
        private readonly DeviceService _deviceService;
        private readonly PasswordService _passwordService;
        private readonly CategoryService _categoryService;

        private readonly AreaService _areaService;
        private readonly SectorService _sectorService;
        private readonly LineService _lineService;

        public MachinesController(IdentityContext identityContext, MachineService machineService,
            IHostingEnvironment enviroment, ResponsibleService responsibleService,
            DeviceService deviceService, AreaService areaService, SectorService sectorService,
            LineService lineService, PermissionsService permissionsService, PasswordService passwordService, 
            CategoryService categoryService)
        {
            _identityContext = identityContext;
            _machineService = machineService;
            _appEnvironment = enviroment;

            _PermissionsService = permissionsService;
            _passwordService = passwordService;
            _categoryService = categoryService;

            _responsibleService = responsibleService;
            _deviceService = deviceService;

            _areaService = areaService;
            _sectorService = sectorService;
            _lineService = lineService;
        }

        public async Task<IActionResult> Index()
        {
            Permissions();

            ViewData["notificationMessage"] = HttpContext.Session.GetString("notificationMessage");
            ViewData["notificationIcon"] = HttpContext.Session.GetString("notificationIcon");
            ViewData["notificationType"] = HttpContext.Session.GetString("notificationType");

            HttpContext.Session.SetString("notificationMessage", "");
            HttpContext.Session.SetString("notificationIcon", "");
            HttpContext.Session.SetString("notificationType", "");

            List<Machine> machines = await _machineService.FindAllAsync();
            return View(machines);
        }

        public async Task<IActionResult> Create()
        {
            Permissions();
            var responsibles = await _responsibleService.FindAllAsync();
            var devices = await _deviceService.FindAllAsync();

            SelectList slResponsibles = new SelectList(responsibles, "ID", "FullName");
            ViewBag.ListResponsibles = slResponsibles;

            SelectList slDevices = new SelectList(devices, "ID", "Name");
            ViewBag.ListDevices = slDevices;

            List<Area> areas = await _areaService.FindAllAsync();
            ViewBag.ListAreas = areas;

            List<Category> categories = await _categoryService.FindAllAsync();
            ViewBag.ListCategories = categories;

            var viewModel = new MachineFormViewModel { Responsibles = responsibles, Devices = devices };
            return View(viewModel);

        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<String> Create(MachineFormViewModel machineFVM, IFormFile photo)
        {
            Permissions();
            List<IFormFile> documents = new List<IFormFile>();
            List<IFormFile> images = new List<IFormFile>();
            List<IFormFile> videos = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                if (file.Name == "document") { documents.Add(file); }
                if (file.Name == "image") { images.Add(file); }
                if (file.Name == "video") { videos.Add(file); }
            }

            List<MachineFile> MFiles = JsonConvert.DeserializeObject<List<MachineFile>>(Request.Form["files"]);

            machineFVM.Machine.Area = await _areaService.FindByIDAsync(machineFVM.Machine.AreaID);
            machineFVM.Machine.Sector = await _sectorService.FindByIDAsync(machineFVM.Machine.SectorID);
            machineFVM.Machine.Line = await _lineService.FindByIDAsync(machineFVM.Machine.LineID);

            if (photo != null)
            {
                //long filesSize = photo.Length;
                //var filePath = Path.GetTempFileName();

                if (photo == null || photo.Length == 0)
                {
                    return ("Error: Invalid path - Machine Photo");
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

            foreach (IFormFile document in documents)
            {
                long filesSize = document.Length;
                var filePath = Path.GetTempFileName();

                if (document == null || document.Length == 0)
                {
                    return ("Error: Invalid path - Machine Document");
                }

                string fileName = document.FileName.Substring(0, document.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Documents\\" + fileName;
                MachineDocument doc = new MachineDocument();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == document.FileName && file.Type == "Document")
                    {
                        doc.Name = file.Name;
                        doc.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        doc.CategoryID = doc.Category.ID;
                        doc.Location = file.Location;
                    }
                }
                doc.FileName = fileName;
                doc.Path = @"/resources/Machines/Documents/";
                doc.Extension = document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                doc.Machine = machineFVM.Machine;
                doc.MachineID = machineFVM.Machine.ID;

                await _machineService.InsertMachineDocumentAsync(doc);

                machineFVM.Machine.AddDocument(doc);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }
            }

            foreach (IFormFile image in images)
            {
                long filesSize = image.Length;
                var filePath = Path.GetTempFileName();

                if (image == null || image.Length == 0)
                {
                    return ("Error: Invalid path - Machine Images");
                }

                string fileName = image.FileName.Substring(0, image.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Images\\" + fileName;
                MachineImage img = new MachineImage();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == image.FileName && file.Type == "Image")
                    {
                        img.Name = file.Name;
                        img.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        img.CategoryID = img.Category.ID;
                    }
                }
                img.FileName = fileName;
                img.Path = @"/resources/Machines/Images/";
                img.Extension = image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                img.Machine = machineFVM.Machine;
                img.MachineID = machineFVM.Machine.ID;
                await _machineService.InsertMachineImageAsync(img);
                machineFVM.Machine.AddImage(img);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            foreach (IFormFile video in videos)
            {
                long filesSize = video.Length;
                var filePath = Path.GetTempFileName();

                if (video == null || video.Length == 0)
                {
                    return ("Error: Invalid path - Machine Videos");
                }

                string fileName = video.FileName.Substring(0, video.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += video.FileName.Substring(video.FileName.LastIndexOf("."), (video.FileName.Length - video.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Videos\\" + fileName;
                MachineVideo vid = new MachineVideo();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == video.FileName && file.Type == "Video")
                    {
                        vid.Name = file.Name;
                        vid.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        vid.CategoryID = vid.Category.ID;
                    }
                }
                vid.FileName = fileName;
                vid.Path = @"/resources/Machines/Videos/";
                vid.Extension = video.FileName.Substring(video.FileName.LastIndexOf("."), (video.FileName.Length - video.FileName.LastIndexOf(".")));
                vid.Machine = machineFVM.Machine;
                vid.MachineID = machineFVM.Machine.ID;
                await _machineService.InsertMachineVideoAsync(vid);
                machineFVM.Machine.AddVideo(vid);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }
            }

            //Files with location path and web
            foreach (MachineFile file in MFiles)
            {
                if (file.Type == "Document" && (file.Location == "path" || file.Location == "web"))
                {                  
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineDocument doc = new MachineDocument 
                    { 
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineFVM.Machine,
                        MachineID = machineFVM.Machine.ID
                    };
                    await _machineService.InsertMachineDocumentAsync(doc);
                    machineFVM.Machine.AddDocument(doc);
                }
                if (file.Type == "Image" && (file.Location == "path" || file.Location == "web"))
                {
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineImage img = new MachineImage
                    {
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineFVM.Machine,
                        MachineID = machineFVM.Machine.ID
                    };
                    await _machineService.InsertMachineImageAsync(img);
                    machineFVM.Machine.AddImage(img);
                }
                if (file.Type == "Video" && (file.Location == "path" || file.Location == "web"))
                {
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineVideo vid = new MachineVideo
                    {
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineFVM.Machine,
                        MachineID = machineFVM.Machine.ID
                    };
                    await _machineService.InsertMachineVideoAsync(vid);
                    machineFVM.Machine.AddVideo(vid);
                }
            }

            if (machineFVM.SelectedResponsibles != null)
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

            return ("Machine created successfuly");
        }

        public async Task<IActionResult> Edit(int? ID)
        {
            Permissions();
            var machine = await _machineService.FindByIDAsync(ID.Value);

            var allResponsibles = await _responsibleService.FindAllAsync();
            var responsibles = ((from obj in machine.MachineResponsibles select obj).ToList()).Select(r => r.Responsible).ToList();
            var allDevices = await _deviceService.FindAllAsync();
            var devices = ((from obj in machine.MachineDevices select obj).ToList()).Select(d => d.Device).ToList();

            allResponsibles = allResponsibles.Where(x => !responsibles.Contains(x)).ToList();
            allDevices = allDevices.Where(x => !devices.Contains(x)).ToList();

            SelectList selectResponsibles = new SelectList(allResponsibles, "ID", "FullName");
            SelectList selectedResponsibles = new SelectList(responsibles, "ID", "FullName");
            ViewBag.ListSelectResponsibles = selectResponsibles;
            ViewBag.ListSelectedResponsibles = selectedResponsibles;

            SelectList selectDevices = new SelectList(allDevices, "ID", "Name");
            SelectList selectedDevices = new SelectList(devices, "ID", "Name");
            ViewBag.ListSelectDevices = selectDevices;
            ViewBag.ListSelectedDevices = selectedDevices;

            List<Area> areas = await _areaService.FindAllAsync();
            ViewBag.ListAreas = areas;

            List<Sector> sectors = await _sectorService.FindAllAsync();
            ViewBag.ListSectors = sectors;

            List<Line> lines = await _lineService.FindAllAsync();
            ViewBag.ListLines = lines;

            List<Category> categories = await _categoryService.FindAllAsync();
            ViewBag.ListCategories = categories;

            ViewData["SelectedArea"] = machine.Area.Name;
            ViewData["SelectedSector"] = machine.Sector.Name;
            ViewData["SelectedLine"] = machine.Line.Name;

            var viewModel = new MachineFormViewModel { Machine = machine, Responsibles = responsibles, Devices = devices };

            return View(viewModel);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task Edit(MachineFormViewModel machineNEW, IFormFile photo)
        {
            Permissions();

            //Get data from original machine
            Machine machineOLD = await _machineService.FindByIDAsync(machineNEW.Machine.ID);

            machineOLD.Name = machineNEW.Machine.Name;
            machineOLD.Description = machineNEW.Machine.Description;
            machineOLD.AssetNumber = machineNEW.Machine.AssetNumber;
            machineOLD.MES_Name = machineNEW.Machine.MES_Name;
            machineOLD.SAP_Name = machineNEW.Machine.SAP_Name;
            machineOLD.WorkCenter = machineNEW.Machine.WorkCenter;
            machineOLD.CostCenter = machineNEW.Machine.CostCenter;
            machineOLD.ServerPath = machineNEW.Machine.ServerPath;
            machineOLD.LastPreventiveMaintenance = machineNEW.Machine.LastPreventiveMaintenance;
            machineOLD.StartDate = machineNEW.Machine.StartDate;

            //Generate list of documents from the update page
            List<IFormFile> documents = new List<IFormFile>();
            List<IFormFile> images = new List<IFormFile>();
            List<IFormFile> videos = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                if (file.Name == "document") { documents.Add(file); }
                if (file.Name == "image") { images.Add(file); }
                if (file.Name == "video") { videos.Add(file); }
            }

            //Get list of files to be removed
            List<MachineFile> MFiles = JsonConvert.DeserializeObject<List<MachineFile>>(Request.Form["files"]);
            int[] DRemove = JsonConvert.DeserializeObject<int[]>(Request.Form["DRemove"]);
            int[] IRemove = JsonConvert.DeserializeObject<int[]>(Request.Form["IRemove"]);
            int[] VRemove = JsonConvert.DeserializeObject<int[]>(Request.Form["VRemove"]);

            List<MachineDocument> RemoveDocumets = new List<MachineDocument>();
            foreach (int ID in DRemove)
            {
                RemoveDocumets.Add(await _machineService.FindMachineDocumentsByID(machineOLD.ID, ID));
            }

            List<MachineImage> RemoveImages = new List<MachineImage>();
            foreach (int ID in IRemove)
            {
                RemoveImages.Add(await _machineService.FindMachineImagesByID(machineOLD.ID, ID));
            }

            List<MachineVideo> RemoveVideos = new List<MachineVideo>();
            foreach (int ID in VRemove)
            {
                RemoveVideos.Add(await _machineService.FindMachineVideosByID(machineOLD.ID, ID));
            }

            //Update Area
            if (machineNEW.Machine.AreaID != machineOLD.AreaID)
            {
                machineOLD.Area = await _areaService.FindByIDAsync(machineNEW.Machine.AreaID);
                machineOLD.AreaID = machineOLD.Area.ID;
            }

            //Update Sector
            if (machineNEW.Machine.SectorID != machineOLD.SectorID)
            {
                machineOLD.Sector = await _sectorService.FindByIDAsync(machineNEW.Machine.SectorID);
                machineOLD.SectorID = machineOLD.Sector.ID;
            }

            //Update Line
            if (machineNEW.Machine.LineID != machineOLD.LineID)
            {
                machineOLD.Line = await _lineService.FindByIDAsync(machineNEW.Machine.LineID);
                machineOLD.LineID = machineOLD.Line.ID;
            }

            //Update Photo
            if (photo != null)
            {
                //Remove OLD Photo
                try
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + machineOLD.ImagePath))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + machineOLD.ImagePath);
                    }
                }
                catch
                {
                }

                //long filesSize = photo.Length;
                //var filePath = Path.GetTempFileName();

                if (photo == null || photo.Length == 0)
                {
                    //return ("Error: Invalid path - Machine Photo");
                }

                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                fileName += photo.FileName.Substring(photo.FileName.LastIndexOf("."), (photo.FileName.Length - photo.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Photo\\" + fileName;
                machineOLD.ImagePath = @"/resources/Machines/Photo/" + fileName;

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }
            }

            //Add documents
            foreach (IFormFile document in documents)
            {
                long filesSize = document.Length;
                //var filePath = Path.GetTempFileName();

                if (document == null || document.Length == 0)
                {
                    //return ("Error: Invalid path - Machine Document");
                }

                string fileName = document.FileName.Substring(0, document.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Documents\\" + fileName;
                MachineDocument doc = new MachineDocument();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == document.FileName && file.Type == "Document")
                    {
                        doc.Name = file.Name;
                        doc.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        doc.CategoryID = doc.Category.ID;
                    }
                }
                doc.FileName = fileName;
                doc.Path = @"/resources/Machines/Documents/";
                doc.Extension = document.FileName.Substring(document.FileName.LastIndexOf("."), (document.FileName.Length - document.FileName.LastIndexOf(".")));
                doc.Machine = machineNEW.Machine;
                doc.MachineID = machineNEW.Machine.ID;

                await _machineService.InsertMachineDocumentAsync(doc);
                machineOLD.AddDocument(doc);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await document.CopyToAsync(stream);
                }
            }

            //Add images
            foreach (IFormFile image in images)
            {
                long filesSize = image.Length;
                var filePath = Path.GetTempFileName();

                if (image == null || image.Length == 0)
                {
                    //return ("Error: Invalid path - Machine Images");
                }

                string fileName = image.FileName.Substring(0, image.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Images\\" + fileName;
                MachineImage img = new MachineImage();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == image.FileName && file.Type == "Image")
                    {
                        img.Name = file.Name;
                        img.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        img.CategoryID = img.Category.ID;
                    }
                }
                img.FileName = fileName;
                img.Path = @"/resources/Machines/Images/";
                img.Extension = image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                img.Machine = machineNEW.Machine;
                img.MachineID = machineNEW.Machine.ID;
                await _machineService.InsertMachineImageAsync(img);
                machineOLD.AddImage(img);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }

            //Add videos
            foreach (IFormFile video in videos)
            {
                long filesSize = video.Length;
                var filePath = Path.GetTempFileName();

                if (video == null || video.Length == 0)
                {
                    //return ("Error: Invalid path - Machine Videos");
                }

                string fileName = video.FileName.Substring(0, video.FileName.LastIndexOf(".")) + "_" + DateTime.Now.ToString("yyMMddHHmmssfffffff");
                fileName += video.FileName.Substring(video.FileName.LastIndexOf("."), (video.FileName.Length - video.FileName.LastIndexOf(".")));
                string destinationPath = _appEnvironment.WebRootPath + "\\resources\\Machines\\Videos\\" + fileName;
                MachineVideo vid = new MachineVideo();
                foreach (MachineFile file in MFiles)
                {
                    if (file.FileName == video.FileName && file.Type == "Video")
                    {
                        vid.Name = file.Name;
                        vid.Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                        vid.CategoryID = vid.Category.ID;
                    }
                }
                vid.FileName = fileName;
                vid.Path = @"/resources/Machines/Videos/";
                vid.Extension = video.FileName.Substring(video.FileName.LastIndexOf("."), (video.FileName.Length - video.FileName.LastIndexOf(".")));
                vid.Machine = machineNEW.Machine;
                vid.MachineID = machineNEW.Machine.ID;
                await _machineService.InsertMachineVideoAsync(vid);
                machineOLD.AddVideo(vid);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }
            }

            //Files with location path and web
            foreach (MachineFile file in MFiles)
            {
                if (file.Type == "Document" && (file.Location == "path" || file.Location == "web"))
                {
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineDocument doc = new MachineDocument
                    {
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineOLD,
                        MachineID = machineOLD.ID
                    };
                    await _machineService.InsertMachineDocumentAsync(doc);
                    machineOLD.AddDocument(doc);
                }
                if (file.Type == "Image" && (file.Location == "path" || file.Location == "web"))
                {
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineImage img = new MachineImage
                    {
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineOLD,
                        MachineID = machineOLD.ID
                    };
                    await _machineService.InsertMachineImageAsync(img);
                    machineOLD.AddImage(img);
                }
                if (file.Type == "Video" && (file.Location == "path" || file.Location == "web"))
                {
                    Category Category = await _categoryService.FindByIDAsync(int.Parse(file.Category));
                    MachineVideo vid = new MachineVideo
                    {
                        Name = file.Name,
                        FileName = file.FileName,
                        Category = Category,
                        CategoryID = Category.ID,
                        Location = file.Location,
                        Machine = machineOLD,
                        MachineID = machineOLD.ID
                    };
                    await _machineService.InsertMachineVideoAsync(vid);
                    machineOLD.AddVideo(vid);
                }
            }

            //Remove documents
            foreach (MachineDocument document in RemoveDocumets)
            {
                try
                {
                    await _machineService.RemoveMachineDocumentAsync(document);
                    machineOLD.MachineDocuments.Remove(document);
                    if(document.Location == "file")
                    {

                    }
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + document.Path + document.FileName))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + document.Path + document.FileName);
                    }
                }
                catch
                {
                }
            }

            //Remove images
            foreach (MachineImage image in RemoveImages)
            {
                try
                {
                    await _machineService.RemoveMachineImageAsync(image);
                    machineOLD.MachineImages.Remove(image);
                    if (image.Location == "file")
                    {
                        if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + image.Path + image.FileName))
                        {
                            System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + image.Path + image.FileName);
                        }
                    }
                }
                catch
                {
                }
            }

            //Remove videos
            foreach (MachineVideo video in RemoveVideos)
            {
                try
                {
                    await _machineService.RemoveMachineVideoAsync(video);
                    machineOLD.MachineVideos.Remove(video);
                    if (video.Location == "file")
                    {
                        if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + video.Path + video.FileName))
                        {
                            System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + video.Path + video.FileName);
                        }
                    }
                }
                catch
                {
                }
            }

            //Add Responsibles to machine
            if(machineNEW.SelectedResponsibles != null)
            {
                foreach (int res in machineNEW.SelectedResponsibles)
                {
                    bool found = false;
                    foreach (MachineResponsible resOLD in machineOLD.MachineResponsibles)
                    {
                        if (resOLD.ResponsibleID == res && resOLD.MachineID == machineOLD.ID)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        Responsible responsible = await _responsibleService.FindByIDAsync(res);
                        MachineResponsible machineResponsible = new MachineResponsible { Machine = machineNEW.Machine, MachineID = machineNEW.Machine.ID, Responsible = responsible, ResponsibleID = responsible.ID };
                        await _machineService.InsertMachineResponsibleAsync(machineResponsible);
                        machineOLD.MachineResponsibles.Add(machineResponsible);
                    }
                }
            }
            
            //Remove responsibles from machine
            if (machineOLD.MachineResponsibles != null)
            {
                foreach (MachineResponsible resOLD in machineOLD.MachineResponsibles.ToList())
                {
                    bool found = false;
                    foreach (int res in machineNEW.SelectedResponsibles)
                    {
                        if (res == resOLD.ResponsibleID && resOLD.MachineID == machineOLD.ID)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        await _machineService.RemoveMachineResponsibleAsync(resOLD.MachineID, resOLD.ResponsibleID);
                        machineOLD.MachineResponsibles.Remove(resOLD);
                    }
                }
            }

            //Add Devices to machine
            if (machineNEW.SelectedDevices != null)
            {
                foreach (int dev in machineNEW.SelectedDevices)
                {
                    bool found = false;
                    foreach (MachineDevice devOLD in machineOLD.MachineDevices)
                    {
                        if (devOLD.DeviceID == dev && devOLD.MachineID == machineOLD.ID)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        Device device = await _deviceService.FindByIDAsync(dev);
                        MachineDevice machineDevice = new MachineDevice { Machine = machineNEW.Machine, MachineID = machineNEW.Machine.ID, Device = device, DeviceID = device.ID };
                        await _machineService.InsertMachineDeviceAsync(machineDevice);
                        machineOLD.MachineDevices.Add(machineDevice);
                    }
                }
            }

            //Remove devices from machine
            if (machineOLD.MachineDevices != null)
            {
                foreach (MachineDevice devOLD in machineOLD.MachineDevices.ToList())
                {
                    bool found = false;
                    foreach (int res in machineNEW.SelectedDevices)
                    {
                        if (res == devOLD.DeviceID && devOLD.MachineID == machineOLD.ID)
                        {
                            found = true;
                        }
                    }
                    if (found == false)
                    {
                        //await _machineService.RemoveMachineResponsibleAsync(devOLD.MachineID, devOLD.DeviceID);
                        machineOLD.MachineDevices.Remove(devOLD);
                    }
                }
            }

            await _machineService.UpdateAsync(machineOLD);

            HttpContext.Session.SetString("notificationMessage", "Maquina atualizada com sucesso");
            HttpContext.Session.SetString("notificationIcon", "bi-check-circle");
            HttpContext.Session.SetString("notificationType", "success");
        }

        public async Task<IActionResult> Delete(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            Permissions();
            var machine = await _machineService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + machine.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + machine.ImagePath);
                }
                foreach (MachineDocument MDoc in machine.MachineDocuments)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + MDoc.Path))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + MDoc.Path);
                    }
                }
                foreach (MachineImage MImg in machine.MachineImages)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + MImg.Path))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + MImg.Path);
                    }
                }
                foreach (MachineVideo MVid in machine.MachineVideos)
                {
                    if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + MVid.Path))
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + MVid.Path);
                    }
                }
            }
            catch
            {

            }
            await _machineService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _identityContext.Department.ToList();
            ViewBag.ListDepartments = departments;
            ViewBag.MachineName = obj.Name;
            ViewBag.MachineID = obj.ID;

            return View(obj);
        }

        public async Task<IActionResult> MachineComments(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> MachineDevices(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> MachineDocuments(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> MachineImages(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<IActionResult> MachineVideos(int? ID)
        {
            Permissions();
            if (ID == null)
            {
                return NotFound();
            }
            var obj = await _machineService.FindByIDAsync(ID.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public async Task<JsonResult> GetSector(int AreaID)
        {
            List<Sector> SectorList;
            SectorList = await _sectorService.FindByAreaIDAsync(AreaID);
            return Json(new SelectList(SectorList, "ID", "Name"));
        }

        public async Task<JsonResult> GetLine(int SectorID)
        {
            List<Line> LineList;
            LineList = await _lineService.FindBySectorIDAsync(SectorID);
            return Json(new SelectList(LineList, "ID", "Name"));
        }

        [HttpPost]
        public IActionResult UploadFiles()
        {
            List<IFormFile> files = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                files.Add(file);
            }
            return View();
        }

        public FileResult Download(string filePath, string fileName, string extension)
        {
            IFileProvider provider = new PhysicalFileProvider(_appEnvironment.WebRootPath + filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimeType = MimeTypes.GetMimeType(extension);
            return File(readStream, mimeType, fileName);
        }

        public FileResult DownloadPath(string path)
        {
            var credentials = new UserCredentials("vt1", "uidj7882", "Engineer17*");
            string type = MimeTypes.GetMimeType(path);
            string name = ""; 
            if(path.Contains("\\"))
            {
                var pathLIO = path.LastIndexOf('\\');
                name = path.Substring(pathLIO + 1, (path.Length - (pathLIO + 1)));
            }
            var FileResult = Impersonation.RunAsUser(credentials, LogonType.Interactive, () =>
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(path);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, path);
            });
            return File(FileResult.FileContents, type, name);
        }

        [HttpPost]
        public async Task<string> AddComment(string UserID, string Comment, int MachineID)
        {
            string data = "";
            try
            {
                Machine machine = await _machineService.FindByIDAsync(MachineID);
                MachineComment machineComment = new MachineComment { UserID = UserID, Comment = Comment, Date = DateTime.Now, Machine = machine, MachineID = machine.ID };
                await _machineService.InsertMachineCommentAsync(machineComment);
                List<MachineComment> comments = await _machineService.FindAllCommentsAsync(MachineID);
                foreach (MachineComment mComment in comments)
                {
                    data += "<tr><td>" + mComment.Comment + "</td> <td>" + mComment.User.FirstName + " " + mComment.User.LastName + "</td> <td>" + mComment.Date.ToString("dd/MM/yyyy HH:mm:ss") + "</td> <td>" + @"<button class=""btn btn-danger"" onclick=""removeComment(" + mComment.ID + @")"">Delete</button>" + "</td></tr>";
                }
            }
            catch
            {
            }
            return data;
        }

        [HttpPost]
        public async Task<string> RemoveComment(int CommentID, int MachineID)
        {
            await _machineService.RemoveCommentAsync(CommentID);

            string data = "";
            List<MachineComment> comments = await _machineService.FindAllCommentsAsync(MachineID);
            foreach (MachineComment mComment in comments)
            {
                data += "<tr><td>" + mComment.Comment + "</td> <td>" + mComment.User.FirstName + " " + mComment.User.LastName + "</td> <td>" + mComment.Date.ToString("dd/MM/yyyy HH:mm:ss") + "</td> <td>" + @"<button class=""btn btn-danger"" onclick=""removeComment(" + mComment.ID + @")"">Delete</button>" + "</td></tr>";
            }
            return data;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePassword(Password Password)
        {
            await _passwordService.InsertAsync(Password);
            return RedirectToAction("Details/" + Password.MachineID, "Machines");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(Password Password)
        {
            await _passwordService.UpdateAsync(Password);
            return RedirectToAction("Details/" +  Password.MachineID, "Machines");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePassword(Password Password)
        {
            await _passwordService.RemoveAsync(Password);
            return RedirectToAction("Details/" + Password.MachineID, "Machines");
        }

        public async Task<PartialViewResult> AddPartialEditPassword(int ID, string MachineName, int MachineID)
        {
            List<Department> departments = _identityContext.Department.ToList();
            Password password = await _passwordService.FindByIDAsync(ID);
            ViewBag.ListDepartments = departments;
            ViewBag.MachineName = MachineName;
            ViewBag.MachineID = MachineID;
            PartialViewResult partial = PartialView("PasswordEdit", password);
            return partial;
        }

        public async Task<PartialViewResult> AddPartialDeletePassword(int ID, string MachineName, int MachineID)
        {
            Password password = await _passwordService.FindByIDAsync(ID);
            ViewBag.MachineName = MachineName;
            ViewBag.MachineID = MachineID;
            PartialViewResult partial = PartialView("PasswordDelete", password);
            return partial;
        }

        [HttpPost]
        public async Task<string> UpdatePreventiveDate(string date, int ID)
        {
            string updatedDate = "";
            try
            {
                DateTime UpdDate = DateTime.Parse(date);
                await _machineService.UpdatePreventiveDate(UpdDate,ID);
                updatedDate = UpdDate.ToString("dd/MM/yyyy");
            }
            catch
            {
            }
            return updatedDate;
        }
    }
}