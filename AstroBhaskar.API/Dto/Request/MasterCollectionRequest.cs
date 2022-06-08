using AstroBhaskar.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace AstroBhaskar.API.Dto.Request
{
    public class MasterCollectionRequest
    {
        public int CollectionId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "CollectionName" + StaticValues.RequiredMessage)]
        public string CollectionName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Key" + StaticValues.RequiredMessage)]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Value" + StaticValues.RequiredMessage)]
        public string Value { get; set; }
        public string DisplayValue { get; set; }
        public string Remark { get; set; }
    }
}
