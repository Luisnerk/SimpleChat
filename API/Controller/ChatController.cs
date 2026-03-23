
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class ChatController : BaseApiController
{
    private ChatRepository _repository;
    public ChatController(IMapper mapper, DataContext context) : base(mapper, context)
    {
        _repository = new ChatRepository(context);
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateChat (ChatCreateDto chatDto)
    {
        Chat newChat = new Chat{
            Name = chatDto.Name,
            Participants = chatDto.UserIds.Select(uid => new ChatParticipant
            {
                AppUserId = uid
            }).ToList()
        };

        _repository.Add(newChat);
        await _repository.SaveAsync();

        return Ok();
    }
}