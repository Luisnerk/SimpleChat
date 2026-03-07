using API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMapper _mapper;
    protected readonly DataContext _context;

    public BaseApiController(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
}