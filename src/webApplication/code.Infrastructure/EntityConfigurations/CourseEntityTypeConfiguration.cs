using code.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code.Infrastructure.EntityConfigurations
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course", DataContext.DefaultSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property<string>(x => x.Title)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50);

            builder.Property<int>(x => x.Grade)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.HasMany(x => x.Enrollments)
            .WithOne(x => x.Course);
            // .HasForeignKey(x => x.Id);

            var navigationEnrollments = builder.Metadata.FindNavigation(nameof(Course.Enrollments));
            navigationEnrollments.SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}