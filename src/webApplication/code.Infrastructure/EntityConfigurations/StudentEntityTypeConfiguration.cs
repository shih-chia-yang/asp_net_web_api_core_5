using System;
using System.Security.Cryptography.X509Certificates;
using code.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code.Infrastructure.EntityConfigurations
{
    public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student", DataContext.DefaultSchema);

            builder.HasKey(s => s.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property<string>(s => s.FirstName)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property<string>(s => s.LastName)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50)
            .IsRequired();

            builder.Property<DateTime>(s => s.EnrollmentDate)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Enrollments)
            .WithOne(x => x.Student);
            //.HasForeignKey(x=>x.Id);

            var navigationEnrollments = builder.Metadata.FindNavigation(nameof(Student.Enrollments));
            navigationEnrollments.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}