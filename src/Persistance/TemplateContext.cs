using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
        {

        }
        public DbSet<ValueEntity> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ValueEntity>()
                .ToTable("Value")
                .HasKey(x => x.Id);
        
        }
    }
}
