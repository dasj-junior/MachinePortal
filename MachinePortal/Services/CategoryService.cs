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
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> FindByIDAsync(int ID)
        {
            return await _context.Category.FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Category>> FindAllAsync()
        {
            return await _context.Category.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task UpdateAsync(Category obj)
        {
            try
            {
                _context.Category.Update(obj);
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
                var obj = await _context.Category.FindAsync(ID);
                _context.Category.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

    }
}
