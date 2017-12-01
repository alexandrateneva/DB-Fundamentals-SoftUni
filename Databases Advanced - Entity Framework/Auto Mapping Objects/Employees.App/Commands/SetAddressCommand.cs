namespace Employees.App.Commands
{
    using System.Linq;
    using Employees.App.Commands.Contracts;
    using Employees.Services;

    public class SetAddressCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetAddressCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var address = string.Join(" ", args.Skip(1));

            var employeeName = this.employeeService.SetAddress(employeeId, address);

            return $"{employeeName}'s address was set to {address}.";
        }
    }
}
