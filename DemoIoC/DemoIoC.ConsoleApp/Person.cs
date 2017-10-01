using System.Text;

namespace DemoIoC.ConsoleApp
{
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
}
