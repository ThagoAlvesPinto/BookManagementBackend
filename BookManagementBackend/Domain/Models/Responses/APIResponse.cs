using BookManagementBackend.Classes;
using System.Text.Json.Serialization;

namespace BookManagementBackend.Domain.Models.Responses
{
    public class APIResponse
    {
        public APIResponse(bool success, string? message = null)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? Message { get; set; }



        public static implicit operator APIResponse(ServiceResult serviceResult)
        {
            return new(serviceResult.Success, serviceResult.Message);
        }
    }

    public class APIResponse<R> : APIResponse
    {
        public APIResponse(R? data, bool success = true, string? message = null) : base(success, message)
        {
            Data = data;
        }

        public APIResponse(bool success = true, string? message = null) : base(success, message)
        {
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public R? Data { get; set; }

        public static implicit operator APIResponse<R>(ServiceResult<R> serviceResult)
        {
            return new(serviceResult.Data, serviceResult.Success, serviceResult.Message);
        }
    }
}
