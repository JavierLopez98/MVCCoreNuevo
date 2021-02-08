using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNuevo.Models
{
    [Table("VistaDept")]
    public class VistaDept
    {
        [Column("posicion")]
        public int posicion { get; set; }
        [Key]
        [Column("Dept_no")]
        public int numero { get; set; }
        [Column("Dnombre")]
        public String Nombre { get; set; }
        public String Loc { get; set; }
    }
}
