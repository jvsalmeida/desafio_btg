using System;

namespace FraudSys.Api.Responses;

public class UpdateLimitResponse
{
    public required Guid Id { get; init; }
    public required string Document { get; init; }
    public required string Agency { get; set; }
    public required string Account { get; set; }
    public required decimal Limit { get; set; }
    public required decimal ConsumedLimit { get; set; }
}
