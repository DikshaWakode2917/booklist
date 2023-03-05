

namespace BooksListitingApis.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):
            base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
        }
        public DbSet<Books> BookList { get; set; }
    }
}
