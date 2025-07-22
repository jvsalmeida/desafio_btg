using System;
using FraudSys.Core.Models;

namespace FraudSys.Core.Repositories;

public interface ITransactionManagerRepository
{
    Task CreateTransaction(TransactionModel transactionModel, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken);
}
