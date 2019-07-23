using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThothCore.Domain.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<CourseUnit> Units { get; set; } = new List<CourseUnit>();
    }
}
