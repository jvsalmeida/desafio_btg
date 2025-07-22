using System.Transactions;
using FraudSys.Api.Requests;
using FraudSys.Api.Responses;
using FraudSys.Core.DTOs;
using FraudSys.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FraudSys.Api.Controllers
{
    [Route("api/transaction-manager")]
    [ApiController]
    public class TransactionManagerController : ControllerBase
    {
        public readonly ITransactionManagerService _transactionManagerService;

        public TransactionManagerController(ITransactionManagerService transactionManagerService)
        {
            _transactionManagerService = transactionManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            var transactionDto = new CreateTransactionDto
            {
                LimitId = request.LimitId,
                Document = request.Document,
                TransactionValue = request.TransactionValue,
            };

            var transactionModel = await _transactionManagerService.CreateTransaction(transactionDto, cancellationToken);

            if (transactionModel is null)
                return NotFound();

            var response = new CreateTransactionResponse
            {
                Id = transactionModel.Id,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransaction(CancellationToken cancellationToken)
        {
            var transactions = await _transactionManagerService.GetAllTransactions(cancellationToken);

            var response = new GetTransactionsResponse
            {
                Transactions = transactions,
            };

            return Ok(response);
        }
    }
}
