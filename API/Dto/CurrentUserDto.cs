
namespace API.Dto;
public class CurrentUserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Token { get; set; } = null!;
}