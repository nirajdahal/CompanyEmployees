using Library.Contracts;
using Library.Entities;
using Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
    }
}
