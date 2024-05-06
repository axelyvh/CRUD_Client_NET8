namespace Application.DTOs.ErrorsDto
{
    public class CodeErrorExceptionDto : CodeErrorResponseDto
    {
        public string? Details { get; set; }
        public object Data { get; set; }
        public CodeErrorExceptionDto(int statusCode, string? message = null, string? details = null, object data = null) : base(statusCode, message)
        {
            Details = details;
            Data = data;
        }
    }
}
