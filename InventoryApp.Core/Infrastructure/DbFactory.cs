using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using InventoryApp.Core.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data.Entity;
using Microsoft.Extensions.Options;

namespace InventoryApp.Core.Infrastructure
{


    /// <summary>   A database factory. </summary>
    public class DbFactory : IDbFactory
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public DbFactory(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options;
        }

        public ApplicationDbContext CreateDbContext()
        {
            return new ApplicationDbContext(_options);
        }
    }

}
