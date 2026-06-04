using API.Data;
using API.Dto;
using API.Entities;
using API.Hubs;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controller;

public class MessageController : BaseApiController
{
    private MessageRepository _repository;
    private readonly IHubContext<ChatHub> _hubContext;
    public MessageController(IMapper mapper, DataContext context, IHubContext<ChatHub> hubContext) : base(mapper, context)
    {
        _repository = new MessageRepository(context);
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<ActionResult> SendMessage(MessageCreateDto messageCreateDto)
    {
        Message newMessage = _mapper.Map<Message>(messageCreateDto);
        _repository.Add(newMessage);
        await _repository.SaveAsync();
        await _hubContext.Clients.Group(newMessage.ChatParticipant.ChatId.ToString()).SendAsync("NewMessage", newMessage);
        return Ok();
    }

    [HttpGet("getMessages")]
    public async Task<ActionResult<IEnumerable<Message>>> GetChatMessages(int chatId)
    {
        IEnumerable<Message> messages = await _repository.GetMessagesAsync(chatId);
        var messagesDto = _mapper.Map<IEnumerable<MessageListDto>>(messages);
        return Ok(messagesDto);
    }
}