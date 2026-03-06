
using API.Dto;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class UserController : BaseApiController
{
    public  UserController(IMapper mapper) : base(mapper) {} 

    [HttpPost("register")]
    public ActionResult<AppUserDto> Register(AppUserRegisterDto user)
    {
        AppUser appUser = _mapper.Map<AppUser>(user);
        AppUserDto appUserDto = _mapper.Map<AppUserDto>(appUser);
        return appUserDto;
    }
}