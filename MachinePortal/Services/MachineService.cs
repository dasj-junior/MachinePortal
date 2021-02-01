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

        public async Task<Machine> FindByIDAsync(int ID)
        {
            return await _context.Machine.FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Machine>> FindAllAsync()
        {
            return await _context.Machine.OrderBy(x => x.ID).ToListAsync();
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

    }
}
