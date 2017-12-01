namespace Employees.DtoModels
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> ManagedEmployees { get; set; } = new HashSet<EmployeeDto>();

        public int ManagedEmployeesCount => this.ManagedEmployees.Count;
    }
}
