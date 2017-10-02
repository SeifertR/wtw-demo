using System.Text;

namespace OptimizationTests
{
    #region Person

    public interface IPerson
    {
        string Identify();
    }

    public class Person : IPerson
    {
        private readonly ILogger logger;
        private readonly IEmailClient emailClient;

        public Person(ILogger logger, IEmailClient emailClient)
        {
            this.logger = logger;
            this.emailClient = emailClient;
        }

        public string Identify()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Person: {GetHashCode()}");
            sb.AppendLine($"{logger.Identify("Person.", 2)}");
            sb.Append($"{emailClient.Identify("Person.", 2)}");
            return sb.ToString();
        }
    }

    #endregion Person

    #region EmailClient

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

    #endregion EmailClient

    #region EmailService

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

    #endregion EmailService

    #region Logger

    public interface ILogger
    {
        string Identify(string parent, int indent);
    }

    public class Logger : ILogger
    {
        public string Identify(string parent, int indent)
        {
            var s = string.Empty;
            for (var i = 0; i < indent; i++)
                s = s + "  ";

            return $"{s}{parent}Logger: {GetHashCode()}";
        }
    }

    #endregion Logger
}
