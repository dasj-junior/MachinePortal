using MachinePortal.Areas.Identity.Data;
using MachinePortal.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class SeedingServiceDEV
    {
        private readonly MachinePortalContext _MPcontext;
        private readonly IdentityContext _Icontext;
        private readonly UserManager<MachinePortalUser> _userManager;

        public SeedingServiceDEV(MachinePortalContext MPcontext, IdentityContext Icontext, UserManager<MachinePortalUser> userManager)
        {
            _MPcontext = MPcontext;
            _Icontext = Icontext;
            _userManager = userManager;
        }

        public void Seed()
        {
            if (_MPcontext.Area.Any() ||
                _MPcontext.Sector.Any() ||
                _MPcontext.Line.Any() ||
                _MPcontext.Category.Any() ||
                _MPcontext.Machine.Any())
            {
                return;
            }

            //Area
            Area area01 = new Area { ID = 1, Name = "Ala 01" };
            Area area02 = new Area { ID = 2, Name = "Ala 02" };

            //Sector
            Sector sector01 = new Sector { ID = 1, Name = "EC", AreaID = 1 };
            Sector sector02 = new Sector { ID = 2, Name = "S&A", AreaID = 1 };
            Sector sector03 = new Sector { ID = 3, Name = "FEM", AreaID = 1 };
            Sector sector04 = new Sector { ID = 4, Name = "PSS", AreaID = 2 };

            //Line
            Line line01 = new Line { ID = 1, Name = "ECU", SectorID = 1 };
            Line line02 = new Line { ID = 2, Name = "ETC 8.6", SectorID = 2 };
            Line line03 = new Line { ID = 3, Name = "Motor M34", SectorID = 2 };
            Line line04 = new Line { ID = 4, Name = "Cover", SectorID = 2 };
            Line line05 = new Line { ID = 5, Name = "Rotor", SectorID = 2 };
            Line line06 = new Line { ID = 6, Name = "Ford Module", SectorID = 3 };
            Line line07 = new Line { ID = 7, Name = "Fuel Pump", SectorID = 3 };
            Line line08 = new Line { ID = 8, Name = "GV4", SectorID = 4 };
            Line line09 = new Line { ID = 9, Name = "ACPS VW", SectorID = 4 };
            Line line10 = new Line { ID = 10, Name = "WSS Line 01", SectorID = 4 };
            Line line11 = new Line { ID = 11, Name = "WSS Line 02", SectorID = 4 };
            Line line12 = new Line { ID = 12, Name = "GEM", SectorID = 4 };

            //Category
            Category category01 = new Category { ID = 1, Name = "Industrial Engineering" };
            Category category02 = new Category { ID = 2, Name = "Development Engineering" };
            Category category03 = new Category { ID = 3, Name = "Test Engineering" };
            Category category04 = new Category { ID = 4, Name = "Quality" };
            Category category05 = new Category { ID = 5, Name = "Controlling" };
            Category category06 = new Category { ID = 6, Name = "MES" };

            //Category
            Machine machine01 = new Machine { ID = 1, AreaID = 1, SectorID = 1, LineID = 1,
                                              Name = "Leakage Test", Description = "ECU Line Leakage Test Locally Developed in 2010",
                                              AssetNumber = 123123, MES_Name = "523LKT11", CostCenter = 60427,
                                              SAP_Name = "523LKT", WorkCenter = "523", ServerPath = @"\\sltm105a\dide2781\MachinePortal\ES\ECU\P110_EST",
                                              LastPreventiveMaintenance = DateTime.Now, StartDate = DateTime.Now};


            //Area
            _MPcontext.Area.AddRange(area01, area02);
            //Sector
            _MPcontext.Sector.AddRange(sector01, sector02, sector03, sector04);
            //Line
            _MPcontext.Line.AddRange(line01, line02, line03, line04, line05, line06,
                                     line07, line08, line09, line10, line11, line12);
            //Category
            _MPcontext.Category.AddRange(category01, category02, category03, category04,
                                        category05, category06);
            //Machine
            _MPcontext.Machine.Add(machine01);

            //Save
            _MPcontext.SaveChangesAsync();
        }

    }
}
