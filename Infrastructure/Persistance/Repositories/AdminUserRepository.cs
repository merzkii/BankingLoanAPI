using Application.Interfaces;
using Core.Entities;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AppDbContext _context;
public AdminUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(AdminUsers adminUser)
        {
            await _context.AdminUsers.AddAsync(adminUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var adminUser= await _context.AdminUsers.FindAsync(id);
            if (id != null)
            {
                _context.AdminUsers.Remove(adminUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AdminUsers>> GetAllAsync()
        {
            return await _context.AdminUsers.ToListAsync();
        }

        public async Task<AdminUsers?> GetByIdAsync(int id)
        {
            return await _context.AdminUsers.FindAsync(id);
        }

        public async Task UpdateAsync(AdminUsers adminUser)
        {
            
            
               _context.AdminUsers.Update(adminUser);
                await _context.SaveChangesAsync();

        }
    }
}
