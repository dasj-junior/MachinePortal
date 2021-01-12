using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class ResponsibleService
    {
        private readonly MachinePortalContext _context;

        public ResponsibleService(MachinePortalContext context)
        {
            _context = context;
        }


        public async Task InsertAsync(Responsible obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Responsible> FindByIDAsync(int ID)
        {
            return await _context.Responsible.FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Responsible>> FindAllAsync()
        {
            return await _context.Responsible.OrderBy(x => x.ID).ToListAsync();
        }

        public async Task UpdateAsync(Responsible obj)
        {
            try
            {
                _context.Responsible.Update(obj);
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
                var obj = await _context.Responsible.FindAsync(ID);
                _context.Responsible.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
