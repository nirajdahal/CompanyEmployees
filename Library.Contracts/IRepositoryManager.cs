using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        Task SaveAsync();

    }

}
