using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Beachapp.Models;

namespace Beachapp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Beach> Beaches{get;set;}
        public DbSet<UserData> UserDatas{get;set;}
        public DbSet<UsersData> UsersDatas{get;set;}
    }
}
