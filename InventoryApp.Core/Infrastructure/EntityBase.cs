﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.Core.Infrastructure
{
    public abstract class EntityBase : IEntityBase
    {
        public int? TranslationHeaderId { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether the inactive. </summary>
        ///
        /// <value> True if inactive, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool Inactive { get; set; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets or sets a value indicating whether this object is deleted. </summary>
        ///
        /// <value> True if this object is deleted, false if not. </value>
        ///-------------------------------------------------------------------------------------------------

        public bool IsDeleted { get; set; }
    }
}

//public interface IHaveTranslationColumn
//{
//    public int TranslationHeaderId { get; set; }

//}