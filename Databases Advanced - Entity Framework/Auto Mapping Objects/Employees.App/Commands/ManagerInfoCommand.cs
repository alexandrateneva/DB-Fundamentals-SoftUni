namespace Employees.App.Commands
{
    using System.Text;
    using Employees.App.Commands.Contracts;
    using Employees.Services;

    public class ManagerInfoCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var managerId = int.Parse(args[0]);

            var manager = this.employeeService.ManagerInfo(managerId);

            var sb = new StringBuilder();

            sb.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.ManagedEmployeesCount}");

            foreach (var employee in manager.ManagedEmployees)
            {
                sb.AppendLine($"    - {employee.FirstName} {employee.LastName} - ${employee.Salary:F2}");

            }

            return sb.ToString().Trim();
        }
    }
}
