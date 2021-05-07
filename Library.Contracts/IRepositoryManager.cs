using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        void Save();
    }

}
