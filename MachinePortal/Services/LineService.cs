using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class LineService
    {

        private readonly MachinePortalContext _context;

        public LineService(MachinePortalContext context)
        {
            _context = context;
        }


        public async Task InsertAsync(Line obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Line> FindByIDAsync(int ID)
        {
            return await _context.Line.Include(s => s.Sector).Include(a => a.Sector.Area).FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Line>> FindAllAsync()
        {
            return await _context.Line.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task UpdateAsync(Line obj)
        {
            try
            {
                _context.Line.Update(obj);
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
                var obj = await _context.Line.FindAsync(ID);
                _context.Line.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

    }
}
