using System;
using System.ComponentModel;
using code.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code.Infrastructure.EntityConfigurations
{
    public class InstructorEntityTypeConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructor", DataContext.DefaultSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property<string>(x => x.LastName)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property<string>(x => x.FirstName)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property<DateTime>(x => x.HireDate)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Class)
            .WithOne(x => x.Instructor);
            // .HasForeignKey(x=>x.Id);

            var navigationClass = builder.Metadata.FindNavigation(nameof(Instructor.Class));

            navigationClass.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}