namespace SetmoreSharp
{
    public class ClientCredentials
    {
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string AuthCode { get; set; } = string.Empty;
        public string RefreshToken { get; set; }
    }
}