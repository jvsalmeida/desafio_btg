using System.Reflection.Metadata;
using Amazon.DynamoDBv2.Model;
using FraudSys.Core.DTOs;
using FraudSys.Core.Models;
using FraudSys.Core.Repositories;

namespace FraudSys.Core.Services;

public class LimitManagerService : ILimitManagerService
{
    private readonly ILimitManagerRepository _limitManagerRepository;

    public LimitManagerService(ILimitManagerRepository limitManagerRepository)
    {
        _limitManagerRepository = limitManagerRepository;
    }

    public async Task<LimitModel> CreateLimit(CreateLimitDto data, CancellationToken cancellationToken)
    {
        var limit = new LimitModel(
            document: data.Document,
            agency: data.Agency,
            account: data.Account,
            limit: data.Limit
        );

        await _limitManagerRepository.CreateLimit(limit, cancellationToken);

        return limit;
    }

    public async Task<IEnumerable<LimitModel>> GetLimits(CancellationToken cancellationToken)
    {
        return await _limitManagerRepository.GetAllLimits(cancellationToken);
    }

    public async Task<LimitModel?> GetLimitById(Guid id, CancellationToken cancellationToken)
    {
        return await _limitManagerRepository.GetLimitById(id, cancellationToken);
    }

    public async Task<LimitModel?> UpdateLimit(UpdateLimitDto data, CancellationToken cancellationToken)
    {
        var limit = await _limitManagerRepository.GetLimitById(data.Id, cancellationToken);

        if (limit is null)
            return null;

        limit.Update(agency: data.Agency, limit: data.Limit);

        await _limitManagerRepository.UpdateLimit(limit, cancellationToken);
        return limit;
    }

    public async Task<bool> DeleteLimit(Guid id, CancellationToken cancellationToken)
    {
        var limit = await _limitManagerRepository.GetLimitById(id, cancellationToken);

        if (limit is null)
            return false;

        await _limitManagerRepository.DeleteLimit(limit, cancellationToken);
        return true;
    }
}