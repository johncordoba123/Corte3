using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loguearse(String usuario,String password)
        {
            try
            {
                byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(password);
                byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                String contrasena = Convert.ToBase64String(tmpHash);
                using (CapaDatos.VentasJuegosEntities db = new CapaDatos.VentasJuegosEntities())
                {
                    var lista = db.usuarios.Where(C => C.Nombre_Usuario == usuario && C.pass == contrasena);//verificamos los datos que traemos
                    if (lista.Count() == 1)
                    {
                        foreach (var Datos in lista.ToList())
                        {
                            if (Datos.rol == "admin")//lee los datos admin
                            {
                                MessageBox.Show("Bienvenido a la tienda " + Datos.Nombre_Completo);
                                Home home = new Home(Datos.Id);
                                home.Show();
                                this.Hide();
                            }
                            else if (Datos.rol == "user")//lee los datos user
                            {
                                MessageBox.Show("Bienvenido a la tienda " + Datos.Nombre_Completo);
                                
                                Home home = new Home(Datos.Id);
                                home.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("No se le a asignado ningun rol al usuario ", "alert");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña no existen");
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Error = " +i);
            }
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuarios.Text;
            String password = txtContraseñas.Text;
            loguearse(usuario,password);
        }


            private void LlbRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
            {
                RegistroUsuarios registro = new RegistroUsuarios();
                registro.Show();
                //this.Hide();
            }
        }
    } 


