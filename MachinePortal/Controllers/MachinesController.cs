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

        public async Task<IActionResult> Index()
        {
            List<Machine> machines = await _machineService.FindAllAsync();

            return View(machines);
        }

        public async Task<IActionResult> Create()
        {
            var responsibles = await _responsibleService.FindAllAsync();
            var devices = await _deviceService.FindAllAsync();

            SelectList slResponsibles = new SelectList(responsibles, "ID", "FullName");
            ViewBag.ListResponsibles = slResponsibles;

            SelectList slDevices = new SelectList(devices, "ID", "Name");
            ViewBag.ListDevices = slDevices;

            List<Area> areas = await _areaService.FindAllAsync();       
            ViewBag.ListAreas = areas;


            var viewModel = new MachineFormViewModel {  Responsibles = responsibles, Devices = devices };
            return View(viewModel);

        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [ValidateAntiForgeryToken]
        public async Task<String> Create(MachineFormViewModel machineFVM, IFormFile photo)
        {
            List<IFormFile> documents = new List<IFormFile>();
            List<IFormFile> images = new List<IFormFile>();
            List<IFormFile> videos = new List<IFormFile>();
            foreach (var file in Request.Form.Files)
            {
                if(file.Name == "document") { documents.Add(file); }
                if (file.Name == "image") { images.Add(file); }
                if (file.Name == "video") { videos.Add(file); }
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
                doc.Name = fileName;
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
                MachineImage doc = new MachineImage();
                doc.Name = fileName;
                doc.Path = @"/resources/Machines/Images/";
                doc.Extension = image.FileName.Substring(image.FileName.LastIndexOf("."), (image.FileName.Length - image.FileName.LastIndexOf(".")));
                doc.Machine = machineFVM.Machine;
                doc.MachineID = machineFVM.Machine.ID;
                await _machineService.InsertMachineImageAsync(doc);
                machineFVM.Machine.AddImage(doc);

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
                MachineVideo doc = new MachineVideo();
                doc.Name = fileName;
                doc.Path = @"/resources/Machines/Videos/";
                doc.Extension = video.FileName.Substring(video.FileName.LastIndexOf("."), (video.FileName.Length - video.FileName.LastIndexOf(".")));
                doc.Machine = machineFVM.Machine;
                doc.MachineID = machineFVM.Machine.ID;
                await _machineService.InsertMachineVideoAsync(doc);
                machineFVM.Machine.AddVideo(doc);

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
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

        public async Task<IActionResult> Delete(int? ID)
        {
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
            var machine = await _machineService.FindByIDAsync(ID);
            try
            {
                if (System.IO.File.Exists(_appEnvironment.WebRootPath + "\\" + machine.ImagePath))
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + "\\" + machine.ImagePath);
                }
                foreach(MachineDocument MDoc in machine.MachineDocuments)
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
                //foreach (MachineComment MCom in machine.MachineComments)
                //{

                //}
                //foreach (MachineDevice MDev in machine.MachineDevices)
                //{

                //}
                //foreach (MachineResponsible MRes in machine.MachineResponsibles)
                //{

                //}
            }
            catch
            {

            }
            await _machineService.RemoveAsync(ID);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? ID)
        {
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

        public FileResult Download(string filePath, string fileName, string extension)
        {
            IFileProvider provider = new PhysicalFileProvider(_appEnvironment.WebRootPath + filePath);
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            string mimeType = MimeTypes.GetMimeType(extension);
            return File(readStream, mimeType, fileName);
        }

        [HttpPost]
        public async Task<string> AddComment(string Author, string Comment, int MachineID)
        {
            Machine machine = await _machineService.FindByIDAsync(MachineID);
            MachineComment machineComment = new MachineComment { Author = Author, Comment = Comment, Date = DateTime.Now, Machine = machine, MachineID = machine.ID };
            await _machineService.InsertMachineCommentAsync(machineComment);

            string data = "";
            List<MachineComment> comments = await _machineService.FindAllCommentsAsync(MachineID);
            foreach(MachineComment mComment in comments)
            {
                data+= "<tr><td>" + mComment.Comment + "</td> <td>" + mComment.Author + "</td> <td>" + mComment.Date.ToString("dd/MM/yyyy HH:mm:ss") + "</td> <td>" + @"<button class=""btn btn-danger"" onclick=""removeComment(" + mComment.ID + @")"">Delete</button>" + "</td></tr>";
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
                data += "<tr><td>" + mComment.Comment + "</td> <td>" + mComment.Author + "</td> <td>" + mComment.Date.ToString("dd/MM/yyyy HH:mm:ss") + "</td> <td>" + @"<button class=""btn btn-danger"" onclick=""removeComment(" + mComment.ID + @")"">Delete</button>" + "</td></tr>";
            }
            return data;
        }

    }
}