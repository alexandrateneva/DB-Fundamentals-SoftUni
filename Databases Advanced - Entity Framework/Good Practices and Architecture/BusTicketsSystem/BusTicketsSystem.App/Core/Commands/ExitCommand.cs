namespace BusTicketsSystem.App.Core.Commands
{
    using System;
    using BusTicketsSystem.App.Core.Commands.Contracts;

    public class ExitCommand : ICommand
    {
        public string Execute(string[] data)
        {
            Environment.Exit(0);

            return null;
        }
    }
}
