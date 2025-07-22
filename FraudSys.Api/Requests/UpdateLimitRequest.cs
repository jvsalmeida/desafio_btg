using System;

namespace FraudSys.Api.Requests;

public class UpdateLimitRequest
{
    public required string Agency { get; init; }
    public required decimal Limit { get; init; }
}
