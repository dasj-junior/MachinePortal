using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class CategoryService
    {
        private readonly MachinePortalContext _context;

        public CategoryService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Category obj)
        {
            try
            {
                _context.Add(obj);
                await _context.SaveChangesAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task UpdateAsync(Category obj)
        {
            bool hasAny = await _context.Category.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            {
                _context.Category.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Category.FindAsync(ID);
                _context.Category.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<Category> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Category.AsNoTracking().FirstOrDefaultAsync(obj => obj.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); }; 
        }

        public async Task<List<Category>> FindAllAsync()
        {
            try
            {
                return await _context.Category.OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

    }
}
