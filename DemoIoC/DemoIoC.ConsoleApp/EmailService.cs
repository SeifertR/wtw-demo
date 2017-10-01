using System.Text;

namespace DemoIoC.ConsoleApp
{
    public interface IEmailService
    {
        string Identify(string parent, int indent);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger logger;

        public EmailService(ILogger logger)
        {
            this.logger = logger;
        }

        public string Identify(string parent, int indent)
        {
            var s = string.Empty;
            for (var i = 0; i < indent; i++)
                s = s + "  ";

            var sb = new StringBuilder();
            sb.AppendLine($"{s}{parent}EmailService: {GetHashCode()}");
            sb.AppendLine($"{logger.Identify(parent + "EmailService.", indent + 2)}");
            return sb.ToString();
        }
    }
}
