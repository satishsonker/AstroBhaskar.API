using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstroBhaskar.API.Models
{
    [Table("SubscribeNewsLetter")]
    public class SubscribeNewsLetter : BaseModel
    {
        [Key]
        public int SubscribeNewsLetterId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email id")]
        public string Email { get; set; }
        public string Type { get; set; }
        public string UnsubscribeReason { get; set; }
        public bool IsDeleted { get; set; }
    }
}
