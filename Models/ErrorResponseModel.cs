using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VethubLanding.Models
{
    public class ErrorResponseModel
    {
        public int StatusCode { get; set; }

        public List<ErrorResponse> Errors { get; set; }

        public ErrorResponseModel()
        {
        }
        public ErrorResponseModel(ModelStateDictionary modelState)
        {
            StatusCode = 400;
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ErrorResponse(key, x.ErrorMessage, null)))
                    .ToList();
        }
    }
    public class ErrorResponse
    {
        public string Field { get; set; }

        public string Message { get; set; }
        public Exception Detail { get; set; }
        public ErrorResponse()
        {
        }
        public ErrorResponse(string field, string message, Exception? detail)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
            Detail = detail;
        }
    }
}
