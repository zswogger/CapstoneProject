using System;
using EZMoney.Models;

namespace EZMoney.Models
{
    public class Profit
    {
        /// <summary>
        /// Id of the profit record
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Amount of the profit record
        /// </summary>
        public decimal profitAmount { get; set; }

        /// <summary>
        /// Date of the profit record
        /// </summary>
        public string profitDate { get; set; }

        /// <summary>
        /// If the profit was refunded or not
        /// </summary>
        public bool refunded { get; set; }

        /// <summary>
        /// Associated transaction ID
        /// </summary>
        public int transactionId { get; set; }

        public Profit()
        {

        }

        public Profit(int id, decimal profitAmount, string profitDate, bool refunded)
        {
            this.id = id;
            this.profitAmount = profitAmount;
            this.profitDate = profitDate;
            this.refunded = refunded;
        }

        /// <summary>
        /// Calculate the profit based on transaction amount
        /// </summary>
        public decimal calculateProfit(Transaction tx)
        {
            return (tx.amount * 0.015m) + 0.08m;
        }

        /// <summary>
        /// Save the profit record in the database
        /// </summary>
        public bool save()
        {
            return DB.saveProfit(this);
        }

        /// <summary>
        /// Refund the profit record
        /// </summary>
        public bool refund()
        {
            return true;
        }
    }
}
