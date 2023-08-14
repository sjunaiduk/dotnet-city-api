namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string _mailTo = "test@gmail.com";
        private readonly string _mailFrom = "from@gmail.com";
        private readonly string _customValue = String.Empty;
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"] ?? _mailTo;
            _mailFrom = configuration["mailSettings:mailFromAddress"] ?? _mailFrom;
            _customValue = configuration["country"] ?? "not set.";

        }
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Custom JSON config file value: {_customValue}");
            Console.WriteLine($"Mail from {_mailFrom} , sending to {_mailTo} with {nameof(LocalMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
