using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Commands.AuthCommands
{
    public class RegisterDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Username must not contain special characters.")]
        public string UserName { get; set; }



        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        public string Password { get; set; }
    }
}
