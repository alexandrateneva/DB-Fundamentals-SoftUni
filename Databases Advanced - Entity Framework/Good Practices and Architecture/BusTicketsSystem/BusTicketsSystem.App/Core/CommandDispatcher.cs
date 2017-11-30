namespace BusTicketsSystem.App.Core
{
    using System;
    using System.Linq;
    using System.Reflection;
    using BusTicketsSystem.Data;
    using BusTicketsSystem.App.Core.Commands.Contracts;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var commandName = ChangeCommandName(commandParameters[0]);
            
            var commandArgs = commandParameters.Skip(1).ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name == $"{commandName}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor.GetParameters().ToArray();

            ICommand command;

            if (constructorParameters.Count() != 0)
            {
                var context = new BusTicketSystemContext();
                command = (ICommand) constructor.Invoke(new object[] {context});
            }
            else
            {
                command = (ICommand) constructor.Invoke(new object[0]);
            }

            return command.Execute(commandArgs);
        }

        public static string FirstLetterToUpperCase(string s)
        {
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }

        public static string ChangeCommandName(string command)
        {
            var commandNameParams = command
                .Split("-")
                .Select(FirstLetterToUpperCase)
                .ToArray();

            return string.Join("", commandNameParams);
        }
    }
}
