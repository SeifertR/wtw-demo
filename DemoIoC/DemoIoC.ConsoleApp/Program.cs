using System;

namespace DemoIoC.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Verify that all singletons share a single instance " +
                              "\nand that all transients have a unique instance.\n");

            var container = new Container();
            container.Register<ILogger, Logger>(LifeCycle.Singleton);
            Console.WriteLine("Logger registered as singleton");

            container.Register<IEmailService, EmailService>(LifeCycle.Singleton);
            Console.WriteLine("EmailService registered as singleton");

            container.Register<IEmailClient, EmailClient>(LifeCycle.Transient);
            Console.WriteLine("EmailClient registered as transient");

            container.Register<IPerson, Person>();
            Console.WriteLine("Person registered as transient");

            Console.WriteLine();

            var logger = container.Resolve<ILogger>();
            var emailService = container.Resolve<IEmailService>();
            var emailClient = container.Resolve<IEmailClient>();
            var p1 = container.Resolve<IPerson>();
            var p2 = container.Resolve<IPerson>();
            var p3 = container.Resolve<IPerson>();

            Console.WriteLine(logger.Identify("", 0));
            Console.WriteLine();

            Console.WriteLine(emailService.Identify("", 0));

            Console.WriteLine(emailClient.Identify("", 0));

            Console.WriteLine("Person 1\n" + p1.Identify());

            Console.WriteLine("Person 2\n" + p2.Identify());

            Console.WriteLine("Person 3\n" + p3.Identify());

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}
