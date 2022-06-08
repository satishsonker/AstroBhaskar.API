using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AstroBhaskar.API.Models
{
    [Table("Users")]
    public class AstroUser : BaseModel
    {
        [Key]
        public int UserId { get; set; }
        public string UserKey { get; set; }
        public string AuthProvidor { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email id")]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string PasswordHash { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string GoogleId { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool isReset { get; set; }
        public bool isBlocked { get; set; }
        public bool isLocked { get; set; }
        public int LoginAttempts { get; set; }
        public string ResetCode { get; set; }
        public DateTime ResetExpireAt { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}
