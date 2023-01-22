using System;
using EZMoney;

namespace EZMoney.Models
{
    public class Transaction
    {
        /// <summary>
        /// Id of the Transaction
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id of the user that has sent the transaction
        /// </summary>
        public int fromUserId { get; set; }

        /// <summary>
        /// Id of the user that is receiving the transaction
        /// </summary>
        public int toUserId { get; set; }

        /// <summary>
        /// Amount of the Transaction
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Profit record to determine the amount of profit made
        /// </summary>
        public Profit profit { get; set; }

        public string transactionDate { get; set; }

        public string memo { get; set; }

        public Transaction()
        {

        }

        public Transaction(int id, int fromUserId, int toUserId, decimal amount, decimal profitAmount, string transactionDate, string memo)
        {
            this.id = id;
            this.fromUserId = fromUserId;
            this.toUserId = toUserId;
            this.amount = amount;
            this.transactionDate = transactionDate;
            this.memo = memo;
            this.profit = new Profit();
            this.profit.profitAmount= profitAmount;
        }



        /// <summary>
        /// Save the transaction to the database
        /// </summary>
        public bool save()
        {
            return DB.saveTransaction(this);
        }

        /// <summary>
        /// Refund the transaction to the sending user
        /// </summary>
        public bool refund(int id)
        {
            return true;
        }
    }
}
