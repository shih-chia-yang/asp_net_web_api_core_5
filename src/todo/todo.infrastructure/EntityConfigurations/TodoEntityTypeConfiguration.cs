using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using todo.domain.Models;

namespace todo.infrastructure.EntityConfigurations
{
    public class TodoEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.ToTable("TodoItems",TodoContext.DEFAULT_SCHEMA);
            builder.HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.Property<string>(x => x.Name)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("name")
            .IsRequired();

            builder.Property<string>(x => x.Event)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("event")
            .IsRequired();
        }
    }
}