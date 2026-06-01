using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto;
public class MessageListDto
{
    public int MessageId { get; set; }
    public string Content { get; set; }
    public DateTime DateTimeSent { get; set; }
    public string SenderName { get; set; }
}