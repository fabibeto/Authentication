using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.OrderModel
{
    public class Ciudad
    {
        [Key]
        public int CiudadID { get; set; }
        public string Imagen { get; set; }
        public string Denominacion { get; set; }
        public long Cant_Habitantes { get; set; }
        public long Superficie { get; set; }
        public int Iconos_GeograficosID { get; set; }
        public virtual ICollection<IconosGeograficos> IconosGeograficos { get; set; }
        
    }
}
