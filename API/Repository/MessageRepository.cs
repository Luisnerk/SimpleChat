using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.Repository;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(DataContext context) : base(context)
    {
    }
}