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

        public async Task InsertMachineImageAsync(MachineImage obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineVideoAsync(MachineVideo obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineResponsibleAsync(MachineResponsible obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertMachineDeviceAsync(MachineDevice obj)
        {
            _context.Add(obj);
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
                                        .Include(Mimg => Mimg.MachineImages)
                                        .Include(Mvid => Mvid.MachineVideos)
                                        .Include(Mcom => Mcom.MachineComments)
                                        .Include(Mdev => Mdev.MachineDevices)
                                        .ThenInclude(dev => dev.Device)
                                        .Include(Mres => Mres.MachineResponsibles)
                                        .ThenInclude(res => res.Responsible)
                                        .FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Machine>> FindAllAsync()
        {
            return await _context.Machine.OrderBy(x => x.ID).ToListAsync();
        }

        public async Task<List<MachineComment>> FindAllCommentsAsync(int ID)
        {
            var result = from obj in _context.MachineComments select obj;
            result = result.Where(x => x.Machine.ID == ID);
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
                var obj = await _context.MachineComments.FindAsync(ID);
                _context.MachineComments.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }



    }
}
