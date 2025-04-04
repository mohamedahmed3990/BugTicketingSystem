using System.Net.Mail;
using BugTicketingSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configurations
{
    internal class AttachmentConfiguration : IEntityTypeConfiguration<Entities.Attachment>
    {
        public void Configure(EntityTypeBuilder<Entities.Attachment> builder)
        {
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Description).IsRequired();

            builder.HasOne(a => a.Bug)
                .WithMany(b => b.Attachments)
                .HasForeignKey(a => a.BugId);
        }
    }
}
