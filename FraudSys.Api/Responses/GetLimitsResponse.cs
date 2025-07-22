using System;
using FraudSys.Core.Models;

namespace FraudSys.Api.Responses;

public class GetLimitsResponse
{
    public required IEnumerable<LimitModel> Limits { get; init; }
}
