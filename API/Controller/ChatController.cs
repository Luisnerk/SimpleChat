using API.Dto;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class ChatController : BaseApiController
{
    private IChatRepository _repository;
    public ChatController(IChatRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreateChat (ChatCreateDto chatDto)
    {
        if (String.IsNullOrEmpty(chatDto.Name) || chatDto.UserIds.Count() < 1)
        {
            return BadRequest();
        }
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

    [HttpGet("getChatList")]
    public async Task<IEnumerable<ChatListDto>> GetChatList(int userId)
    {
        IEnumerable<Chat> chatList = await _repository.GetUserChats(userId);
        IEnumerable<ChatListDto> chatListDto = _mapper.Map<IEnumerable<ChatListDto>>(chatList);
        return chatListDto;
    }
}