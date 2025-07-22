using System;
using FraudSys.Core.DTOs;
using FraudSys.Core.Models;

namespace FraudSys.Core.Repositories;

public interface ILimitManagerRepository
{
    Task CreateLimit(LimitModel limitModel, CancellationToken cancellationToken);
    Task<IEnumerable<LimitModel>> GetAllLimits(CancellationToken cancellationToken);
    Task<LimitModel?> GetLimitById(Guid id, CancellationToken cancellationToken);
    Task UpdateLimit(LimitModel limitModel, CancellationToken cancellationToken);
    Task DeleteLimit(LimitModel limitModel, CancellationToken cancellationToken);
}
