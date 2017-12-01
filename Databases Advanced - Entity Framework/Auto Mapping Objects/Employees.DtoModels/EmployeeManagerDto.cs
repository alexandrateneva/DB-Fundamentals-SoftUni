namespace Employees.DtoModels
{
    public class EmployeeManagerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }
    }
}
