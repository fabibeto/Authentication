using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.OrderModel
{
    public class Continente
    {
        [Key]
        public int ContinenteID { get; set; }
        public string Imagen { get; set; }
        public string Denominacion { get; set; }
        public int CiudadID { get; set; }
        public virtual ICollection<Ciudad> Ciudades { get; set; }
     }
}
