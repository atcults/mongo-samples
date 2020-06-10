using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Samples.Models
{
    public class Clients
    {
        [BsonId]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Remarks { get; set; }
        public string ReferenceName { get; set; }

        public bool IsActive { get; set; }
        public bool CanBet { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Currency Currency { get; set; }

        [JsonPropertyName("Role")]
        [BsonRepresentation(BsonType.String)]
        public Role Role { get; set; }

        public string ParentId { get; set; }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        [BsonElement("ADMIN")]
        ADMIN = 1,

        [BsonElement("SUPER MASTER")]
        SUPER_MASTER = 2,

        [BsonElement("SUB SUPER MASTER")]
        SUB_SUPER_MASTER = 4,

        [BsonElement("MASTER")]
        MASTER = 8,

        [BsonElement("CLIENT")]
        CLIENT = 16
    }

    public struct Partnership
    {
        public decimal Sports { get; set; }
        public decimal Casino { get; set; }
        public decimal Other { get; set; }
    }


    public struct BetConfig
    {
        public int MinBet { get; set; }
        public int MaxBet { get; set; }
        public int BetDelay { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sports
    {
        [BsonElement("SOCCER")]
        SOCCER = 1,

        TENNIS = 2,

        CRICKET = 4
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MarketType
    {
        MARKET,
        BOOKMAKER,
        SESSION        
    }


    public struct GameController
    {
        public bool Cricket { get; set; }
        public bool Soccer { get; set; }
        public bool Tennis { get; set; }
        public bool Binary { get; set; }
        public bool Casino { get; set; }
    }

    public class Currency
    { 
        [BsonId]
        public string Name { get; set; }
        public byte Mux { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Side
    {
        BACK, LAY
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatus
    {
        EXECUTION_COMPLETE,
        EXECUTABLE
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderType
    {
        LIMIT,				// normal exchange limit order for immediate execution
        LIMIT_ON_CLOSE,		// limit order for the auction (SP)
        MARKET_ON_CLOSE		// market order for the auction (SP)
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PersistenceType
    {
        LAPSE,				// lapse the order at turn-in-play
        PERSIST,			// put the order into the auction (SP) at turn-in-play
        MARKET_ON_CLOSE,	// put the order into the auction (SP) at turn-in-play
    }

    public class RunnerProfitAndLoss
    {
        [JsonPropertyName("selectionId")]
        public long SelectionId { get; set; }

        [JsonPropertyName("ifWin")]
        public double IfWin { get; set; }

        [JsonPropertyName("ifLose")]
        public double IfLose { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0}", "RunnerProfitAndLoss")
                        .AppendFormat(" : SelectionId={0}", SelectionId)
                        .AppendFormat(" : IfWin={0}", IfWin)
                        .AppendFormat(" : IfLose={0}", IfLose);

            return sb.ToString();
        }
    }

    public class MarketProfitAndLoss
    {
        [JsonPropertyName("marketId")]
        public string MarketId { get; set; }

        [JsonPropertyName("commissionApplied")]
        public double CommissionApplied { get; set; }

        [JsonPropertyName("profitAndLosses")]
        public List<RunnerProfitAndLoss> ProfitAndLosses { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0}", "MarketProfitAndLoss")
                        .AppendFormat(" : MarketId={0}", MarketId)
                        .AppendFormat(" : CommissionApplied={0}", CommissionApplied)
                        .ToString();

            if (ProfitAndLosses != null && ProfitAndLosses.Count > 0)
            {
                int idx = 0;
                foreach (var profitandloss in ProfitAndLosses)
                {
                    sb.AppendFormat(" : ProfitAndLosses[{0}]={1}", idx++, profitandloss);
                }
            }

            return sb.ToString();
        }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BetStatus
    {
        SETTLED,
        VOIDED,
        LAPSED,
        CANCELLED
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MarketStatus
    {
        INACTIVE, OPEN, SUSPENDED, CLOSED
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InstructionReportErrorCode
    {
        OK,
        INVALID_BET_SIZE,
        INVALID_RUNNER,
        BET_TAKEN_OR_LAPSED,
        BET_IN_PROGRESS,
        RUNNER_REMOVED,
        MARKET_NOT_OPEN_FOR_BETTING,
        LOSS_LIMIT_EXCEEDED,
        MARKET_NOT_OPEN_FOR_BSP_BETTING,
        INVALID_PRICE_EDIT,
        INVALID_ODDS,
        INSUFFICIENT_FUNDS,
        INVALID_PERSISTENCE_TYPE,
        ERROR_IN_MATCHER,
        INVALID_BACK_LAY_COMBINATION,
        ERROR_IN_ORDER,
        INVALID_BID_TYPE,
        INVALID_BET_ID,
        CANCELLED_NOT_PLACED,
        RELATED_ACTION_FAILED,
        NO_ACTION_REQUIRED,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum InstructionReportStatus
    {
        SUCCESS,
        FAILURE,
        TIMEOUT,
    }

    public class PriceSize
    {
        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("size")]
        public double Size { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}@{1}", Size, Price)
                        .ToString();
        }
    }

    public class CurrentOrderSummary
    {
        [JsonPropertyName("betId")]
        public string BetId { get; set; }

        [JsonPropertyName("marketId")]
        public string MarketId { get; set; }

        [JsonPropertyName("selectionId")]
        public string SelectionId { get; set; }

        [JsonPropertyName("priceSize")]
        public PriceSize PriceSize { get; set; }

        [JsonPropertyName("side")]
        public Side Side { get; set; }

        [JsonPropertyName("status")]
        public OrderStatus Status { get; set; }

        [JsonPropertyName("persistenceType")]
        public PersistenceType PersistenceType { get; set; }

        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }

        [JsonPropertyName("placedDate")]
        public DateTime PlacedDate { get; set; }

        [JsonPropertyName("matchedDate")]
        public DateTime MatchedDate { get; set; }

        [JsonPropertyName("averagePriceMatched")]
        public double AveragePriceMatched { get; set; }

        [JsonPropertyName("sizeMatched")]
        public double SizeMatched { get; set; }

        [JsonPropertyName("sizeRemaining")]
        public double SizeRemaining { get; set; }

        [JsonPropertyName("sizeLapsed")]
        public double SizeLapsed { get; set; }

        [JsonPropertyName("sizeCancelled")]
        public double SizeCancelled { get; set; }

        [JsonPropertyName("sizeVoided")]
        public double SizeVoided { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendFormat("{0}", "CurrentOrderSummary")
                        .AppendFormat(" : BetId={0}", BetId)
                        .AppendFormat(" : MarketId={0}", MarketId)
                        .AppendFormat(" : SelectionId={0}", SelectionId)
                        .AppendFormat(" : PriceSize={0}", PriceSize)
                        .AppendFormat(" : Side={0}", Side)
                        .AppendFormat(" : Status={0}", Status)
                        .AppendFormat(" : PersistenceType={0}", PersistenceType)
                        .AppendFormat(" : OrderType={0}", OrderType)
                        .AppendFormat(" : PlacedDate={0}", PlacedDate)
                        .AppendFormat(" : MatchedDate={0}", MatchedDate)
                        .AppendFormat(" : AveragePriceMatched={0}", AveragePriceMatched)
                        .AppendFormat(" : SizeMatched={0}", SizeMatched)
                        .AppendFormat(" : SizeRemaining={0}", SizeRemaining)
                        .AppendFormat(" : SizeLapsed={0}", SizeLapsed)
                        .AppendFormat(" : SizeCancelled={0}", SizeCancelled)
                        .AppendFormat(" : SizeVoided={0}", SizeVoided);

            return sb.ToString();
        }
    }

    public class OrderRequest
    {
        public string CustomerRef { get; set; }
        public string MarketId { get; set; }

    }

    public class OrderInstruction
    {
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }

        [JsonPropertyName("selectionId")]
        public long SelectionId { get; set; }

        [JsonPropertyName("side")]
        public Side Side { get; set; }

        [JsonPropertyName("limitOrder")]
        public LimitOrder LimitOrder { get; set; }

        [JsonPropertyName("limitOnCloseOrder")]
        public LimitOnCloseOrder LimitOnCloseOrder { get; set; }

        [JsonPropertyName("marketOnCloseOrder")]
        public MarketOnCloseOrder MarketOnCloseOrder { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder()
                .AppendFormat("OrderType={0}", OrderType)
                .AppendFormat(" : SelectionId={0}", SelectionId)
                .AppendFormat(" : Side={0}", Side);

            switch (OrderType)
            {
                case OrderType.LIMIT:
                    sb.AppendFormat(" : LimitOrder={0}", LimitOrder);
                    break;
                case OrderType.LIMIT_ON_CLOSE:
                    sb.AppendFormat(" : LimitOnCloseOrder={0}", LimitOnCloseOrder);
                    break;
                case OrderType.MARKET_ON_CLOSE:
                    sb.AppendFormat(" : MarketOnCloseOrder={0}", MarketOnCloseOrder);
                    break;
            }

            return sb.ToString();
        }
    }

    public class LimitOrder
    {
        [JsonPropertyName("size")]
        public double Size { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        [JsonPropertyName("persistenceType")]
        public PersistenceType PersistenceType { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                        .AppendFormat("Size={0}", Size)
                        .AppendFormat(" : Price={0}", Price)
                        .AppendFormat(" : PersistenceType={0}", PersistenceType)
                        .ToString();
        }
    }

    public class LimitOnCloseOrder
    {
        [JsonPropertyName("size")]
        public double Size { get; set; }

        [JsonPropertyName("liability")]
        public double Liability { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                        .AppendFormat("Size={0}", Size)
                        .AppendFormat(" : Liability={0}", Liability)
                        .ToString();
        }
    }

    public class MarketOnCloseOrder
    {
        [JsonPropertyName("size")]
        public double Size { get; set; }

        public override string ToString()
        {
            return new StringBuilder()
                        .AppendFormat("Size={0}", Size)
                        .ToString();
        }
    }


}
