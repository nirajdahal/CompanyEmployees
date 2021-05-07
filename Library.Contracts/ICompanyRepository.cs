using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Contracts
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges);
    }
}
 