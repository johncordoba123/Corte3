using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
   public class Usuarios
    {
        public static List <ListadoUsuarios> Obtener()
        {
            using (CapaDatos.VentasJuegosEntities db = new CapaDatos.VentasJuegosEntities())
            {
                return (from f in db.usuarios
                        select new ListadoUsuarios()
                        { 
                            Id = f.Id,
                            Nombre_Completo = f.Nombre_Completo,
                            Nombre_Usuario = f.Nombre_Usuario,
                            rol = f.rol 
                        }).ToList();
            }
        }

    }
}
