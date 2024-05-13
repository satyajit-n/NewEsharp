namespace BuildingBlock.Helper
{
    public class CustomExceptionHandler : Exception
    {
        public int StatusCode { get; }
        public string StatusMessage { get; }

        public CustomExceptionHandler(int code, string message) : base(message)
        {
            StatusCode = code;
            StatusMessage = message;
        }
    }
}
