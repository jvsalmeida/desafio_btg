using Amazon.DynamoDBv2.DataModel;

namespace FraudSys.Core.Models;



[DynamoDBTable("LimitModel")]
public class LimitModel
{
    [DynamoDBHashKey("Id")]
    public Guid Id { get; init; }

    [DynamoDBRangeKey("Document")]
    public string Document { get; init; }

    [DynamoDBProperty("Agency")]
    public string Agency { get; set; }

    [DynamoDBProperty("Account")]
    public string Account { get; set; }

    [DynamoDBProperty("Limit")]
    public decimal Limit { get; set; }

    [DynamoDBProperty("ConsumedLimit")]
    public decimal ConsumedLimit { get; set; }

    public LimitModel()
    {
        Id = Guid.Empty;
        Document = string.Empty;
        Agency = string.Empty;
        Account = string.Empty;
        Limit = decimal.Zero;
        ConsumedLimit = decimal.Zero;
    }

    public LimitModel(
        string document,
        string agency,
        string account,
        decimal limit
    )
    {
        Id = Guid.NewGuid();
        Document = document;
        Agency = agency;
        Account = account;
        Limit = limit;
        ConsumedLimit = 0;
    }

    public bool ConsumeLimit(TransactionModel transaction)
    {
        if (Document != transaction.Document)
        {
            return false;
        }

        if (!IsLimitEnough(transaction.TransactionValue))
        {
            return false;
        }

        ConsumedLimit += transaction.TransactionValue;

        return true;
    }

    public bool IsLimitEnough(decimal value)
    {
        return value <= AvailableLimit();
    }

    public decimal AvailableLimit()
    {
        return Limit - ConsumedLimit;
    }

    public void Update(string agency, decimal limit)
    {
        if (limit >= 0)
        {
            Agency = agency;
            Limit = limit;
        }
    }
}
