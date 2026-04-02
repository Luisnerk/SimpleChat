
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Chat> Chat { get; set; }
    public DbSet<ChatParticipant> ChatParticipant { get; set; }
    public DbSet<Message> Messages { get; set; }
}