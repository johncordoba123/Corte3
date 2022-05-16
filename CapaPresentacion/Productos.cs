using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CapaPresentacion
{
    public partial class Productos : Form
    {
        public int? Id;
        public Productos(int? Id)
        {
            InitializeComponent();
            this.Id = Id;
        }
       
        private void Productos_Load(object sender, EventArgs e)
        {
            using (CapaDatos.VentasJuegosEntities db = new CapaDatos.VentasJuegosEntities())
            {
                var listado = db.datos.Where(C => C.fk_usuarios == Id).Select(p => new { p.Id, p.Producto, p.Stock, p.valor });
                dgvProductos.DataSource = listado.ToList();
            }
        }
    }
}
