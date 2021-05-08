using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public Employee GetEmployee(Guid companyId, Guid id, bool trackChanges) => 
            FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges).SingleOrDefault();

        public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
         FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
         .OrderBy(e => e.Name);
    }
}
