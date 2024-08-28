using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Commons.Identity;
using CommonsDev.Models;

namespace CommonsDev.Data
{
    public class ApplicationDbContext : CommonsDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TestClass> Tests { get; set; }
        public DbSet<Institute> Institutes { get; set; }

        public override List<IWorkSpace> GetIWorkSpaces()
        {
            var list = new List<IWorkSpace>();
            list.AddRange(Institutes);
            return list;
        }

    }
}
