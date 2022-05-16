using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace CapaPresentacion
{
    public partial class RegistroUsuarios : Form
    {
        public RegistroUsuarios()
        {
            InitializeComponent();
        }
        
        private void btnRegistarse_Click(object sender, EventArgs e)
        {
            using (CapaDatos.VentasJuegosEntities db = new CapaDatos.VentasJuegosEntities())
            {
                byte[] tmpSource;
                byte[] tmpHash;
                CapaDatos.usuarios lista = new CapaDatos.usuarios();
                lista.Nombre_Usuario = txtNombre_Usuario.Text;
                lista.Nombre_Completo = txtNombre.Text;
                tmpSource = ASCIIEncoding.ASCII.GetBytes(txtContraseña.Text);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                lista.pass = Convert.ToBase64String(tmpHash);
                lista.rol = txtRol.Text;

                db.usuarios.Add(lista);
                db.SaveChanges();
                this.Close();
                Login Log = new Login();
            }
        }
    }
}
