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
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Line obj)
        {
            bool hasAny = await _context.Line.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Line.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Line.FindAsync(ID);
                _context.Line.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Line> FindByIDAsync(int ID)
        {
            return await _context.Line.Include(s => s.Sector).Include(a => a.Sector.Area).AsNoTracking().FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Line>> FindBySectorIDAsync(int ID)
        {
            try
            {
                return await _context.Line.Where(x => x.SectorID == ID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Line>> FindAllAsync()
        {
            try
            {
                return await _context.Line.OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }; 
        }

        

    }
}
