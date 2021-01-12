using MachinePortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class DocumentService
    {
        private readonly MachinePortalContext _context;

        public DocumentService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task<List<Document>> FindByDeviceIDAsync(int ID)
        {
            var result = from obj in _context.Document select obj;
            result = result.Where(x => x.Device.ID == ID);
            return await result.ToListAsync();
        }

        public async Task InsertAsync(Document obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Document obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }


    }
}
