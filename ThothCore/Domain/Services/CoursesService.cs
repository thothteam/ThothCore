using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothCore.Domain.Models;
using ThothCore.Domain.Repositories;

namespace ThothCore.Domain.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository _coursesRepository;

        public CoursesService(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<IEnumerable<Course>> ListAsync()
        {
            return await _coursesRepository.ListAsync();
        }
    }
}
