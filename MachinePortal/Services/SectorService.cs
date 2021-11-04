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
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Sector obj)
        {
            bool hasAny = await _context.Sector.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Sector.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Sector.FindAsync(ID);
                _context.Sector.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Sector> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Sector.Include(a => a.Area).Include(l => l.Lines).AsNoTracking().FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };  
        }

        public async Task<List<Sector>> FindByAreaIDAsync(int ID)
        {
            try
            {
                return await _context.Sector.Where(x => x.AreaID == ID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Sector>> FindAllAsync()
        {
            try
            {
                return await _context.Sector.OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

    }
}
