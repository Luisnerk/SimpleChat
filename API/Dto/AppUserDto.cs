
namespace API.Dto;
public class AppUserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }
    public int Age { get; set; }
}