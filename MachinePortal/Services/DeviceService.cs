using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class DeviceService
    {
        private readonly MachinePortalContext _context;

        public DeviceService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Device obj)
        {   
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Device obj)
        {
            bool hasAny = await _context.Device.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Device.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Device.FindAsync(ID);
                _context.Device.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Device> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Device.Include(d => d.Documents).FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };   
        }

        public async Task<List<Device>> FindAllAsync()
        {
            try
            {
                return await _context.Device.OrderBy(x => x.ID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }
     
    }
}
