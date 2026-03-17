
using API.Entities;

namespace API.Dto;
public class ChatCreateDto
{
    public string Name { get; set; } = null!;
    public required List<int> UsersId { get; set; }
}