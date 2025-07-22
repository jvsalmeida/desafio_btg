using System;

namespace FraudSys.Core.DTOs;

public class CreateLimitDto
{
    public required string Document { get; init; }
    public required string Agency { get; init; }
    public required string Account { get; init; }
    public required decimal Limit { get; init; }
}
