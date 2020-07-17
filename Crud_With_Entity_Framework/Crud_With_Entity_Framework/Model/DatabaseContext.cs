using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud_With_Entity_Framework.Model
{
    class DatabaseContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
