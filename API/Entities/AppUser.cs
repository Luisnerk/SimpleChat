namespace API.Entities;
public class AppUser : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;

}