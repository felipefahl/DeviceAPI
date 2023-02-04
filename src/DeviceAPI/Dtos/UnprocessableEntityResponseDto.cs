namespace DeviceAPI.Dtos
{
    public class UnprocessableEntityResponseDto : ErrorResponseDto
    {

        public UnprocessableEntityResponseDto(IDictionary<string, string[]> errors, string? traceId = null)
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
            TraceId = traceId;
            Errors = errors;
        }
        public IDictionary<string, string[]> Errors { get; }
    }
}
