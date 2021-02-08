using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("Dept")]
    public class Departamento
    {
        [Key]
        [Column("Dept_No")]
        public int Numero { get; set; }
        [Column("Dnombre")]
        public String Nombre { get; set; }
        [Column("Loc")]
        public String Localidad { get; set; }
    }
}
