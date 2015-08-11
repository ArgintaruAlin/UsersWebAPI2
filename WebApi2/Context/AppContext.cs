using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApi2.Models;

namespace WebApi2.Context
{
    public class AppContext : DbContext
    {
        public AppContext() :
            base("AppContext")
        {

        }

        public DbSet<User> Users { get; set; }
    }
}