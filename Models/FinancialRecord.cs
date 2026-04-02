namespace FinanceDashboard.Models
{
    public class FinancialRecord
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // Income / Expense
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public int CreatedBy { get; set; } // UserId
    }
}
