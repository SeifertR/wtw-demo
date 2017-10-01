namespace DemoIoC.ConsoleApp
{
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
}
