using AstroBhaskar.API.Constants;
using System.ComponentModel.DataAnnotations;

namespace AstroBhaskar.API.Dto.Request
{
    public class UserRequest
    {
        public int UserId { get; set; }
        public string UserKey { get; set; }
        public string AuthProvidor { get; set; }

        [EmailAddress(ErrorMessage = StaticValues.InvaliEmail)]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string GoogleId { get; set; }

    }
}
