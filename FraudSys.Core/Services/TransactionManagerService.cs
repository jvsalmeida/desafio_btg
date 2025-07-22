using FraudSys.Core.DTOs;
using FraudSys.Core.Models;
using FraudSys.Core.Repositories;

namespace FraudSys.Core.Services;

public class TransactionManagerService : ITransactionManagerService
{
    private readonly ILimitManagerRepository _limitManagerRepository;
    private readonly ITransactionManagerRepository _transactionManagerRepository;

    public TransactionManagerService(ILimitManagerRepository limitManagerRepository, ITransactionManagerRepository transactionManagerRepository)
    {
        _limitManagerRepository = limitManagerRepository;
        _transactionManagerRepository = transactionManagerRepository;
    }

    public async Task<TransactionModel?> CreateTransaction(CreateTransactionDto data, CancellationToken cancellationToken)
    {
        var limit = await _limitManagerRepository.GetLimitById(data.LimitId, cancellationToken);

        var transaction = new TransactionModel(
            document: data.Document,
            transactionValue: data.TransactionValue
        );

        if (limit is null)
        {
            transaction.RejectTransaction();
        }
        else
        {
            transaction.ExecuteTransaction(limit);

            if (transaction.IsApproved())
            {
                await _limitManagerRepository.UpdateLimit(limit, cancellationToken);
            }
        }

        await _transactionManagerRepository.CreateTransaction(transaction, cancellationToken);

        return transaction;
    }

    public async Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken)
    {
        return await _transactionManagerRepository.GetAllTransactions(cancellationToken);
    }
}
