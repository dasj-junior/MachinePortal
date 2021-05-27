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

        public async Task InsertAsync(Machine obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineDocumentAsync(MachineDocument obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMachineDocumentAsync(MachineDocument obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineImageAsync(MachineImage obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMachineImageAsync(MachineImage obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineVideoAsync(MachineVideo obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMachineVideoAsync(MachineVideo obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineResponsibleAsync(MachineResponsible obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMachineResponsibleAsync(int MachineID, int ReponsibleID)
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

        public async Task InsertMachineDeviceAsync(MachineDevice obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveMachineDeviceAsync(int MachineID, int DeviceID)
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

        public async Task InsertMachineCommentAsync(MachineComment obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Machine> FindByIDAsync(int ID)
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
                                        //.ThenInclude(Mpas => Mpas.Department)
                                        .Include(Mdev => Mdev.MachineDevices)
                                        .ThenInclude(dev => dev.Device)
                                        .Include(Mres => Mres.MachineResponsibles)
                                        .ThenInclude(res => res.Responsible)
                                        .FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Machine>> FindByLine(int LineID)
        {
            return await _context.Machine.OrderBy(x => x.ID).Where(m => m.LineID == LineID).ToListAsync();
        }

        public async Task<List<MachineDocument>> FindAllMachineDocuments(int ID)
        {
            Machine machine = await _context.Machine.Include(Mdoc => Mdoc.MachineDocuments).FirstOrDefaultAsync(obj => obj.ID == ID);
            return machine.MachineDocuments.ToList();
        }

        public async Task<MachineDocument> FindMachineDocumentsByID(int MachineID, int MachineDocumentID)
        {
            Machine machine = await _context.Machine.Include(Mdoc => Mdoc.MachineDocuments).FirstOrDefaultAsync(obj => obj.ID == MachineID);
            return machine.MachineDocuments.FirstOrDefault(x => x.ID == MachineDocumentID);
        }

        public async Task<List<MachineImage>> FindAllMachineImages(int ID)
        {
            Machine machine = await _context.Machine.Include(Mimg => Mimg.MachineImages).FirstOrDefaultAsync(obj => obj.ID == ID);
            return machine.MachineImages.ToList();
        }

        public async Task<MachineImage> FindMachineImagesByID(int MachineID, int MachineImageID)
        {
            Machine machine = await _context.Machine.Include(Mimg => Mimg.MachineImages).FirstOrDefaultAsync(obj => obj.ID == MachineID);
            return machine.MachineImages.FirstOrDefault(x => x.ID == MachineImageID);
        }

        public async Task<List<MachineVideo>> FindAllMachineVideos(int ID)
        {
            Machine machine = await _context.Machine.Include(Mvid => Mvid.MachineVideos).FirstOrDefaultAsync(obj => obj.ID == ID);
            return machine.MachineVideos.ToList();
        }

        public async Task<MachineVideo> FindMachineVideosByID(int MachineID, int MachineVideoID)
        {
            Machine machine = await _context.Machine.Include(Mvid => Mvid.MachineVideos).FirstOrDefaultAsync(obj => obj.ID == MachineID);
            return machine.MachineVideos.FirstOrDefault(x => x.ID == MachineVideoID);
        }

        public async Task<List<Machine>> FindAllAsync()
        {
            return await _context.Machine.OrderBy(x => x.ID).ToListAsync();
        }

        public async Task<List<MachineComment>> FindAllCommentsAsync(int ID)
        {
            var result = from obj in _context.MachineComment select obj;
            result = result.Include(u => u.User).Where(x => x.Machine.ID == ID);
            return await result.ToListAsync();
        }

        public async Task UpdateAsync(Machine obj)
        {
            try
            {
                _context.Machine.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Machine.FindAsync(ID);
                _context.Machine.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task RemoveCommentAsync(int ID)
        {
            try
            {
                var obj = await _context.MachineComment.FindAsync(ID);
                _context.MachineComment.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
