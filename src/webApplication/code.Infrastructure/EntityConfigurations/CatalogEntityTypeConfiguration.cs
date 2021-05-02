using System.Numerics;
using code.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace code.Infrastructure.EntityConfigurations
{
    public class CatalogEntityTypeConfiguration : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable("Catalog", DataContext.DefaultSchema);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.InstructorId)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.Instructor)
            .WithMany(x => x.Class);

            var navigationInstructor = builder.Metadata.FindNavigation(nameof(Catalog.Instructor));
            navigationInstructor.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.Course);

            builder.Property(x => x.CourseId)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

            var navigationCourse = builder.Metadata.FindNavigation(nameof(Catalog.Course));
            navigationCourse.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasMaxLength(50);

            builder.Property(x => x.Tuition)
            .UsePropertyAccessMode(PropertyAccessMode.Property)
            .HasColumnType("money");

            builder.Property(x => x.StartDate)
            .UsePropertyAccessMode(PropertyAccessMode.Property);

            builder.Property(x => x.RowVersion)
            .IsConcurrencyToken()
            .ValueGeneratedOnAddOrUpdate();
        }
    }
}