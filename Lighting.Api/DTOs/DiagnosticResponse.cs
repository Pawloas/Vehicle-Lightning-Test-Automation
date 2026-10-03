namespace Lighting.Api.DTOs
{
    public class DiagnosticResponse
    {
        public string Code { get; init; }
        public string Severity { get; init; }
        //public string Parameter { get; init; }
        public string Message { get; init; }
        public string TimeStamp { get; init; }
    }
}
