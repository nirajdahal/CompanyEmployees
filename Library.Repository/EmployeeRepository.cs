using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Repository
{
    class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
