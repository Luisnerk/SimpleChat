using API.Dto;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Authorize]
public class UserController : BaseApiController
{
    private IAppUserRepository _repository;
    public  UserController(IAppUserRepository repository, IMapper mapper) : base(mapper) {
        _repository = repository;
    } 

    [HttpPost("register")]
    public async Task<ActionResult<AppUserDto>> Register(AppUserRegisterDto user)
    {
        AppUser appUser = _mapper.Map<AppUser>(user);
        _repository.Add(appUser);
        await _repository.SaveAsync();

        AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
        return appUserDto;
    }

    [HttpGet("searchList")]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> SearchList(string nameParam)
    {
        IEnumerable<AppUser> users = await _repository.SearchList(nameParam);
        IEnumerable<AppUserDto> usersDto = _mapper.Map<IEnumerable<AppUserDto>>(users);
        return Ok(usersDto);
    }
}