using ChallengeAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.UserDbContextContext
{
    
    public class UserDbContextContex : IdentityDbContext<User>
    {
        private const string Schema = "user";
        public UserDbContextContex(DbContextOptions<UserDbContextContex> options):base(options) 
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema(Schema);
        }
    }
}
