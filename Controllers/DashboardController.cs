using FinanceDashboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceDashboard.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var records = _context.FinancialRecords.ToList();

            var totalIncome = records
                .Where(r => r.Type == "Income")
                .Sum(r => r.Amount);

            var totalExpense = records
                .Where(r => r.Type == "Expense")
                .Sum(r => r.Amount);

            var categoryData = records
                .GroupBy(r => r.Category)
                .Select(g => new {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount)
                });

            return Ok(new
            {
                totalIncome,
                totalExpense,
                netBalance = totalIncome - totalExpense,
                categoryData,
                recent = records.OrderByDescending(r => r.Date).Take(5)
            });
        }
    }
}
