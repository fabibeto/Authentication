using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.Views.Auth.Login
{
    public class LoginResponseViewModel
    {
        [Required]
        [MinLength(6)]
        public string Token { get; set; }
        
        [Required]
        [MinLength(6)]
        public DateTime ValidTo { get; set; }
    }
}
