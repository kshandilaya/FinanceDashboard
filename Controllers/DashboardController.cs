using FinanceDashboard.DTOs;
using FinanceDashboard.Helpers;
using FinanceDashboard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
            var role = RoleHelper.GetRole(HttpContext);

            if (string.IsNullOrEmpty(role))
                return Unauthorized("Role header missing");

            if (role == "Viewer")
                return Unauthorized("Viewer cannot access dashboard");

            var records = _context.FinancialRecords.ToList();

            var totalIncome = records
                .Where(r => r.Type == "Income")
                .Sum(r => r.Amount);

            var totalExpense = records
                .Where(r => r.Type == "Expense")
                .Sum(r => r.Amount);

            var categoryBreakdown = records
                .GroupBy(r => r.Category)
                .Select(g => new CategorySummaryDto
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount)
                })
                .ToList();

            var recentTransactions = records
                .OrderByDescending(r => r.Date)
                .Take(5)
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

            var response = new DashboardResponseDto
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                NetBalance = totalIncome - totalExpense,
                CategoryBreakdown = categoryBreakdown,
                RecentTransactions = recentTransactions
            };

            return Ok(response);
        }
    }
}
