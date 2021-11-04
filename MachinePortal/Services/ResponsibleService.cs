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
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Responsible obj)
        {
            bool hasAny = await _context.Responsible.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Responsible.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Responsible.FindAsync(ID);
                _context.Responsible.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Responsible> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Responsible.Include(d => d.Department)
                .AsNoTracking()
                .FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

        public async Task<List<Responsible>> FindAllAsync()
        {
            try
            {
                return await _context.Responsible.Include(d => d.Department)
                .OrderBy(x => x.ID).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }
   
    }
}
