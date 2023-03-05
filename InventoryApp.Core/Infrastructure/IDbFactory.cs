﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Core.Infrastructure
{
    /// <summary>   Interface for database factory. </summary>
    public interface IDbFactory
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Initializes this object. </summary>
        ///
        /// <returns>   An ApplicationDbContext. </returns>
        ///-------------------------------------------------------------------------------------------------

        ApplicationDbContext CreateDbContext();

    }
}
