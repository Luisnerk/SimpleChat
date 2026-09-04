using System.Collections.Frozen;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using API.Dto;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Authorize]
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

        chatDto.UserIds.Add(User.GetUserId());

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
    public async Task<IEnumerable<ChatListDto>> GetChatList()
    {
        var userId = User.GetUserId();
        IEnumerable<Chat> chatList = await _repository.GetUserChats(userId);
        IEnumerable<ChatListDto> chatListDto = _mapper.Map<IEnumerable<ChatListDto>>(chatList);
        return chatListDto;
    }
}