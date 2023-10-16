﻿using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Company> GetAllCompanies(bool trackchanges) => FindAll(trackchanges)
                    .OrderBy(c => c.Name)
                    .ToList();

        public Company GetCompany(Guid companyId, bool trackchanges) => 
            FindByCondition(c => c.Id.Equals(companyId), trackchanges)
            .SingleOrDefault();
    }
}
