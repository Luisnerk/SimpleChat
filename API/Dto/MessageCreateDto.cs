using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto;
public class MessageCreateDto
{
    public string Content { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
}