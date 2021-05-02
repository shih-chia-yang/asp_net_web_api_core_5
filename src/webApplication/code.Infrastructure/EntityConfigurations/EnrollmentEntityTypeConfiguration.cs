using code.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code.Infrastructure.EntityConfigurations
{
    public class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment", DataContext.DefaultSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // builder.Property<int>(x => x.CourseId)
            // .UsePropertyAccessMode(PropertyAccessMode.Property);
            // .IsRequired();

            // builder.Property<int>(x => x.StudentId)
            // .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasOne(x => x.Student)
            .WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId);
            // .HasForeignKey<Student>("StudentId");

            var navigationStudent = builder.Metadata.FindNavigation(nameof(Enrollment.Student));
            navigationStudent.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.Course)
            .WithMany(x => x.Enrollments).HasForeignKey(x=>x.CourseId);
            // .HasForeignKey<Course>("CourseId");

            var navigationCourse = builder.Metadata.FindNavigation(nameof(Enrollment.Course));
            navigationCourse.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}