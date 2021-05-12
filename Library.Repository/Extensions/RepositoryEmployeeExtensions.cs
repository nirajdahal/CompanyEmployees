using Library.Entities.Models;
using Library.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
        employees.Where(e => (e.Age >= minAge && e.Age <= maxAge));
        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return employees;
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }
        public static IQueryable<Employee> Pagination(this IQueryable<Employee> employees, EmployeeParameters employeeParameters)
        {
           
            return employees
                .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
                .Take(employeeParameters.PageSize);
        }
    }
}
