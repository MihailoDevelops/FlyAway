using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyAway.Domain.Entities;

namespace FlyAway.DataAccess.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).HasMaxLength(300).IsRequired();

            builder.HasMany(c => c.CommentLikes).WithOne(cl => cl.Comment).HasForeignKey(x => x.CommentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
