using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class SectorService
    {

        private readonly MachinePortalContext _context;

        public SectorService(MachinePortalContext context)
        {
            _context = context;
        }


        public async Task InsertAsync(Sector obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Sector> FindByIDAsync(int ID)
        {
            return await _context.Sector.Include(a => a.Area).Include(l => l.Lines).FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Sector>> FindAllAsync()
        {
            return await _context.Sector.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task UpdateAsync(Sector obj)
        {
            try
            {
                _context.Sector.Update(obj);
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
                var obj = await _context.Sector.FindAsync(ID);
                _context.Sector.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

    }
}
