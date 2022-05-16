using System;
using System.Collections;
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
using Microsoft.Office.Interop.Word;


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
        
        ArrayList Producto = new ArrayList();
        ArrayList Stock = new ArrayList();

        ArrayList Nombre = new ArrayList();
        ArrayList Cantidad = new ArrayList();

        private void MostrarDatos()
        {
            dgvVentas.DataSource = CapaLogica.Datos.Obtener();
            decimal total = 0;

            foreach (DataGridViewRow row in dgvVentas.Rows)
            {
                total += int.Parse(row.Cells["Valor"].Value.ToString());
            }
            textBox1.Text = total.ToString();
        }
        private void Home_Load(object sender, EventArgs e)
        {
            dgvVentas.DataSource = CapaLogica.Datos.Obtener();
            MostrarDatos();
            ListadoProductos();
            ListadoProductosByChartBar();
        }

        private void Crear_Click(object sender, EventArgs e)
        {
            int? Id = null;
            RegistroDatos formcrea = new RegistroDatos(ide, Id);
            formcrea.ShowDialog();
            MostrarDatos();
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            int? Id = int.Parse(dgvVentas.Rows[dgvVentas.CurrentRow.Index].Cells[0].Value.ToString());
            if (Id != null)
            {
                RegistroDatos edit = new RegistroDatos(ide, Id);
                edit.ShowDialog();
                MostrarDatos();
            }
        }

        private void Eliminar_Click(object sender, EventArgs e)
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

        private void ExportaraExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgvVentas);
        }
        public void ExportToExcel(DataGridView listado)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Application.Workbooks.Add(true);
            int indexColumn = 0;
            foreach (DataGridViewColumn columna in listado.Columns)
            {
                indexColumn++;
                app.Cells[1, indexColumn] = columna.Name;
            }
            int indexRow = 0;
            foreach (DataGridViewRow fila in listado.Rows)
            {
                indexRow++;
                indexRow = 0;
                foreach (DataGridViewColumn columna in listado.Columns)
                {
                    indexColumn++;
                    app.Cells[indexRow + 1, indexColumn] = fila.Cells[columna.Name].Value;
                }
            }
            app.Visible = true;
        }

        private void ExportaraWord_Click(object sender, EventArgs e)
        {
            ExportToWord(dgvVentas);
        }

        public void ExportToWord(DataGridView listado)
        {
            var app = new Microsoft.Office.Interop.Word.Application();
            var doc = app.Documents.Add();

            object missing = System.Reflection.Missing.Value;

            Table tbl = doc.Tables.Add(doc.Content, listado.RowCount + 1, listado.ColumnCount);

            for (var columna = 0; columna < listado.ColumnCount; columna++)
            {
                tbl.Cell(1, columna).Range.Text = listado.Columns[columna].HeaderText;
            }
            for (var fila = 0; fila < listado.RowCount; fila++)
            {
                for (var columna = 0; columna < listado.ColumnCount; columna++)
                {
                    tbl.Cell(fila + 2, columna + 1).Range.Text = listado.Rows[fila].Cells[columna].FormattedValue.ToString();
                }
            }
            tbl.set_Style(WdBuiltinStyle.wdStyleTableLightListAccent1);
            app.Visible = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void ListadoProductos()
        {
            var lista = Datos.Obtener();

            foreach (var listado in lista)
            {
                Producto.Add(listado.Producto);
                Stock.Add(listado.Stock);
            }
            chart1.Series[0].Points.DataBindXY(Producto, Stock);
        }

        public void ListadoProductosByChartBar()
        {
            var lista = Datos.Obtener();

            foreach (var listado in lista)
            {
                Nombre.Add(listado.Producto);
                Cantidad.Add(listado.Stock);
            }
            chart2.Series[0].Points.DataBindXY(Nombre, Cantidad);
        }

        private void btnGotoLogin_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            this.Hide();
        }

        /* private void Ir_a_Admin_Click(object sender, EventArgs e)
         {
             HacerFiltro hacerfiltro = new HacerFiltro();
             hacerfiltro.Show();
             this.Hide();
         }*/










        /*private void button5_Click(object sender, EventArgs e)
        {
           /* decimal total = 0;

            foreach (DataGridViewRow row in dgvVentas.Rows)
            {
                total += int.Parse(row.Cells["Valor"].Value.ToString());
            }
            textBox1.Text = total.ToString();
        }*/


    }
}
