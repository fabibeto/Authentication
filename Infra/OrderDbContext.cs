using ChallengeAPI.Entities;
using ChallengeAPI.OrderModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.Infra
{

     public class OrderDbContext:DbContext
    {
        public DbSet<Ciudad> Ciudades { get; set; }
        public DbSet<IconosGeograficos> IconosGeograficos { get; set; }
        public DbSet<Continente> Continentes { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        { }

    }
}
