using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP_Ovning15.Core.Repositories
{
    public interface IUnitofWork
    {
        ICourseRepository CourseRepository { get; }
        IModuleRepository ModuleRepository { get; }
        Task CompleteAsync();

    }
}
