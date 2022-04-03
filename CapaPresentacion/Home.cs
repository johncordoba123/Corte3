using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;
using CapaLogica;

namespace CapaPresentacion
{
    public partial class Home : Form
    {
        public int ide;
        public Home(int ide)
        {
            InitializeComponent();
            this.ide = ide;
        }

        private void MostrarDatos()
        {
            dgvVentas.DataSource = CapaLogica.Datos.Obtener();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            dgvVentas.DataSource = CapaLogica.Datos.Obtener();
            MostrarDatos();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            int? Id = null;
            RegistroDatos formcrea = new RegistroDatos(ide,Id);
            formcrea.ShowDialog();
            MostrarDatos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? Id = int.Parse(dgvVentas.Rows[dgvVentas.CurrentRow.Index].Cells[0].Value.ToString());
            if (Id != null)
            {
                RegistroDatos edit = new RegistroDatos(ide,Id);
                edit.ShowDialog();
                MostrarDatos();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? Id = int.Parse(dgvVentas.Rows[dgvVentas.CurrentRow.Index].Cells[0].Value.ToString());
            if (Id != null)
            {
                using (VentasJuegosEntities db = new VentasJuegosEntities())
                {
                    datos listado = db.datos.Find(Id);
                    db.datos.Remove(listado);
                    db.SaveChanges();
                }
                MostrarDatos();
            }
        }
    }
}
