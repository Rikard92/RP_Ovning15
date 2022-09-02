using Microsoft.EntityFrameworkCore;
using RP_Ovning15.Core.Entities;
using RP_Ovning15.Core.Repositories;
using RP_Ovning15.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Data.Repositories
{
    internal class ModuleRepository : IModuleRepository
    {
        private readonly LmsApiContext db;

        public ModuleRepository(LmsApiContext context)
        {
            db = context;
        }

        public async void Add(Module module)
        {
            db.Add(module);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db!.Module.AnyAsync(a => a.Id == id);
        }

        public async Task<Module?> FindAsync(int? id)
        {
            return await db!.Module.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await db.Module.ToListAsync();
        }

        public async Task<Module?> GetModule(int? id)
        {
            return await db!.Module.FirstOrDefaultAsync( a => a.Id == id);
        }

        public async void Remove(Module module)
        {
            db.Remove(module);
        }

        public async void Update(Module module)
        {
            db.Update(module);
        }
    }
}
