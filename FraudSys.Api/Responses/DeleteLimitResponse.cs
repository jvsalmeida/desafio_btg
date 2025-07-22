using System;

namespace FraudSys.Api.Responses;

public class DeleteLimitResponse
{
    public required bool? Deleted { get; init; }
}
