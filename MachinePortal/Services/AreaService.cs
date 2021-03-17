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
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Area> FindByIDAsync(int ID)
        {
            return await _context.Area.Include(s => s.Sectors).FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Area>> FindAllAsync()
        {
            return await _context.Area.Include(s => s.Sectors).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task UpdateAsync(Area obj)
        {
            try
            {
                _context.Area.Update(obj);
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
                var obj = await _context.Area.FindAsync(ID);
                _context.Area.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

    }
}
