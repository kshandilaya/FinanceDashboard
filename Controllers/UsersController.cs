using FinanceDashboard.DTOs;
using FinanceDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateUser(UserCreateDto dto)
        {
            if (Request.Headers["x-user-role"] != "Admin")
                return Unauthorized("Only Admin can create users");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToList();

            return Ok(users);
        }
    }
}
