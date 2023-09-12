using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int? IdUsuario { get; set; }
        [Required]
        [StringLength(50)]

        public string Nombre { get; set; }
        [Required]
        public string ApellidoPaterno{ get; set; }
        public string ApellidoMaterno{ get; set; }
        public string Imagen{ get; set; }
        [Required]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        public bool Status { get; set; }
        //propiedad de navegacion
        public ML.Rol Rol { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion Direccion { get; set; }
    }
}

//PL -add, update, delete
//BL -GetAll, GetById
