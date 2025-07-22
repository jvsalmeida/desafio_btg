using System;
using FraudSys.Core.DTOs;
using FraudSys.Core.Models;

namespace FraudSys.Core.Services;

public interface ILimitManagerService
{
    Task<LimitModel> CreateLimit(CreateLimitDto data, CancellationToken cancellationToken);
    Task<IEnumerable<LimitModel>> GetLimits(CancellationToken cancellationToken);
    Task<LimitModel?> GetLimitById(Guid id, CancellationToken cancellationToken);
    Task<LimitModel?> UpdateLimit(UpdateLimitDto data, CancellationToken cancellationToken);
    Task<bool> DeleteLimit(Guid id, CancellationToken cancellationToken);
}
