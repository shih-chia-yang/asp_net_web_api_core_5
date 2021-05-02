using System.Threading;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Kernal;
using code.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace code.Infrastructure
{
    public class DataContext:DbContext,IUnitOfWork
    {
        public const string DefaultSchema="Data";

        public DbSet<Course> Courses{ get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken=default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollmentEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InstructorEntityTypeConfiguration());
        }
    }

    public class DataContextDesignFactory:IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(@"Data Source=127.0.0.1;Initial Catalog=Code.Service.Data;Persist Security Info=True;User ID=SA;password=qwer%TGB;");
            return new DataContext(optionsBuilder.Options);
        }
    }
}