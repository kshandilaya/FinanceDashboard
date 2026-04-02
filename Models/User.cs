namespace FinanceDashboard.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // keep simple (no hashing for now)
        public string Role { get; set; } // Admin, Analyst, Viewer
        public bool IsActive { get; set; } = true;
    }
}
