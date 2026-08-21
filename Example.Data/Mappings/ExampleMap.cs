using ExampleMS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Example.Data.Mappings
{
    public class ExampleMap : IEntityTypeConfiguration<ExampleData>
    {
        public void Configure(EntityTypeBuilder<ExampleData> builder)
        {
            _ = builder.ToTable("Example");
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            _ = builder.Property(x => x.Description).HasMaxLength(20);
            _= builder.Property(x => x.ActiveStatus).IsRequired();
            _ = builder.Property(x => x.CreatedUserId);
            _ = builder.Property(x => x.EditedUserId);
            _ = builder.Property(x => x.CreatedDate);
            _ = builder.Property(x => x.EditedDate);

        }
    }
}
