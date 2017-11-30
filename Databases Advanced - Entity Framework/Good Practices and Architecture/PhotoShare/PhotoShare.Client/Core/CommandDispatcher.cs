namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using Data;
    using System.Reflection;
    using PhotoShare.Client.Core.Commands.Attributes;
    using PhotoShare.Client.Core.Commands.Contracts;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var commandName = commandParameters[0];

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
                var context = new PhotoShareContext();
                command = (ICommand)constructor.Invoke(new object[] { context });
            }
            else
            {
                command = (ICommand)constructor.Invoke(new object[0]);
            }

            var logAttribute = command.GetType().GetCustomAttribute<LogStateAttribute>();
            if (logAttribute != null && logAttribute.NecessaryLogState != Session.GetLogStateOfUser())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            return command.Execute(commandArgs);
        }
    }
}
