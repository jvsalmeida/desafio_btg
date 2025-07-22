using System;
using FraudSys.Core.DTOs;
using FraudSys.Core.Models;

namespace FraudSys.Core.Services;

public interface ITransactionManagerService
{
    Task<TransactionModel?> CreateTransaction(CreateTransactionDto data, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionModel>> GetAllTransactions(CancellationToken cancellationToken);
}
