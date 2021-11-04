using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class AreaService
    {

        private readonly MachinePortalContext _context;

        public AreaService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Area obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Area obj)
        {
            bool hasAny = await _context.Area.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Area.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Area.FindAsync(ID);
                _context.Area.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Area> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Area.Include(s => s.Sectors).AsNoTracking().FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Area>> FindAllAsync()
        {
            try
            {
                return await _context.Area.Include(s => s.Sectors).OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

    }
}
