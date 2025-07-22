using Amazon.DynamoDBv2.DataModel;
using FraudSys.Core.Models;

namespace FraudSys.Core.Repositories;

public class TransactionManagerRepository : ITransactionManagerRepository
{
    private readonly IDynamoDBContext _database;

    public TransactionManagerRepository(IDynamoDBContext database)
    {
        _database = database;
    }

    public async Task CreateTransaction(TransactionModel transactionModel, CancellationToken cancellationToken)
    {
        await _database.SaveAsync(transactionModel, cancellationToken);
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken)
    {
        return await _database.ScanAsync<TransactionModel>([]).GetRemainingAsync(cancellationToken);
    }
}
