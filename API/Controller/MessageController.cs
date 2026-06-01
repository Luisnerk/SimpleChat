using API.Data;
using API.Dto;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

public class MessageController : BaseApiController
{
    private MessageRepository _repository;
    public MessageController(IMapper mapper, DataContext context) : base(mapper, context)
    {
        _repository = new MessageRepository(context);
    }

    [HttpPost("send")]
    public async Task<ActionResult> SendMessage(MessageCreateDto messageCreateDto)
    {
        Message newMessage = _mapper.Map<Message>(messageCreateDto);
        _repository.Add(newMessage);
        await _repository.SaveAsync();
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