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
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Device> FindByIDAsync(int ID)
        {
            return await _context.Device.Include(d => d.Documents).FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Device>> FindAllAsync()
        {
            return await _context.Device.OrderBy(x => x.ID).ToListAsync();
        }

        public async Task UpdateAsync(Device obj)
        {
            try
            {
                _context.Device.Update(obj);
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
                var obj = await _context.Device.FindAsync(ID);
                _context.Device.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
