using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;

namespace FraudSys.Core.Models;



[DynamoDBTable("TransactionModel")]
public class TransactionModel
{
    [DynamoDBHashKey("Id")]
    public Guid Id { get; init; }

    [DynamoDBRangeKey("Document")]
    public string Document { get; init; }

    [DynamoDBProperty("TransactionValue")]
    public decimal TransactionValue { get; set; }

    [DynamoDBProperty("Status")]
    public TransactionStatus Status { get; set; }

    public TransactionModel()
    {
        Id = Guid.Empty;
        Document = string.Empty;
        TransactionValue = decimal.Zero;
        Status = TransactionStatus.Pending;
    }

    public TransactionModel(
        string document,
        decimal transactionValue
    )
    {
        Id = Guid.NewGuid();
        Document = document;
        TransactionValue = transactionValue;
        Status = TransactionStatus.Pending;
    }

    public void RejectTransaction()
    {
        Status = TransactionStatus.Rejected;
    }

    public void ExecuteTransaction(LimitModel limit)
    {
        if (Status != TransactionStatus.Pending)
        {
            return;
        }

        if (limit.ConsumeLimit(this))
        {
            Status = TransactionStatus.Approved;
        }
        else
        {
            Status = TransactionStatus.Rejected;
        }
    }

    public bool IsApproved()
    {
        return Status == TransactionStatus.Approved;
    }

    public enum TransactionStatus
    {
        Pending,
        Approved,
        Rejected
    }


}
