using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
                FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToList();


        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(c => c.Name).Include(c => c.Employees)
             .ToList();

        }

        public Company GetCompany(Guid companyId, bool trackChanges)
        {
            return FindByCondition(x => x.Id.Equals(companyId), trackChanges).Include(x => x.Employees).SingleOrDefault();
        }
    }
}
