using FinanceDashboard.DTOs;
using FinanceDashboard.Helpers;
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
            var currentUser = RoleHelper.GetUser(HttpContext, _context);

            if (currentUser == null)
                return Unauthorized("Invalid or inactive user");

            if (!RoleHelper.IsAdmin(currentUser))
                return Unauthorized("Only Admin allowed");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                IsActive = true
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
            var role = RoleHelper.GetUser(HttpContext, _context);

            if (role == null)
                return Unauthorized("Invalid or inactive user");

            if (!RoleHelper.IsAdmin(role))
                return Unauthorized("Only Admin allowed");

            var users = _context.Users
                .Where(u => u.IsActive) 
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

        [HttpPut("update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserCreateDto dto)
        {
            var role = RoleHelper.GetUser(HttpContext, _context);

            if (role == null)
                return Unauthorized("Invalid or inactive user");

            if (!RoleHelper.IsAdmin(role))
                return Unauthorized("Only Admin allowed");

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Role = dto.Role;

            _context.SaveChanges();

            return Ok(new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeactivateUser(int id)
        {
            var role = RoleHelper.GetUser(HttpContext, _context);

            if (role == null)
                return Unauthorized("Invalid or inactive user");

            if (!RoleHelper.IsAdmin(role))
                return Unauthorized("Only Admin allowed");

            var user = _context.Users.Find(id);

            if (user == null)
                return NotFound("User not found");

            user.IsActive = false;

            _context.SaveChanges();

            return Ok("User deactivated successfully");
        }
    }
}
