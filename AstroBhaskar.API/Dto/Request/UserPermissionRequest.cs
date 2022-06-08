using System;
using System.ComponentModel.DataAnnotations;

namespace AstroBhaskar.API.Dto.Request
{
    public class UserPermissionRequest
    {
        public int UserPermissionId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "User key is required")]
        public string UserKey { get; set; }

        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Invalid user Id")]
        public int UserId { get; set; }
        public bool CanView { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanCreate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
