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

namespace Lab_Biotec
{
    public partial class RegistroHecho : Form
    {
        Boolean de_resul = false;
        Conexion conexion;

        public RegistroHecho()
        {
            InitializeComponent();
            System.Media.SystemSounds.Asterisk.Play();
            iniciarConexion();
        }

        private void iniciarConexion()
        {
            try
            {
                conexion = new Conexion();
            }
            catch (MySqlException)
            {
                MessageBox.Show("ocurrió un error al intentar cone333ctarse");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inicio.llenartabla();
            this.Close();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!de_resul)
            {

                MySqlCommand cmd = new MySqlCommand("SELECT id_cliente FROM clientes ORDER BY id_cliente DESC LIMIT 1", conexion.getConnection());
                String identificador = cmd.ExecuteScalar().ToString();


                int id = int.Parse(identificador);

                Perfil perf = new Perfil(id);
                Inicio.llenartabla();

                perf.ShowDialog();
                this.Close();
            }
            else
            {
                Form[] forms = Application.OpenForms.Cast<Form>().ToArray();
                foreach (Form thisForm in forms)
                {
                    if (thisForm.Name == "Inicio" || thisForm.Name == "Perfil")
                    {
                        thisForm.Activate();
                    }
                    else
                    {
                        thisForm.Close();
                    }

                    this.Close();
                }
            }
        }

        //        cargo

        public RegistroHecho(Boolean de_resul)
        {
            InitializeComponent();
            System.Media.SystemSounds.Asterisk.Play();
            this.de_resul = de_resul;

        }

    }
}
