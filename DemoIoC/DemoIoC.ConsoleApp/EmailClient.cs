using System.Text;

namespace DemoIoC.ConsoleApp
{
    public interface IEmailClient
    {
        string Identify(string parent, int indent);
    }

    public class EmailClient : IEmailClient
    {
        private readonly ILogger logger;
        private readonly IEmailService emailService;

        public EmailClient(ILogger logger, IEmailService emailService)
        {
            this.logger = logger;
            this.emailService = emailService;
        }

        public string Identify(string parent, int indent)
        {
            var s = string.Empty;
            for (var i = 0; i < indent; i++)
                s = s + "  ";

            var sb = new StringBuilder();
            sb.AppendLine($"{s}{parent}EmailClient: {GetHashCode()}");
            sb.AppendLine($"{logger.Identify(parent + "EmailClient.", indent + 2)}");
            sb.AppendLine($"{emailService.Identify(parent + "EmailClient.", indent + 2)}");
            return sb.ToString();
        }
    }
}
