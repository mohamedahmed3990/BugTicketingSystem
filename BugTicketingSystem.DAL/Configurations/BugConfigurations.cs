using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configurations
{
    public class BugConfigurations : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.Property(b => b.Name).IsRequired();

            builder.Property(b => b.Description).IsRequired();

            builder.HasOne(b => b.Project)
                .WithMany(p => p.Bugs)
                .HasForeignKey(b => b.ProjectId);
        }
    }
}
