using InventoryApp.DTO;
using InventoryApp.DTO.Translation;

using System.Text.Json.Serialization;

namespace InventoryApp.DTO
{
    public abstract class BaseDto : IDto
    {


        [JsonIgnore]
        public int langId { get; set; }
        //public IResponseDto Response { get; set; } = new ResponseDto();
    }

    public class GetEntityInputDto : BaseDto
    {
        public int Id { get; set; }
    }
    public class DeleteEntityInputDto : BaseDto
    {
        public int Id { get; set; }

    }
    public class DeleteTrackedEntityInputDto : DeleteEntityInputDto
    {
        [JsonIgnore]
        public DateTime? ModifiedDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public int? ModifiedBy { get; set; }
    }
}