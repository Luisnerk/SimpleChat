using API.Dto;
using API.Entities;
using API.Hubs;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controller;

public class MessageController : BaseApiController
{
    private IMessageRepository _repository;
    private IChatParticipantRepository _participantRepository;
    private readonly IHubContext<ChatHub> _hubContext;
    public MessageController(IMessageRepository repository, IChatParticipantRepository participantRepository, IMapper mapper, IHubContext<ChatHub> hubContext) : base(mapper)
    {
        _repository = repository;
        _participantRepository = participantRepository;
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<ActionResult> SendMessage(MessageCreateDto messageCreateDto)
    {
        try
        {   
            var participant = await _participantRepository.GetChatParticipant(messageCreateDto.ChatId, messageCreateDto.UserId);
            Message newMessage = _mapper.Map<Message>(messageCreateDto);
            _repository.Add(newMessage);
            await _repository.SaveAsync();
            await _hubContext.Clients.Group(participant!.ChatId.ToString()).SendAsync("NewMessage", newMessage);
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest();
        }
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