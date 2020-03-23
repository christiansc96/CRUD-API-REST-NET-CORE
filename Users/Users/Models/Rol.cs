using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TareaSemana7_E1_ChristianSanchez.Models
{
    public class Rol
    {
        [Key]
        public int IdRol { get; set; }

        [Required]
        public string NombreRol { get; set; }
    }
}
