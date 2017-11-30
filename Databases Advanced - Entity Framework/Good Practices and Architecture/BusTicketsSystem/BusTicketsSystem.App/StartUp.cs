namespace BusTicketsSystem.App
{
    using BusTicketsSystem.App.Core;
    using BusTicketsSystem.Data;

    public class StartUp
    {
        public static void Main()
        {
            using (var context = new BusTicketSystemContext())
            {
                DataGenerator.ResetDatabase(context);
            }

            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }
    }
}
