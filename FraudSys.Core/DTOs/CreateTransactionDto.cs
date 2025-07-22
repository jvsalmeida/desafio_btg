using System;

namespace FraudSys.Core.DTOs;

public class CreateTransactionDto
{
    public required Guid LimitId { get; init; }
    public required string Document { get; init; }
    public required decimal TransactionValue { get; init; }
}
