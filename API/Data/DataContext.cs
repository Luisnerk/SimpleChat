
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>()
                        .HasIndex(x => x.Username)
                        .IsUnique();
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Chat> Chat { get; set; }
    public DbSet<ChatParticipant> ChatParticipant { get; set; }
    public DbSet<Message> Messages { get; set; }
}