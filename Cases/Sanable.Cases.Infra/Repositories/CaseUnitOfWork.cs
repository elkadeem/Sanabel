using System;
using System.Threading;
using System.Threading.Tasks;
using BusinessSolutions.Common.Core;
using BusinessSolutions.Common.EntityFramework;
using Sanable.Cases.Domain.Repositories;

namespace Sanable.Cases.Infra
{
    public class CaseUnitOfWork : BaseUnitOfWork, ICaseUnitOfWork
    {
        private readonly CaseResearchDataContext _dbContext;
        public ICaseRepository CaseRepository { get; private set; }

        public ICaseResearchRepository CaseResearchRepository { get; private set; }

        public ICaseAidRepository CaseAidRepository { get; private set; }

        public CaseUnitOfWork(CaseResearchDataContext dbContext) : base(dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");

            _dbContext = dbContext;
            CaseRepository = new CaseRepository(_dbContext);
            CaseResearchRepository = new CaseResearchRepository(_dbContext);
            CaseAidRepository = new CaseAidRepository(_dbContext);
        }        
    }
}
