using System;
using System.Collections.Generic;

namespace InventoryApp.Core.Entities
{
    public partial class TranslationHeader:EntityBase
    {
        public TranslationHeader()
        {
            TranslationDetails = new HashSet<TranslationDetail>();
        }

        public int Id { get; set; }
        public int TableNameId { get; set; }

        public virtual TableName TableName { get; set; } = null!;
        public virtual ICollection<TranslationDetail> TranslationDetails { get; set; }
    }
}
