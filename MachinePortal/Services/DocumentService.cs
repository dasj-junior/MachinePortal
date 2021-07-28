using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class DocumentService
    {
        private readonly MachinePortalContext _context;

        public DocumentService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(DeviceDocument obj)
        {
            try
            {
                _context.DeviceDocument.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(DeviceDocument obj)
        {
            try
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<List<DeviceDocument>> FindByDeviceIDAsync(int ID)
        {
            try
            {
                var result = from obj in _context.DeviceDocument select obj;
                result = result.Where(x => x.Device.ID == ID);
                return await result.ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }; 
        }

        public async Task<DeviceDocument> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.DeviceDocument.FirstOrDefaultAsync(d => d.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

    }
}
