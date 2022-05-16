using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion;
using System.Runtime.InteropServices;
namespace CapaPresentacion
{
    public partial class HacerFiltro : Form
    {
        public HacerFiltro()
        {
            InitializeComponent();
        }
      
        private void ObtenerDatos()
        {
            dgvHacerFiltro.DataSource = CapaLogica.Usuarios.Obtener();
        }
        private void HacerFiltro_Load(object sender, EventArgs e)
        {
            ObtenerDatos();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int? Id = int.Parse(dgvHacerFiltro.Rows[dgvHacerFiltro.CurrentRow.Index].Cells[0].Value.ToString());
            Form producto = new Productos(Id);
            producto.ShowDialog();
        }

        private void GoLogin_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
