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
namespace CapaPresentacion
{
    public partial class RegistroDatos : Form
    {
        public int? Id;
        public int ide;
        datos lista = null;
        public RegistroDatos(int ide,int? Id = null)
        {
            InitializeComponent();
            this.ide = ide;
            this.Id = Id;
            if (Id != null)
                cargarDatosFormularios();
        }

        private void cargarDatosFormularios()
        {
            using (VentasJuegosEntities db = new VentasJuegosEntities())
            {
                lista = db.datos.Find(Id);
                txtProducto.Text = lista.Producto;
                txtStock.Text = lista.Stock;
                txtValor.Text = lista.valor.ToString();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (VentasJuegosEntities db = new VentasJuegosEntities())
            {
                if (Id == null)
                {
                    lista = new datos();
                }

                lista.Producto = txtProducto.Text;
                lista.Stock = txtStock.Text;
                lista.valor= int.Parse(txtValor.Text);
                lista.fk_usuarios = ide;

                if (Id == null)
                {
                    db.datos.Add(lista);
                }
                else
                {
                    db.Entry(lista).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                this.Close();
            }
        }
    }
}
    

