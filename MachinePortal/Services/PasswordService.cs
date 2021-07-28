using MachinePortal.Models;
using MachinePortal.Services.Exceptions;
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

        public async Task InsertAsync(Password obj)
        {
            _context.Password.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Password obj)
        {
            bool hasAny = await _context.Password.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new NotFoundException("ID not found");
            }
            try
            { 
                _context.Password.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e) { throw new DbConcurrencyException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task RemoveAsync(Password obj)
        {
            try
            {
                _context.Password.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e) { throw new IntegrityException(e.Message); }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        public async Task<List<Password>> FindByMachineIDAsync(int ID)
        {
            try
            {
                Machine machine = await _context.Machine.Include(Mpas => Mpas.Passwords).FirstOrDefaultAsync(obj => obj.ID == ID);
                return machine.Passwords.ToList();
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }
        
        public async Task<Password> FindByIDAsync(int ID)
        {
            try
            {
                return await _context.Password.FirstOrDefaultAsync(x => x.ID == ID);
            }
            catch (Exception e) { throw new Exception(e.Message); };
        }

    }
}
