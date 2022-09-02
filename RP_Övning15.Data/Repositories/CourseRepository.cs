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
    public class CourseRepository : ICourseRepository
    {
        private readonly LmsApiContext db;

        public CourseRepository(LmsApiContext context)
        {
            db = context;
        }

        public async void Add(Course course)
        {
            db.Add(course);
        }

        public async Task<bool> AnyAsync(int? id)
        {
            return await db!.Course.AnyAsync(a => a.Id == id);
        }

        public async Task<Course?> FindAsync(int? id)
        {
            return await db!.Course.Include(e => e.Modules).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await db.Course.Include(e => e.Modules).ToListAsync();
        }

        public async Task<Course?> GetCourse(int? id)
        {            
            return await db!.Course.Include(e => e.Modules).FirstOrDefaultAsync(a => a.Id == id);
        }

        public void Remove(Course course)
        {
            db.Remove(course);  
        }

        public void Update(Course course)
        {
            db.Update(course);
        }
    }
}
