using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class AssetService
    {
        private readonly MachinePortalContext _context;

        public AssetService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(Asset obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Asset> FindByIDAsync(int ID)
        {
            return await _context.Asset.FirstOrDefaultAsync(obj => obj.ID == ID);
        }

        public async Task<List<Asset>> FindAllAsync()
        {
            return await _context.Asset.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task RemoveAsync(int ID)
        {
            try
            {
                var obj = await _context.Asset.FindAsync(ID);
                _context.Asset.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
