using Microsoft.EntityFrameworkCore;
using RP_Ovning15.Core.Repositories;
using RP_Ovning15.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Data.Repositories
{
    public class UnitofWork : IUnitofWork
    {
        private readonly LmsApiContext db;
        public ICourseRepository CourseRepository { get; private set; }

        public IModuleRepository ModuleRepository { get; private set; }

        public UnitofWork(LmsApiContext context)
        {
            db = context;

            CourseRepository = new CourseRepository(context);
            ModuleRepository = new ModuleRepository(context);
        }

        public async Task CompleteAsync()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
