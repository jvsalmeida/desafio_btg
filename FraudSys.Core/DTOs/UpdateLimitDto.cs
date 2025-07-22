using System;

namespace FraudSys.Core.DTOs;

public class UpdateLimitDto
{
    public required Guid Id { get; init; }
    public required string Agency { get; init; }
    public required decimal Limit { get; init; }
}
