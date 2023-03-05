using System;
using System.Collections.Generic;

namespace InventoryApp.Core.Entities
{
    public partial class TranslationDetail:EntityBase
    {
        public int Id { get; set; }
        public int TranslationHeaderId { get; set; }
        public int LanguageId { get; set; }
        public string Translation { get; set; } = null!;
        public string? FieldName { get; set; }

        public virtual Language Language { get; set; } = null!;
        public virtual TranslationHeader TranslationHeader { get; set; } = null!;
    }
}
