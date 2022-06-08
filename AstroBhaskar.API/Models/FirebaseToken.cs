using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstroBhaskar.API.Models
{
    [Table("FirebaseToken")]
    public class FirebaseToken : BaseModel
    {
        [Key]
        public int TokenId { get; set; }
        public string Token { get; set; }

        [EmailAddress(ErrorMessage = "Invalid user email id")]
        public string UserEmail { get; set; }
    }
}
