using Microsoft.EntityFrameworkCore;

namespace NotesApp
{
    public class Context : DbContext 
    {

        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

    }
}
