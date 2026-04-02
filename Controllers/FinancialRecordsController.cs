using FinanceDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Controllers
{
    [ApiController]
    [Route("api/records")]
    public class FinancialRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FinancialRecordsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(FinancialRecord record)
        {
            if (Request.Headers["x-user-role"] != "Admin")
                return Unauthorized("Only Admin can create records");

            _context.FinancialRecords.Add(record);
            _context.SaveChanges();

            return Ok(record);
        }

        [HttpGet]
        public IActionResult GetAll(string? type, string? category)
        {
            var query = _context.FinancialRecords.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.Type == type);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(x => x.Category == category);

            return Ok(query.ToList());
        }
    }
}
