﻿using EZMoney.Models;
using EZMoney;

namespace EZMoney.Models
{
    public class Wallet
    {
        /// <summary>
        /// Id of the wallet
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Id of the wallet's owner
        /// </summary>
        public int userId { get; set; }

        /// <summary>
        /// Current amount held in wallet
        /// </summary>
        public decimal currentAmount { get; set; }

        public Wallet() 
        { 
        
        }

        public Wallet(int id, int userId, decimal currentAmount)
        {
            this.id = id;
            this.userId = userId;
            this.currentAmount = currentAmount;
        }

        /// <summary>
        /// Get current amount held in wallet
        /// </summary>
        public static Wallet getWalletByUserId(int id)
        {
            return DB.getUserWallet(id);
        }

        public string getWalletBalanceAsString()
        {
            Global.sessionUser.wallet = DB.getUserWallet(Global.sessionUser.id);
            return Global.sessionUser.wallet.currentAmount.ToString();
        }

        public bool saveCurrentBalance(int id, decimal currentAmount)
        {
            return DB.saveWalletBalance(id, currentAmount);
        }

    }
}
