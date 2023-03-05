using System;
using System.Collections.Generic;

namespace InventoryApp.Core.Entities
{
    public partial class TableName:EntityBase
    {
        public TableName()
        {
            TranslationHeaders = new HashSet<TranslationHeader>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TranslationHeader> TranslationHeaders { get; set; }
    }
}
