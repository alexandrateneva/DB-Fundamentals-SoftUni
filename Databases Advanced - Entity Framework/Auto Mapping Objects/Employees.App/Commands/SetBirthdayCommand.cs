namespace Employees.App.Commands
{
    using System;
    using Employees.App.Commands.Contracts;
    using Employees.Services;

    public class SetBirthdayCommand : ICommand
    {
        private readonly EmployeeService employeeService;

        public SetBirthdayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);

            var employeeName = this.employeeService.SetBirthday(employeeId, date);

            return $"{employeeName}'s birthday was set to {args[1]}.";
        }
    }
}
