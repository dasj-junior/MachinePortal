using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class PermissionService
    {

        private readonly MachinePortalContext _context;

        public PermissionService(MachinePortalContext context)
        {
            _context = context;
        }


        public async Task InsertAsync(Permission obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Permission> FindByIDAsync(int ID)
        {
            return await _context.Permission.FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Permission>> FindAllAsync()
        {
            return await _context.Permission.OrderBy(x => x.ID).ToListAsync();
        }

        public async Task UpdateAsync(Permission obj)
        {
            try
            {
                _context.Permission.Update(obj);
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
                var obj = await _context.Permission.FindAsync(ID);
                _context.Permission.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

    }
}
