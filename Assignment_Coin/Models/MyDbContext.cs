﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Assignment_Coin.Models
{
    public class MyDbContext : IdentityDbContext<Account>
    {

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public MyDbContext() : base("name=MyDbContext")
        {
        }

        public static MyDbContext Create()
        {
            return new MyDbContext();
        }

        public System.Data.Entity.DbSet<Assignment_Coin.Models.Coin> Coins { get; set; }

        public System.Data.Entity.DbSet<Assignment_Coin.Models.Market> Markets { get; set; }
    }
}