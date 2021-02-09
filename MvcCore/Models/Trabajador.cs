using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("Trabajadores")]
    public class Trabajador
    {
        [Key]

        public int Id { get; set; }
        public String Apellido { get; set; }
        public String Oficio { get; set; }
        public int Salario { get; set; }

    }
}
