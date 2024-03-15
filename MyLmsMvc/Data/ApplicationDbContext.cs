using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLmsMvc.Models;

namespace MyLmsMvc.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MyLmsMvc.Models.Book> Book { get; set; } = default!;
        public DbSet<MyLmsMvc.Models.Reader> Reader { get; set; } = default!;
        public DbSet<MyLmsMvc.Models.Borrowing> Borrowing { get; set; } = default!;
    }
}
