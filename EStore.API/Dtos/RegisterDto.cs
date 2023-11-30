using System;
using System.ComponentModel.DataAnnotations;

namespace EStore.API.Dtos
{
	public class RegisterDto
	{
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z]\\w{3,14}$")]
        public string Password { get; set; }
	}
}

