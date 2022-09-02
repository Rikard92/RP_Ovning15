using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RP_Ovning15.Core.Entities;

namespace RP_Ovning15.Data.Data
{
    public class LmsApiContext : DbContext
    {
        public LmsApiContext(DbContextOptions<LmsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; } = default!;

        public DbSet<Module> Module { get; set; } = default!;
    }
}
