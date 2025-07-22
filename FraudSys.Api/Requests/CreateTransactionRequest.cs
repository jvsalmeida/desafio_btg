using System;

namespace FraudSys.Api.Requests;

public class CreateTransactionRequest
{
    public required Guid LimitId { get; init; }
    public required string Document { get; init; }
    public required decimal TransactionValue { get; init; }
}
