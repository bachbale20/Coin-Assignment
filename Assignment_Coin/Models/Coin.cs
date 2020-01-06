using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Assignment_Coin.Models
{
    public class Coin
    {
        [Key]
        public string Code { get; set; }
        [Required]
        public string BaseAsset { get; set; }
        [Required]
        public string QuoteAsset { get; set; }
        [Required]
        public decimal LastPrice { get; set; }
        [Required]
        public int Volumn24h { get; set; }
        [ForeignKey("Market")]
        public int? MarketId { get; set; }
        
        public virtual Market Market { get; set; }
        
        public CoinStatus Status { get; set; }


        public enum CoinStatus
        {
            NotDeleted = 0, Deleted = -1
        }

        internal bool IsDeleted()
        {
            return this.Status == CoinStatus.Deleted;
        }
    }
}