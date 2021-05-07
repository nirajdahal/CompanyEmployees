﻿using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
        Employee GetEmployee(Guid companyId, Guid id, bool trackChanges);

    }
}
