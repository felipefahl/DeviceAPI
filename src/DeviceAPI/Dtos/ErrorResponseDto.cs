namespace DeviceAPI.Dtos
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? TraceId { get; set; }
    }
}
