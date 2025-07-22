using FraudSys.Api.Requests;
using FraudSys.Api.Responses;
using FraudSys.Core.DTOs;
using FraudSys.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.Api.Controllers;



[ApiController]
[Route("api/limit-manager")]
public class LimitManagerController : ControllerBase
{
    private readonly ILimitManagerService _limitManagerService;

    public LimitManagerController(ILimitManagerService limitManagerService)
    {
        _limitManagerService = limitManagerService;

    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateLimitResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLimit([FromBody] CreateLimitRequest request, CancellationToken cancellationToken)
    {
        var limitDto = new CreateLimitDto
        {
            Document = request.Document,
            Agency = request.Agency,
            Account = request.Account,
            Limit = request.Limit,
        };

        var limitModel = await _limitManagerService.CreateLimit(limitDto, cancellationToken);

        var response = new CreateLimitResponse
        {
            Id = limitModel.Id.ToString(),
        };

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetLimit(CancellationToken cancellationToken)
    {
        var limits = await _limitManagerService.GetLimits(cancellationToken);

        var response = new GetLimitsResponse
        {
            Limits = limits,
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLimitById(Guid id, CancellationToken cancellationToken)
    {
        var limitModel = await _limitManagerService.GetLimitById(id, cancellationToken);

        if (limitModel is null)
        {
            return NotFound();
        }

        var response = new GetLimitByIdResponse
        {
            Id = limitModel.Id,
            Document = limitModel.Document,
            Agency = limitModel.Agency,
            Account = limitModel.Account,
            Limit = limitModel.Limit,
            ConsumedLimit = limitModel.ConsumedLimit
        };

        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLimit(Guid id, [FromBody] UpdateLimitRequest request, CancellationToken cancellationToken)
    {
        var dto = new UpdateLimitDto
        {
            Id = id,
            Agency = request.Agency,
            Limit = request.Limit,
        };

        var limitModel = await _limitManagerService.UpdateLimit(dto, cancellationToken);

        if (limitModel is null)
            return NotFound();

        var response = new UpdateLimitResponse
        {
            Id = limitModel.Id,
            Document = limitModel.Document,
            Agency = limitModel.Agency,
            Account = limitModel.Account,
            Limit = limitModel.Limit,
            ConsumedLimit = limitModel.ConsumedLimit
        };

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLimit(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _limitManagerService.DeleteLimit(id, cancellationToken);

        if (deleted is false)
            return NotFound();

        var response = new DeleteLimitResponse
        {
            Deleted = deleted
        };

        return Ok(response);
    }
}
