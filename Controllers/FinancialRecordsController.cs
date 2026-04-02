using FinanceDashboard.DTOs;
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

        // ✅ CREATE
        [HttpPost]
        public IActionResult Create(FinancialRecordCreateDto dto)
        {
            if (Request.Headers["x-user-role"] != "Admin")
                return Unauthorized("Only Admin can create records");

            if (dto.Amount <= 0)
                return BadRequest("Amount must be greater than 0");

            var record = new FinancialRecord
            {
                Amount = dto.Amount,
                Type = dto.Type,
                Category = dto.Category,
                Date = dto.Date,
                Notes = dto.Notes,
                CreatedBy = dto.CreatedBy
            };

            _context.FinancialRecords.Add(record);
            _context.SaveChanges();

            return Ok(record);
        }

        // ✅ GET ALL (with filters)
        [HttpGet]
        public IActionResult GetAll(string? type, string? category, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.FinancialRecords.AsQueryable();

            if (!string.IsNullOrEmpty(type))
                query = query.Where(x => x.Type == type);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(x => x.Category == category);

            if (startDate.HasValue)
                query = query.Where(x => x.Date >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.Date <= endDate.Value);

            var result = query
                .Select(r => new FinancialRecordResponseDto
                {
                    Id = r.Id,
                    Amount = r.Amount,
                    Type = r.Type,
                    Category = r.Category,
                    Date = r.Date,
                    Notes = r.Notes
                })
                .ToList();

            return Ok(result);
        }

        // ✅ UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(int id, FinancialRecordCreateDto dto)
        {
            if (Request.Headers["x-user-role"] != "Admin")
                return Unauthorized("Only Admin can update records");

            var record = _context.FinancialRecords.Find(id);

            if (record == null)
                return NotFound("Record not found");

            record.Amount = dto.Amount;
            record.Type = dto.Type;
            record.Category = dto.Category;
            record.Date = dto.Date;
            record.Notes = dto.Notes;

            _context.SaveChanges();

            return Ok(record);
        }

        // ✅ DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (Request.Headers["x-user-role"] != "Admin")
                return Unauthorized("Only Admin can delete records");

            var record = _context.FinancialRecords.Find(id);

            if (record == null)
                return NotFound("Record not found");

            _context.FinancialRecords.Remove(record);
            _context.SaveChanges();

            return Ok("Deleted successfully");
        }
    }
}
