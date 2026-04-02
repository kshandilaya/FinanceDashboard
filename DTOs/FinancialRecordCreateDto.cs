namespace FinanceDashboard.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class FinancialRecordCreateDto
    {
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public string Type { get; set; }

        public string Category { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public int CreatedBy { get; set; }
    }
}
