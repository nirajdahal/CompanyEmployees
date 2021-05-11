using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
               await  FindByCondition(x => ids.Contains(x.Id), trackChanges)
                .ToListAsync();


        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name).Include(c => c.Employees)
             .ToListAsync();

        }

        public async Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(companyId), trackChanges).Include(x => x.Employees).SingleOrDefaultAsync();
        }
    }
}
