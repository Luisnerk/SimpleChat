
using API.Data;
using API.Dto;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class ChatParticipantController : BaseApiController
{
    private readonly ChatParticipantRepository _repository;
    public ChatParticipantController(IMapper mapper, DataContext context) : base(mapper, context)
    {
        _repository = new ChatParticipantRepository(context);
    }

    [HttpPost("addParticipant")]
    public async Task<ActionResult> AddParticipant(ChatParticipantDto participantDto)
    {
        ChatParticipant participant = _mapper.Map<ChatParticipant>(participantDto);
        _repository.Add(participant);
        await _repository.SaveAsync();
        return Ok();
    }
}
