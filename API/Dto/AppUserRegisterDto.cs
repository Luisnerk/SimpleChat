
namespace API.Dto;
public class AppUserRegisterDto
{
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string Password { get; set; } = null!;

}