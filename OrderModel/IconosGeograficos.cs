using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeAPI.OrderModel
{
    public class IconosGeograficos
    {
        [Key]
        public int Iconos_GeograficosID { get; set; }
        public string Imagen { get; set; }
        public string Denominacion { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Creación")]

        public DateTime Fecha_Creacion { get; set; }
        public long Altura { get; set; }
        public int CiudadID { get; set; }
        public virtual Ciudad Ciudad { get; set; }
        
    }
}
