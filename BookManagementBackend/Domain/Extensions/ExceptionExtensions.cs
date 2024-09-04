namespace BookManagementBackend.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetExceptionMessage(this Exception ex)
        {
            string message = ex.Message;

            if (ex.InnerException is not null)
                message += $"\n Inner - {ex.InnerException.GetExceptionMessage()}";

            return message;
        }
    }
}
