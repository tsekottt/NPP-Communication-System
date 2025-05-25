using DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DAL.EF
{
    public class NPPCSContext
        : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Request> Requests { get; set; }

        public NPPCSContext(DbContextOptions options)
            : base(options)
        {
        }

    }
}
