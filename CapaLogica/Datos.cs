using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
   public class Datos
    {
       public static List<ListadoDatos> Obtener()
       {
           using (VentasJuegosEntities db = new VentasJuegosEntities())
           {
               return  (from b in db.datos
                        select new ListadoDatos()
                       {
                           Id = b.Id,
                           Producto= b.Producto,
                           Stock = b.Stock,
                           valor = b.valor,
                           fk_usuarios = b.fk_usuarios
                        }).ToList();
            }
        }
    }
}
