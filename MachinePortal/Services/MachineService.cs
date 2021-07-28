using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class MachineService
    {
        private readonly MachinePortalContext _context;

        public MachineService(MachinePortalContext context)
        {
            _context = context;
        }

        //Machine
        public async Task InsertAsync(Machine obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Machine obj)
        {
            bool hasAny = await _context.Machine.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Machine.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Machine.FindAsync(ID);
                _context.Machine.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Machine Search
        public async Task<Machine> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Machine.Include(Mdoc => Mdoc.MachineDocuments)
                                        .ThenInclude(Mdoc => Mdoc.Category)
                                        .Include(Mimg => Mimg.MachineImages)
                                        .ThenInclude(Mimg => Mimg.Category)
                                        .Include(Mvid => Mvid.MachineVideos)
                                        .ThenInclude(Mvid => Mvid.Category)
                                        .Include(Mcom => Mcom.MachineComments)
                                        .ThenInclude(com => com.User)
                                        .Include(Mpas => Mpas.Passwords)
                                        .ThenInclude(Mpas => Mpas.Department)
                                        .Include(Mdev => Mdev.MachineDevices)
                                        .ThenInclude(dev => dev.Device)
                                        .Include(Mres => Mres.MachineResponsibles)
                                        .ThenInclude(res => res.Responsible)
                                        .ThenInclude(dep => dep.Department)
                                        .FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Machine>> FindByLine(int LineID)
        {
            try
            {
                return await _context.Machine.OrderBy(x => x.ID).Where(m => m.LineID == LineID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Machine>> FindAllAsync()
        {
            try
            {
                return await _context.Machine.OrderBy(x => x.ID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        //Machine Documents
        public async Task InsertMachineDocumentAsync(MachineDocument obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveMachineDocumentAsync(MachineDocument obj)
        {
            try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task InsertMachineImageAsync(MachineImage obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveMachineImageAsync(MachineImage obj)
        {
            try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task InsertMachineVideoAsync(MachineVideo obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveMachineVideoAsync(MachineVideo obj)
        {
            try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Machine Documents Search
        public async Task<List<MachineDocument>> FindAllMachineDocuments(int ID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mdoc => Mdoc.MachineDocuments).FirstOrDefaultAsync(obj => obj.ID == ID);
                return machine.MachineDocuments.ToList();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<MachineDocument> FindMachineDocumentsByID(int MachineID, int MachineDocumentID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mdoc => Mdoc.MachineDocuments).FirstOrDefaultAsync(obj => obj.ID == MachineID);
                return machine.MachineDocuments.FirstOrDefault(x => x.ID == MachineDocumentID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<MachineImage>> FindAllMachineImages(int ID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mimg => Mimg.MachineImages).FirstOrDefaultAsync(obj => obj.ID == ID);
                return machine.MachineImages.ToList();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<MachineImage> FindMachineImagesByID(int MachineID, int MachineImageID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mimg => Mimg.MachineImages).FirstOrDefaultAsync(obj => obj.ID == MachineID);
                return machine.MachineImages.FirstOrDefault(x => x.ID == MachineImageID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<MachineVideo>> FindAllMachineVideos(int ID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mvid => Mvid.MachineVideos).FirstOrDefaultAsync(obj => obj.ID == ID);
                return machine.MachineVideos.ToList();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<MachineVideo> FindMachineVideosByID(int MachineID, int MachineVideoID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mvid => Mvid.MachineVideos).FirstOrDefaultAsync(obj => obj.ID == MachineID);
                return machine.MachineVideos.FirstOrDefault(x => x.ID == MachineVideoID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        //Machine Responsibles
        public async Task InsertMachineResponsibleAsync(MachineResponsible obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveMachineResponsibleAsync(int MachineID, int ReponsibleID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mres => Mres.MachineResponsibles).FirstOrDefaultAsync(obj => obj.ID == MachineID);
                foreach (MachineResponsible Mres in machine.MachineResponsibles)
                {
                    if (Mres.ResponsibleID == ReponsibleID)
                    {
                        _context.Remove(Mres);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Machine Devices
        public async Task InsertMachineDeviceAsync(MachineDevice obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveMachineDeviceAsync(int MachineID, int DeviceID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mdev => Mdev.MachineDevices).FirstOrDefaultAsync(obj => obj.ID == MachineID);
                foreach (MachineDevice Mdev in machine.MachineDevices)
                {
                    if (Mdev.DeviceID == DeviceID)
                    {
                        _context.Remove(Mdev);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Machine Comments
        public async Task InsertMachineCommentAsync(MachineComment obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveCommentAsync(int ID)
        {
            try
            {
                var obj = await _context.MachineComment.FindAsync(ID);
                _context.MachineComment.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Machine Comments Search
        public async Task<List<MachineComment>> FindAllCommentsAsync(int ID)
        {
            try
            {
                var result = from obj in _context.MachineComment select obj;
                result = result.Include(u => u.User).Where(x => x.Machine.ID == ID);
                return await result.ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        //Preventive Maintenance
        public async Task UpdatePreventiveDate(DateTime date, int ID)
        {
            bool hasAny = await _context.Machine.AnyAsync(x => x.ID == ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                Machine machine = new Machine { ID = ID, LastPreventiveMaintenance = date };
                _context.Machine.Attach(machine);
                _context.Entry(machine).Property(x => x.LastPreventiveMaintenance).IsModified = true;
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }
    }
}
