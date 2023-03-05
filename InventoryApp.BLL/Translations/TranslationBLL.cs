using AutoMapper;

using InventoryApp.Core.Entities;
using InventoryApp.DTO.Translation;
using InventoryApp.Repositroy.Base;

namespace InventoryApp.BLL.Translations
{
    public class TranslationBLL : ITranslationBLL
    {
        private readonly IMapper _mapper;

        private readonly IRepository<TableName> _tablesRepository;
        private readonly IRepository<Language> _languageRepository;
        private readonly IRepository<TranslationHeader> _translationRepository;

        public TranslationBLL( IMapper mapper, IRepository<TableName> tablesRepository, IRepository<Language> languageRepository, IRepository<TranslationHeader> translationRepository )
        {
            _tablesRepository = tablesRepository;
            _languageRepository = languageRepository;
            _translationRepository = translationRepository;
            _mapper = mapper;
        }

        public int GetTableId( string entityName )
        {
            if (string.IsNullOrWhiteSpace(entityName))
                return 0;
            //TODO: Add Schema
            return _tablesRepository.Where(x => x.Name == entityName/*.ToLower()*/).FirstOrDefault()?.Id ?? 0;
        }

        public List<LanaguageLookupDto> GetAllLanaguages( )
        {
            var languages = _languageRepository.GetAll().ToList();
            return _mapper.Map<List<LanaguageLookupDto>>(languages);
        }

        public TranslationHeader GetTranslation( TranslationHeaderInputDto inputDto )
        {
            var tableId = GetTableId(inputDto.EntityName ?? string.Empty);
            if (tableId == 0)
                return null;

            TranslationHeader model = new TranslationHeader
            {
                TableNameId = tableId,
                TranslationDetails = CreateTranslationDetails(inputDto.Details, inputDto.FieldName)
            };

            return model;
        }

        private List<TranslationDetail> CreateTranslationDetails( List<TranslationDetailInputDto> details, string? fieldName )
        {
            var languages = GetAllLanaguages();
            List<TranslationDetail> output = new List<TranslationDetail>();
            foreach (TranslationDetailInputDto item in details)
            {
                output.Add(new TranslationDetail
                {
                    FieldName = fieldName,
                    LanguageId = item.LanguageId,
                    Translation = item.Translation,
                });
            }
            return output;
        }

        #region Basic Crud

        //public async Task CreateTranslation( TranslationHeaderInputDto inputDto, bool commit )
        //{
        //    var model = _mapper.Map<TranslationHeader>(inputDto);
        //    await _translationRepository.AddAsync(model);
        //    if (commit)
        //        await _unitOfWork.CommitAsync();
        //}
        #endregion Basic Crud

    }
}