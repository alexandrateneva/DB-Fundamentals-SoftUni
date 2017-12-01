namespace Employees.App.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using Employees.App.Commands.Contracts;
    using Employees.Services;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var age = int.Parse(args[0]);

            var employees = this.employeeService.GetEmployeesOlderThan(age);

            if (!employees.Any())
            {
                throw new ArgumentException($"There aren't employees older than {age}.");
            }

            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                var managerName = "[no manager]";
                if (employee.ManagerFirstName != null)
                {
                    managerName = employee.ManagerFirstName + " " + employee.ManagerLastName;
                }

                sb.AppendLine($"{employee.FirstName} {employee.LastName} - ${employee.Salary:F2} - Manager: {managerName}");
            }

            return sb.ToString().Trim();
        }
    }
}
