
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using API.Dto;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;
public class AccountController : BaseApiController
{
    private IAppUserRepository _repository;
    private ITokenService _tokenService;
    public AccountController(IMapper mapper, IAppUserRepository repository, ITokenService tokenService) : base(mapper)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<CurrentUserDto>> Register(AppUserRegisterDto registerDto)
    {
        AppUser? userExists =  await _repository.GetUser(registerDto.Username);
        if (userExists != null)
        {
            return BadRequest("Usuario ya existente");
        }
        
        using var hmac = new HMACSHA512();

        AppUser newUser = _mapper.Map<AppUser>(registerDto);
        newUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
        newUser.PasswordSalt = hmac.Key;

        _repository.Add(newUser);
        await _repository.SaveAsync();

        return Ok(new CurrentUserDto
        {
            Name = newUser.Name,
            Username = newUser.Username,
            Token = _tokenService.CreateToken(newUser)
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<CurrentUserDto>> Login(LoginDto loginData)
    {
        AppUser? user = await _repository.GetUser(loginData.Username);
        if (user == null)
        {
            return Unauthorized("Usuario o contraseña incorrectos");
        }
        if (!CheckPassword(loginData.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Unauthorized("Usuario o contraseña incorrectos");
        }

        return Ok(new CurrentUserDto
        {
            Name = user.Name,
            Username = user.Username,
            Token = _tokenService.CreateToken(user)
        });
    }

    private bool CheckPassword(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != hash[i])
            {
                return false;
            }
        }
        
        return true;
    }
}
