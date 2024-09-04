using BookManagementBackend.Domain.Models.Responses;

namespace BookManagementBackend.Classes
{
    public class ServiceResult
    {
        public ServiceResult(bool success = true, string? message = null, bool exceptionGenerated = false)
        {
            Success = success;
            Message = message ?? string.Empty;
            ExceptionGenerated = exceptionGenerated;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public bool ExceptionGenerated { get; set; }
    }

    public class ServiceResult<R> : ServiceResult
    {
        public ServiceResult(R? resultado, bool sucesso = true, string? mensagem = null, bool exceptionGenerated = false) : base(sucesso, mensagem, exceptionGenerated)
        {
            Data = resultado;
        }

        public ServiceResult(bool sucesso = true, string? mensagem = null, bool exceptionGenerated = false) : base(sucesso, mensagem, exceptionGenerated)
        {
        }

        public R? Data { get; set; }
    }
}
