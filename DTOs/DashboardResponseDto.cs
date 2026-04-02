namespace FinanceDashboard.DTOs
{
    public class DashboardResponseDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetBalance { get; set; }

        public List<CategorySummaryDto> CategoryBreakdown { get; set; }
        public List<FinancialRecordResponseDto> RecentTransactions { get; set; }
    }

    public class CategorySummaryDto
    {
        public string Category { get; set; }
        public decimal Total { get; set; }
    }
}
