namespace BuildingBlock
{
    public class APIResponseDto
    {
        public bool isSuccess { get; set; } = true;
        public string? displayMessage { get; set; }
        public dynamic? responseBody { get; set; }
        public dynamic? supportMessage { get; set; }
    }
}
