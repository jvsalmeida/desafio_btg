using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using FraudSys.Core.DTOs;
using FraudSys.Core.Models;

namespace FraudSys.Core.Repositories;



public class LimitManagerRepository : ILimitManagerRepository
{
    private readonly IDynamoDBContext _database;

    public LimitManagerRepository(IDynamoDBContext database)
    {
        _database = database;
    }

    public async Task CreateLimit(LimitModel limitModel, CancellationToken cancellationToken)
    {
        await _database.SaveAsync(limitModel, cancellationToken);
    }

    public async Task<IEnumerable<LimitModel>> GetAllLimits(CancellationToken cancellationToken)
    {
        return await _database.ScanAsync<LimitModel>([]).GetRemainingAsync(cancellationToken);
    }

    public async Task<LimitModel?> GetLimitById(Guid id, CancellationToken cancellationToken)
    {
        return (await _database.QueryAsync<LimitModel>(id).GetRemainingAsync(cancellationToken)).FirstOrDefault();
    }

    public async Task UpdateLimit(LimitModel limitModel, CancellationToken cancellationToken)
    {
        await _database.SaveAsync(limitModel, cancellationToken);
    }

    public async Task DeleteLimit(LimitModel limitModel, CancellationToken cancellationToken)
    {
        await _database.DeleteAsync<LimitModel>(limitModel, cancellationToken);
    }
}
