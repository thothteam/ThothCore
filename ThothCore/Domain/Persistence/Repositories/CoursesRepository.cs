using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ThothCore.Domain.Models;
using ThothCore.Domain.Persistence.Contexts;
using ThothCore.Domain.Repositories;

namespace ThothCore.Domain.Persistence.Repositories
{
    public class CoursesRepository: BaseRepository, ICoursesRepository
    {
        public CoursesRepository(ApplicationDbContext context) : base(context)
        {

        }


        public async Task<IEnumerable<Course>> ListAsync()
        {
            return await _context.Courses.ToListAsync();
        }

    }
}
