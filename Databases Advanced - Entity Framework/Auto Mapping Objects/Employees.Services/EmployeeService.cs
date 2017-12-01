namespace Employees.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Employees.DtoModels;
    using Employees.Models;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee = this.GetEmployeeById(employeeId);

            var employeeDto = Mapper.Map<EmployeeDto>(employee);

            return employeeDto;
        }

        public EmployeePersonalDto PersonalById(int employeeId)
        {
            var employee = this.GetEmployeeById(employeeId);

            var employeePersonalDto = Mapper.Map<EmployeePersonalDto>(employee);

            return employeePersonalDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);

            this.context.Employees.Add(employee);

            this.context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = this.GetEmployeeById(employeeId);

            employee.Birthday = date;

            this.context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public string SetAddress(int employeeId, string address)
        {
            var employee = this.GetEmployeeById(employeeId);

            employee.Address = address;

            this.context.SaveChanges();

            return $"{employee.FirstName} {employee.LastName}";
        }

        public void SetManager(int employeeId, int managerId)
        {
            var employee = this.GetEmployeeById(employeeId);

            var manager = this.GetEmployeeById(managerId);

            employee.Manager = manager;

            this.context.SaveChanges();
        }

        public ManagerDto ManagerInfo(int managerId)
        {
            var manager = this.GetEmployeeById(managerId);

            var managerDto = Mapper.Map<ManagerDto>(manager);

            return managerDto;
        }

        public IList<EmployeeManagerDto> GetEmployeesOlderThan(int age)
        {
            var employees = this.context
                .Employees
                .Include(e => e.Manager)
                .Where(e => e.Birthday != null && (DateTime.Now.Year - e.Birthday.Value.Year) > age)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeManagerDto>()
                .ToList();

            return employees;
        }

        private Employee GetEmployeeById(int employeeId)
        {
            var employee = this.context
                .Employees
                .Include(e => e.ManagedEmployees)
                .SingleOrDefault(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException($"Employee with ID {employeeId} was not found.");
            }

            return employee;
        }
    }
}
