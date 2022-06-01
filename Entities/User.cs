using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.Entities
{
    public class User : IdentityUser
    {
        //control de baja lógica
        public bool  IsActive { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento {get; set;}
    }
}
