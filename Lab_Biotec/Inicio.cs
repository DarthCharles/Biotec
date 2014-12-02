using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Threading;

namespace Lab_Biotec
{

    public partial class Inicio : Form
    {

        static Conexion conexion;
        string ultimo;

        public Inicio()
        {
            InitializeComponent();

            tablaclientes.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10);
            string path = @"C:\Lab_Biotec\Respaldo";
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            iniciarConexion();

            consulta = consul.Text;
            llenartabla();
           
                ultimo = conexion.count();
            
            timer1.Start();
        }
        String consulta;
        private void iniciarConexion()
        {
            try
            {
                conexion = new Conexion();

            }
            catch (MySqlException)
            {

                MessageBox.Show("INICIO 61 Ocurrió un error al intentar conectarse a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public TextBox TextBox1
        {
            get
            {
                return consul;
            }
        }



        private void configuraciónToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Configuraciones con = new Configuraciones();
            con.ShowDialog();

        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About au = new About();
            au.ShowDialog();
        }

        public static void llenartabla()
        {
            try
            {
                MySqlDataAdapter mySqlDataAdapter;
                DataSet DS = new DataSet();


                mySqlDataAdapter = new MySqlDataAdapter("select id_cliente, nombre, apellido_paterno, apellido_materno, telefono from clientes order by id_cliente desc", conexion.getConnection());
                DataSet DSi = new DataSet();
                mySqlDataAdapter.Fill(DSi);

                tablaclientes.DataSource = DSi.Tables[0];


                set_table_headers();
            }
            catch (Exception)
            {

                MessageBox.Show(" Ocurrió un error al intentar llenar la tabla, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void button4_Click(object sender, EventArgs e)
        {

        }



        private void consul_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MySqlDataAdapter mySqlDataAdapter;
                DataSet DS = new DataSet();

                //ObtenerTextbox();

                this.consulta = consul.Text;
                String[] ANALISIS = consulta.Split();
                if (ANALISIS.Length == 2)
                {
                    mySqlDataAdapter = new MySqlDataAdapter("select id_cliente, nombre, apellido_paterno, apellido_materno, telefono  from clientes where nombre like '%" + ANALISIS[0] + "%' AND apellido_paterno like '%" + ANALISIS[1] + "%' or nombre like '%" + consulta + "%' or apellido_paterno like '%" + consulta + "%' order by id_cliente desc", conexion.getConnection());

                }
                else
                {
                    mySqlDataAdapter = new MySqlDataAdapter("select id_cliente, nombre, apellido_paterno, apellido_materno, telefono from clientes where nombre like '%" + consulta + "%' or apellido_paterno like '%" + consulta + "%' order by id_cliente desc", conexion.getConnection());
                }
                // mySqlDataAdapter = new MySqlDataAdapter("select * from clientes having nombre like '%"+consulta+"%'", connection);

                mySqlDataAdapter.Fill(DS);
                tablaclientes.DataSource = DS.Tables[0];

                set_table_headers();
            }
            catch
            {
                MessageBox.Show("Error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static void set_table_headers()
        {
            tablaclientes.Columns[0].HeaderText = "Id";
            tablaclientes.Columns[1].HeaderText = "Nombre";
            tablaclientes.Columns[2].HeaderText = "Apellido Paterno";
            tablaclientes.Columns[3].HeaderText = "Apellido Materno";
            tablaclientes.Columns[4].HeaderText = "Telefono";




            tablaclientes.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;



            tablaclientes.Columns[0].Width = 40;


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Boolean n = true;
            NuevoCliente nuevo = new NuevoCliente(n, 0);
            nuevo.ShowDialog();
            Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
            foreach (Form thisForm in forms)
            {
                if (thisForm.Name == "RegistroHecho")
                {
                    thisForm.Activate();
                }
            }
            //this.Hide();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                llenartabla();
            }
            catch
            {


                MessageBox.Show("INICIO 209 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            conexion.Close();
            Application.Exit();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (tablaclientes.SelectedRows.Count != 0)
            {
                String valorid = tablaclientes.CurrentRow.Cells[0].Value.ToString();
                int id = int.Parse(valorid);
                Perfil perf = new Perfil(id);

                perf.ShowDialog();

            }
            else
            {
                MessageBox.Show("No hay clientes que mostrar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            consul.Text = "";
        }

        private void maToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("Manual de Usuario.pdf");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
                if (int.Parse(ultimo) != int.Parse(conexion.count()))
                {
                    Inicio.llenartabla();
                    ultimo = conexion.count();
                }
        }


    }
}
