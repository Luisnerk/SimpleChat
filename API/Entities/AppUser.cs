namespace API.Entities;
public class AppUser : BaseEntity
{
    public string Name { get; set; }
    public DateOnly BirthDate { get; set; }

}