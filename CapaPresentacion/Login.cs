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
using System.Runtime.InteropServices;

//variables = son los datos de tipo int,string,short,long,etc;
//metodos = Secuencia de enunciados dentro de una unidad logica en nuestro programa;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        
        public Login()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd,int wsmg ,int wparam, int lparam);


        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loguearse(String usuario,String password)//Metodo loguearse con variables usuario y contraseña
        {
            try
            {
                byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(password);//libreria para enciptar los datos (contraseña)
                byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);//libreria para enciptar los datos
                String contrasena = Convert.ToBase64String(tmpHash);//lee datos int y se parsea para datos string
                using (CapaDatos.VentasJuegosEntities db = new CapaDatos.VentasJuegosEntities())
                {
                    var lista = db.usuarios.Where(C => C.Nombre_Usuario == usuario && C.pass == contrasena);//verificamos los datos que traemos
                    if (lista.Count() == 1)
                    {
                        foreach (var Datos in lista.ToList())//usamos este foreach donde se dice que traiga valores de Datos(clase en la capaLogica), y los traiga en una lista 
                        {
                            if (Datos.rol == "admin")//lee los datos admin
                            {
                                MessageBox.Show("Bienvenido a la tienda " + Datos.Nombre_Completo);//En esta caja de texto que colocamos el nombre completo nos da la bienvenida
                                HacerFiltro hacerfiltro = new HacerFiltro(/*Datos.Id*/);//nombramos la variable Id(usuarios) del metodo de la base de datos,nos direcciona al formulario de home
                                hacerfiltro.Show();
                                this.Hide();// Metodo para ocultar el formulario
                            }
                            else if (Datos.rol == "user")//lee los datos user
                            {
                                MessageBox.Show("Bienvenido a la tienda " + Datos.Nombre_Completo);//En esta caja de texto que colocamos el nombre completo nos da la bienvenida

                                Home home = new Home(Datos.Id);//nos direcciona al formulario de home,nombramos la variable Id(usuarios) del metodo de la base de datos 
                                home.Show();
                                this.Hide();// Metodo para ocultar el formulario
                            }
                            else
                            {
                                MessageBox.Show("No se le a asignado ningun rol al usuario ", "alert");//Si no toma valor admin ni user muestra este mesaje
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Contraseña no existen");//si no existe ningun usuario muestra este mensaje
                    }
                }
            }
            catch (Exception i)
            {
                MessageBox.Show("Error = " +i);//Si no hubo errores ignora el catch y sigue con la logica del try, si hubo errores ignora el try y ejecuta el codigo de catch
            }
        }
        private void btnCrear_Click(object sender, EventArgs e)
        {
            String usuario = txtUsuarios.Text;//declaramos el usuario con nuestra caja de texto nombrada usuarios
            String password = txtContraseñas.Text;//declaramos el usuario con nuestra caja de texto nombrada usuarios
            loguearse(usuario,password);//tomamos el metodo que nombramos anteriormente y le ponemos los variables usuario y password
        }
        private void LlbRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegistroUsuarios registro = new RegistroUsuarios();//nos direcciona al formulario de registro
            registro.Show();// muestra el formulario
            //this.Hide();
        }

      

        private void txtUsuarios_Enter(object sender, EventArgs e)
        {
            if (txtUsuarios.Text == "USUARIO")
            {
                txtUsuarios.Text = "";
                txtUsuarios.ForeColor = Color.LightGray;
            }
        }

        private void txtUsuarios_Leave(object sender, EventArgs e)
        {
            if (txtUsuarios.Text==""){
                txtUsuarios.Text = "USUARIO";
                txtUsuarios.ForeColor = Color.DimGray;
            }
        }

        private void txtContraseñas_Enter(object sender, EventArgs e)
        {
            if (txtContraseñas.Text == "CONTRASEÑA"){
                txtContraseñas.Text = "";
                txtContraseñas.ForeColor = Color.LightGray;
                txtContraseñas.UseSystemPasswordChar = true;
            }
        }

        private void txtContraseñas_Leave(object sender, EventArgs e)
        {
            if (txtContraseñas.Text == ""){
                txtContraseñas.Text = "CONTRASEÑA";
                txtContraseñas.ForeColor = Color.DimGray;
                txtContraseñas.UseSystemPasswordChar = false;
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
} 


