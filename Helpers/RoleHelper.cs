namespace FinanceDashboard.Helpers
{
    public static class RoleHelper
    {
        public static string GetRole(HttpContext context)
        {
            return context.Request.Headers["x-user-role"].ToString();
        }

        public static bool IsAdmin(HttpContext context)
        {
            return GetRole(context) == "Admin";
        }

        public static bool IsAnalyst(HttpContext context)
        {
            return GetRole(context) == "Analyst";
        }

        public static bool IsViewer(HttpContext context)
        {
            return GetRole(context) == "Viewer";
        }
    }
}
