using System;
using System.ComponentModel.DataAnnotations;


namespace FraudSys.Api.Requests;

public class CreateLimitRequest
{
    [Required]
    [MinLength(11)]
    public required string Document { get; init; }
    [Required]
    public required string Agency { get; set; }
    [Required]
    public required string Account { get; set; }
    [Required]
    public required decimal Limit { get; set; }
}
