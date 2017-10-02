using OptimizationTests.IoC.Containers;
using System;
using System.Diagnostics;
using System.Net;

namespace OptimizationTests
{
    class Program
    {
        private static readonly int count = 10000;

        static void Main(string[] args)
        {
            long createDefault = 0;
            long createParam = 0;
            long createPerson = 0;
            long invokeDefault = 0;
            long invokeParam = 0;
            long invokePerson = 0;
            long cachedDefault = 0;
            long cachedParam = 0;
            long cachedPerson = 0;
            var sw = new Stopwatch();

            Console.WriteLine("Ready. Press any key to begin...");
            Console.ReadLine();

            Console.WriteLine("Create Default");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CreateDefault();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                createDefault += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Create Parameterized");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CreateParam();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                createParam += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Create Person");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CreatePerson();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                createPerson += sw.ElapsedMilliseconds;
            }
            Console.WriteLine();

            Console.WriteLine("Invoke Default");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                InvokeDefault();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                invokeDefault += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Invoke Parameterized");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                InvokeParam();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                invokeParam += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Invoke Person");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                InvokePerson();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                invokePerson += sw.ElapsedMilliseconds;
            }

            Console.WriteLine();

            Console.WriteLine("Cached Default");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CachedDefault();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                cachedDefault += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Cached Parameterized");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CachedParam();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                cachedParam += sw.ElapsedMilliseconds;
            }

            Console.WriteLine("Cached Person");
            for (var n = 0; n < 10; n++)
            {
                sw.Reset();
                sw.Start();
                CachedPerson();
                sw.Stop();
                Console.WriteLine($"Run time: {sw.ElapsedMilliseconds} ms");
                cachedPerson += sw.ElapsedMilliseconds;
            }

            Console.Write("----------------------------------\n\n");
            Console.WriteLine($"Create Default Total/Average: {createDefault} / {createDefault/10}");
            Console.WriteLine($"Create Paramed Total/Average: {createParam} / {createParam/10}");
            Console.WriteLine($"Create Person  Total/Average: {createPerson} / {createPerson/10}\n");
            Console.WriteLine($"Invoke Default Total/Average: {invokeDefault} / {invokeDefault/10}");
            Console.WriteLine($"Invoke Paramed Total/Average: {invokeParam} / {invokeParam/10}");
            Console.WriteLine($"Invoke Person  Total/Average: {invokePerson} / {invokePerson/10}\n");
            Console.WriteLine($"Cached Default Total/Average: {cachedDefault} / {cachedDefault / 10}");
            Console.WriteLine($"Cached Paramed Total/Average: {cachedParam} / {cachedParam / 10}");
            Console.WriteLine($"Cached Person  Total/Average: {cachedPerson} / {cachedPerson / 10}");

            Console.WriteLine();
            Console.WriteLine("Test complete. Press any key to exit...");
            Console.ReadLine();
        }

        static void CreateDefault()
        {
            var container = new CreateContainer();
            container.Register<ILogger, Logger>();
            for (var n = 0; n < count; n++)
                container.Resolve<ILogger>();
        }

        static void CreateParam()
        {
            var container = new CreateContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            for (var n = 0; n < count; n++)
                container.Resolve<IEmailClient>();
        }

        static void CreatePerson()
        {
            var container = new CreateContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            container.Register<IPerson, Person>();

            for(var n = 0; n < count; n++)
                container.Resolve<IPerson>();
        }

        static void InvokeDefault()
        {
            var container = new InvokeContainer();
            container.Register<ILogger, Logger>();
            for (var n = 0; n < count; n++)
                container.Resolve<ILogger>();
        }

        static void InvokeParam()
        {
            var container = new InvokeContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            for (var n = 0; n < count; n++)
                container.Resolve<IEmailClient>();
        }

        static void InvokePerson()
        {
            var container = new InvokeContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            container.Register<IPerson, Person>();

            for (var n = 0; n < count; n++)
                container.Resolve<IPerson>();
        }

        static void TestHybrid()
        {
            Console.WriteLine("Testing with hybrid container");
            var container = new HybridContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            container.Register<IPerson, Person>();

            for (var n = 0; n < count; n++)
                container.Resolve<IPerson>();
        }

        static void CachedDefault()
        {
            var container = new CachedContainer();
            container.Register<ILogger, Logger>();
            for (var n = 0; n < count; n++)
                container.Resolve<ILogger>();
        }

        static void CachedParam()
        {
            var container = new CachedContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            for (var n = 0; n < count; n++)
                container.Resolve<IEmailClient>();
        }

        static void CachedPerson()
        {
            var container = new CachedContainer();
            container.Register<ILogger, Logger>();
            container.Register<IEmailService, EmailService>();
            container.Register<IEmailClient, EmailClient>();
            container.Register<IPerson, Person>();

            for (var n = 0; n < count; n++)
                container.Resolve<IPerson>();
        }
    }
}
