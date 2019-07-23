using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothCore.Domain.Models;

namespace ThothCore.Domain.Repositories
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> ListAsync();
    }
}
