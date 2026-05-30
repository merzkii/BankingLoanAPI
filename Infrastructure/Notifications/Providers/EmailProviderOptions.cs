namespace Infrastructure.Notifications.Providers
{
    public class EmailProviderOptions
    {
        public string FromAddress { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}
