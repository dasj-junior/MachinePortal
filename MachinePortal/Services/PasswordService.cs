using MachinePortal.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Password>> FindByMachineIDAsync(int ID)
        {
            Machine machine = await _context.Machine.Include(Mpas => Mpas.Passwords).FirstOrDefaultAsync(obj => obj.ID == ID);
            return machine.Passwords.ToList();
        }
        public async Task<Password> FindByIDAsync(int ID)
        {
            return await _context.Password.FirstOrDefaultAsync(x => x.ID == ID);
        }

        public async Task InsertAsync(Password obj)
        {
            _context.Password.Add(obj);
            await _context.SaveChangesAsync();        
        }

        public async Task UpdateAsync(Password obj)
        {
            _context.Password.Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Password obj)
        {
            _context.Password.Remove(obj);
            await _context.SaveChangesAsync();
        }


    }
}
