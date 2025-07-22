using System;
using FraudSys.Core.Models;

namespace FraudSys.Api.Responses;

public class GetTransactionsResponse
{
    public required IEnumerable<TransactionModel> Transactions { get; init; }
}
