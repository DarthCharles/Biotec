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

    public partial class NuevoCliente : Form
    {

        Conexion conexion;
        public NuevoCliente(Boolean nuevo, int id)
        {
            InitializeComponent();
            iniciarConexion();
            this.nuevo = nuevo;
            this.id = id;
            cbmes.MaxDropDownItems = 10;

            for (int i = 2105; i >= 1920; i--)
            {
                cbaño.Items.Add(i);
            }
            
            if (nuevo == false)
            {
                this.Text = "Editar datos de cliente";
                llenarcampos();
            }
            else
            {
                this.Text = "Ingreso de nuevo cliente";
            }
        }
        Boolean nuevo;
        int id;
        private void iniciarConexion()
        {
            try
            {
                conexion = new Conexion();
            }
            catch (MySqlException)
            {

                MessageBox.Show("ocurrió un error al intentar conectarse", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void llenarcampos()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT nombre FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox1.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT apellido_paterno FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox2.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT apellido_materno FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox3.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT telefono FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox4.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT correo FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox5.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT domicilio FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            textBox6.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT sexo FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            cbsexo.Text = cmd.ExecuteScalar().ToString();
            cmd = new MySqlCommand("SELECT fechanac FROM clientes WHERE id_cliente='" + id + "'", conexion.getConnection());
            string[] words = cmd.ExecuteScalar().ToString().Split(' ');
            cbdia.Text = words[0];
            cbmes.Text = words[2];
            cbaño.Text = words[4];



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
                    return days.ToString() + " dias";
                else
                    return months.ToString() + " meses";
            }
            return years.ToString() + "anos";
        } 
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                TextBox[] arrText = new TextBox[] { textBox1, textBox2, textBox3};
                Boolean completo = true;

                
                for (int i = 0; i < arrText.Length; i++)
                {
                    if (arrText[i].Text == String.Empty)
                    {
                        completo = false;
                    }
                }
                if (cbsexo.Text == "")
                {
                   completo = false; 
                }
            
                if (completo == true)
                {
                    MySqlCommand cmd = new MySqlCommand();

                    if (!validacionfecha())
                    {
                        
                      
                        MessageBox.Show("Por favor ingrese una fecha válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (nuevo == true)
                        {
                            cmd.CommandText = "INSERT INTO clientes (nombre, apellido_paterno, apellido_materno, telefono, correo, domicilio, sexo, fechanac) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + cbsexo.Text + "','" + cbdia.Text + " de " + cbmes.Text + " de " +cbaño.Text+"');";
                            cmd.Connection = conexion.getConnection();
                            cmd.ExecuteNonQuery();
                            this.Close();
                            RegistroHecho reg = new RegistroHecho();
                            reg.Show();

                        }
                        else
                        {
                            cmd.CommandText = "UPDATE clientes SET nombre='" + textBox1.Text + "', apellido_paterno='" + textBox2.Text + "', apellido_materno='" + textBox3.Text + "', telefono='" + textBox4.Text + "', correo='" + textBox5.Text + "', domicilio ='" + textBox6.Text + "', sexo ='" + cbsexo.Text + "', fechanac ='" + cbdia.Text + " de " + cbmes.Text + " de " + cbaño.Text + "' WHERE id_cliente='" + id + "'";
                            cmd.Connection = conexion.getConnection();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Cliente modificado con éxito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            Inicio.llenartabla();
                        }
                    }

                }
                else
                {
                    
                    MessageBox.Show("Por favor llene todos los campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("No se pudo guardar, error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
           

        }
        private bool validacionfecha()
        {

            if (cbdia.Text == "" || cbmes.Text == "" || cbdia.Text == "")
            {
                return false;
            }

            int mes = cbmes.SelectedIndex + 1;
            string date = cbdia.Text + "/" + mes + "/" + cbaño.Text;
            DateTime bday = Convert.ToDateTime(date);

            DateTime today = DateTime.Today;
            if (calculateAge(bday, today).Contains("-"))
            {

                MessageBox.Show("Su cliente aún no ha nacido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            } 
           

            string a = cbdia.Text + cbmes.Text;
            switch (a)

            {
                case "30Febrero":
                    return false;

                case "31Febrero":
                   
                    return false;

                case "31Abril":
                    return false;


                case "31Junio":
                    return false;

                case "31Septiembre":
                    return false;

                case "31Noviembre":
                    return false;
                default:
                    return true;

            }

        }

    }
}
