using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstroBhaskar.API.Models
{
    [Table("MasterCollection")]
    public class MasterCollection : BaseModel
    {
        [Key]
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string DisplayValue { get; set; }
        public string Remark { get; set; }

    }
}
