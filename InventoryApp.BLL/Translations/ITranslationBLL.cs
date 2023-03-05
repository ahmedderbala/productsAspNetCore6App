using InventoryApp.Core.Entities;
using InventoryApp.DTO.Translation;

namespace InventoryApp.BLL.Translations
{
    public interface ITranslationBLL
    {
        List<LanaguageLookupDto> GetAllLanaguages( );
        int GetTableId( string entityName );
        TranslationHeader GetTranslation( TranslationHeaderInputDto inputDto );
    }
}