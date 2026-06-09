using API.Dto;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class ChatParticipantController : BaseApiController
{
    private readonly IChatParticipantRepository _repository;
    public ChatParticipantController(IChatParticipantRepository repository, IMapper mapper) : base(mapper)
    {
        _repository = repository;
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
