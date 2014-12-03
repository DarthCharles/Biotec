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
    public partial class Perfil : Form
    {
        Conexion conexion;

        public Perfil(int id)
        {
            InitializeComponent();
            this.id = id;
            this.identificador = id.ToString();
            llenar_perfil();
        }

        int id;
        String nombre;
        String identificador;
        String apellidop;
        String apellidom;
        String tel;
        String correo;
        String domicilio;
        String sexo;
        String fecha;
        string edad;
        private void llenar_perfil()
        {
            try
            {
                iniciarConexion();
                MySqlCommand cmd = new MySqlCommand("SELECT nombre FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                nombre = cmd.ExecuteScalar().ToString();
                cnombre.Text = nombre;
                cmd = new MySqlCommand("SELECT apellido_paterno FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                apellidop = cmd.ExecuteScalar().ToString();
                capellido.Text = apellidop;
                cmd = new MySqlCommand("SELECT apellido_materno FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                apellidom = cmd.ExecuteScalar().ToString();
                capellidoma.Text = apellidom;
                cmd = new MySqlCommand("SELECT telefono FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                tel = cmd.ExecuteScalar().ToString();
                ctelefono.Text = tel;
                cmd = new MySqlCommand("SELECT correo FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                correo = cmd.ExecuteScalar().ToString();
                ccorreo.Text = correo;
                cmd = new MySqlCommand("SELECT domicilio FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                domicilio = cmd.ExecuteScalar().ToString();
                label10.Text = domicilio;
                cmd = new MySqlCommand("SELECT sexo FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                sexo = cmd.ExecuteScalar().ToString();
                label1.Text = sexo;
                cmd = new MySqlCommand("SELECT fechanac FROM clientes WHERE id_cliente='" + identificador + "'", conexion.getConnection());
                fecha = cmd.ExecuteScalar().ToString();
                label8.Text = fecha;

                string[] words = fecha.ToString().Split(' ');

                String date = words[0] + "/" + words[2] + "/" + words[4];
                DateTime bday = Convert.ToDateTime(date);
                DateTime today = DateTime.Today;
                edad = calculateAge(bday, today);
                label12.Text = edad;
                
                conexion.Close();
            }
            catch
            {
                MessageBox.Show("PERFIL 58 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private string calculateAge(DateTime birthDate, DateTime now)
        {

            birthDate = birthDate.Date;
            now = now.Date;

            var days = now.Day - birthDate.Day;
            if (days < 0)
            {
                var newNow = now.AddMonths(-1);
                days += (int)(now - newNow).TotalDays;
                now = newNow;
            }
            var months = now.Month - birthDate.Month;
            if (months < 0)
            {
                months += 12;
                now = now.AddYears(-1);
            }
            var years = now.Year - birthDate.Year;
            if (years == 0)
            {
                if (months == 0)
                    return days.ToString() + " días";
                else
                    return months.ToString() + " meses";
            }
            return years.ToString() + " años";
        }

        private void iniciarConexion()
        {
            try
            {
                conexion = new Conexion();
            }
            catch (MySqlException)
            {
                MessageBox.Show("PERFIL 73 Ocurrió un error al intentar conectarse a la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void cerrar()
        {
            this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Está a punto de eliminar toda la información del cliente de la base de datos. ¿Desea continuar?", "Borrar cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    iniciarConexion();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandText = "DELETE FROM clientes WHERE id_cliente='" + id + "'";
                    cmd.Connection = conexion.getConnection();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cliente eliminado con éxito", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Inicio.llenartabla();
                    conexion.Close();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("PERFIL 101 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean n = false;
                NuevoCliente ed = new NuevoCliente(n, id);
                ed.ShowDialog();
                llenar_perfil();
            }
            catch
            {
                MessageBox.Show("PERFIL 118 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            String completo = nombre + " " + apellidop + " " + apellidom;
            Seleccion_Analisis s = new Seleccion_Analisis(correo, completo, identificador,sexo,edad);
            s.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("PERFIL 133 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //try
            //{
                iniciarConexion();
                string sql = "select * from documento where id_cliente = " + id + ";";
                MySqlCommand busqueda = new MySqlCommand(sql, conexion.getConnection());
                MySqlDataReader reader = busqueda.ExecuteReader();
                reader.Read();


                if (reader.HasRows)
                {
                    String completo = nombre + " " + apellidop + " " + apellidom;
                    conexion.Close();
                    Historial historial = new Historial(identificador + "", correo, completo, sexo, edad);
                    historial.ShowDialog();
                    conexion.Close();
                    reader.Close();
                }
                else
                {
                    MessageBox.Show("No existen registros de análisis", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conexion.Close();
                }
            //}
            //catch
            //{
            //    MessageBox.Show("PERFIL 170 Ocurrió un error en la base de datos, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DetallesDeudas DD = new DetallesDeudas();
            DD.ShowDialog();
        }
    }
}
