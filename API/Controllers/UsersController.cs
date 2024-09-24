using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseAPIController
{
    private readonly DataContext _context;
    private readonly IUserRepository repository;
    private readonly IMapper mapper;

    public UsersController(DataContext context, IUserRepository _repository, IMapper _mapper)
    {
        _context = context; //Dependency Injection of DBContext class.
        repository = _repository;
        mapper = _mapper;
    }

  
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await repository.GetUsersAsync();

        var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);

        if (usersToReturn != null )
        {
            return Ok(usersToReturn);
        }
        else
        {
            return NotFound();
        }
    }


    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await repository.GetUserByUsernameAsync(username);
        var userToReturn = mapper.Map<MemberDto>(user);

        if (userToReturn == null)
        {
            return NotFound();
        }

        return userToReturn;
    }
}

