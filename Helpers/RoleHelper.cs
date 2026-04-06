using FinanceDashboard.Models;

namespace FinanceDashboard.Helpers
{
    public static class RoleHelper
    {
        public static User? GetUser(HttpContext context, AppDbContext db)
        {
            var userIdHeader = context.Request.Headers["x-user-id"].FirstOrDefault();

            if (string.IsNullOrEmpty(userIdHeader))
                return null;

            if (!int.TryParse(userIdHeader, out int userId))
                return null;

            return db.Users.FirstOrDefault(u => u.Id == userId && u.IsActive);
        }

        public static bool IsAdmin(User user)
        {
            return user.Role == "Admin";
        }

        public static bool IsAnalyst(User user)
        {
            return user.Role == "Analyst";
        }

        public static bool IsViewer(User user)
        {
            return user.Role == "Viewer";
        }
    }
}