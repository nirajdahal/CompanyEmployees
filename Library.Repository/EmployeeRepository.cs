﻿using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using Library.Entities.RequestFeatures;
using Library.Repository.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) => 
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges) =>
         await FindByCondition(e => e.CompanyId.Equals(companyId),trackChanges)
            .FilterEmployees(employeeParameters.MinAge, employeeParameters.MaxAge)
            .Search(employeeParameters.SearchTerm)
            .Pagination(employeeParameters)
            .OrderBy(e => e.Name)
            .ToListAsync();


    }
}
