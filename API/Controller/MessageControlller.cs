using API.Data;
using API.Dto;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

public class MessageControlller : BaseApiController
{
    private MessageRepository _repository;
    public MessageControlller(IMapper mapper, DataContext context) : base(mapper, context)
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
}