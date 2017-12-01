namespace Employees.App
{
    using System;
    using System.Linq;

    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            while (true)
            {
                try
                {
                    var input = Console.ReadLine();
                    var commandTokens = input.Split(' ');
                    var commandName = commandTokens[0];
                    var commandArgs = commandTokens.Skip(1).ToArray();

                    var command = CommandParser.Parse(this.serviceProvider, commandName);

                    var result = command.Execute(commandArgs);

                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
