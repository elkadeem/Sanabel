using BusinessSolutions.Common.Core;

namespace Sanable.Cases.Domain.Repositories
{
    public interface ICaseUnitOfWork : IUnitOfWork
    {
        ICaseRepository CaseRepository { get; }

        ICaseResearchRepository CaseResearchRepository { get; }

        ICaseAidRepository CaseAidRepository { get; }
    }
}
