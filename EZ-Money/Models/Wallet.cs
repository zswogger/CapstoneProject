using EZMoney.Models;
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

        /// <summary>
        /// Saves the current balance of the users wallet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentAmount"></param>
        /// <returns></returns>
        public bool saveCurrentBalance(int id, decimal currentAmount)
        {
            return DB.saveWalletBalance(id, currentAmount);
        }

        public static bool saveNewWallet(int userId)
        {
            return DB.saveNewWallet(userId);
        }

    }
}
