using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThothCore.Domain.Persistence.Contexts;

namespace ThothCore.Domain.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
