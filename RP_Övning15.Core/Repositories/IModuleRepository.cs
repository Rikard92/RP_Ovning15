using RP_Ovning15.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Core.Repositories
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllModules(); 
        Task<Module> GetModule(int? id); 
        Task<Module> FindAsync(int? id); 
        Task<bool> AnyAsync(int? id); 
        void Add(Module module); 
        void Update(Module module); 
        void Remove(Module module);
    }
}
