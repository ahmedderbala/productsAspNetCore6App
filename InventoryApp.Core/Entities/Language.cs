using System;
using System.Collections.Generic;

namespace InventoryApp.Core.Entities
{
    public partial class Language:EntityBase
    {
        public Language()
        {
            TranslationDetails = new HashSet<TranslationDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<TranslationDetail> TranslationDetails { get; set; }
    }
}
