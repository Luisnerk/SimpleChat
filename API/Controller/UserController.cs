
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Entities;
using API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class UserController : BaseApiController
{
    private AppUserRepository _repository;
    public  UserController(IMapper mapper, DataContext context) : base(mapper, context) {
        _repository = new AppUserRepository(_context);
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
}