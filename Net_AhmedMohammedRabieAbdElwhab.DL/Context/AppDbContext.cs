using Net_AhmedMohammedRabieAbdElwhab.DL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net_AhmedMohammedRabieAbdElwhab.DL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
           : base("DefaultConnection")
        {
        }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
        public DbSet<User> Users { get; set; }
    }
}

