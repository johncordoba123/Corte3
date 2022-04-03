using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class ListadoDatos
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Stock { get; set; }
        public int? valor { get; set; }
        public int fk_usuarios { get; set; }
    }
}
