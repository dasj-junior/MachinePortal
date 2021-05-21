using MachinePortal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachinePortal.Services
{
    public class PasswordService
    {
        private readonly MachinePortalContext _context;

        public PasswordService(MachinePortalContext context)
        {
            _context = context;
        }

        public async Task<List<MachinePassword>> FindByMachineIDAsync(int ID)
        {
            Machine machine = await _context.Machine.Include(Mpas => Mpas.MachinePasswords).FirstOrDefaultAsync(obj => obj.ID == ID);
            return machine.MachinePasswords.ToList();
        }

        public async Task InsertAsync(MachinePassword obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(MachinePassword obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }


    }
}
