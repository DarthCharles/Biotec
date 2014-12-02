using MySql.Data.MySqlClient;
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

namespace Lab_Biotec
{

    public partial class Historial : Form
    {
        String mail, nombre, sexo, edad;
        Conexion conexion;
        ArrayList analisisTodos = new ArrayList();
        BaseDatos bases = new BaseDatos();
        string ultimo;
        public void conectar()
        {
            try
            {
                conexion = new Conexion();
            }
            catch
            {
                MessageBox.Show("HISTORIAL 28 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void acomodar(DataGridView tabla)
        {
            System.Drawing.Font font = new System.Drawing.Font("Microsoft Sans Serif", 10.0f);// fuente                     
            tabla.Font = font;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tabla.Columns[0].Width = 40;
            tabla.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            tabla.Columns[1].Width = 90;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.ReadOnly = true;
            cambiar_nombres_tablas();
            conexion.Close();
            sort();
        }

        void llenar_tabla(string cliente)
        {
            tabla.Rows.Clear();
            ArrayList contenido = new ArrayList();
            string sql = "select idAnalisis, fecha, tipo from documento inner join clientes on documento.id_cliente = clientes.id_cliente where clientes.id_cliente = " + cliente + ";";
            MySqlCommand busqueda = new MySqlCommand(sql, conexion.getConnection());
            MySqlDataReader reader = busqueda.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    contenido.Add(reader.GetValue(i).ToString());
                }
            }
            reader.Close();

            if (tabla.ColumnCount == 0)
            {
                tabla.Columns.Add("id", "id");
                tabla.Columns.Add("fecha", "Fecha");
                tabla.Columns.Add("analisis", "Analisis");
            }
            try
            {
                for (int i = 0; i < contenido.Count; i += 3)
                {
                    DateTime dt = Convert.ToDateTime(contenido[i + 1].ToString());
                    string fecha = dt.ToString("dd/MM/yyyy");

                    tabla.Rows.Add(contenido[i].ToString(),
                                   fecha,
                                   contenido[i + 2].ToString());
                    acomodar(tabla);
                }
            }
            catch { }

        }


        
        void busqueda_por_fecha(string fecha, string cliente)
        {
            try
            {
                conectar();
                tabla.Rows.Clear();
                ArrayList contenido = new ArrayList();
                string sql = "select idAnalisis, fecha, tipo from documento inner join clientes on documento.id_cliente = clientes.id_cliente where clientes.id_cliente = " + cliente + " and documento.fecha = '" + fecha + "';";
                MySqlCommand busqueda = new MySqlCommand(sql, conexion.getConnection());
                MySqlDataReader reader = busqueda.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        contenido.Add(reader.GetValue(i).ToString());
                    }
                }
                reader.Close();

                if (tabla.ColumnCount == 0)
                {
                    tabla.Columns.Add("id", "id");
                    tabla.Columns.Add("fecha", "Fecha");
                    tabla.Columns.Add("analisis", "Analisis");
                }
                try
                {
                    for (int i = 0; i < contenido.Count; i += 3)
                    {
                        DateTime dt = Convert.ToDateTime(contenido[i + 1].ToString());
                        fecha = dt.ToString("dd/MM/yyyy");

                        tabla.Rows.Add(contenido[i].ToString(),
                                       fecha,
                                       contenido[i + 2].ToString());
                        acomodar(tabla);
                    }
                }
                catch { }

            }
            catch
            {
                MessageBox.Show("HISTORIAL 73 Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void set_table_headers()
        {
            tabla.Columns[0].HeaderText = "id";
            tabla.Columns[1].HeaderText = "Fecha";
            tabla.Columns[2].HeaderText = "Tipo";
        }
        void cambiar_nombres_tablas()
        {
            for (int i = 0; i < tabla.RowCount; i++)
            {
                string lol = tabla.Rows[i].Cells[2].Value.ToString();
                tabla.Rows[i].Cells[2].Value = set_tabla(lol);
            }
        }

        string set_tabla(string tipo)
        {
            switch (tipo)
            {
                case "tcoag":
                    return "Tiempos de Coagulación";

                case "tsang":
                    return "Tipeo Sanguíneo";

                case "glh":
                    return "Glicohemoglobina";

                case "copropara":
                    return "Coproparasitoscópico";

                case "bh":
                    return "Biometría Hemática";

                case "ego":
                    return "Examen Gral. de Orina";

                case "mq":
                    return "Multiquímica";

                case "ptiroideo":
                    return "Perfil Tiroideo";

                case "pembarazo":
                    return "Cuantificación de HGC";

                case "copro":
                    return "Coprológico";

                case "copro3":
                    return "Coprológico III";

                case "dabuso":
                    return "Drogas de Abuso";

                case "preumatoide":
                    return "Perfil Reumatoide";

                case "pcardiaco":
                    return "Perfil Cardiaco";

                case "phierro":
                    return "Perfil de Hierro";

                case "plipidos":
                    return "Perfil de Lípidos";

                case "plaquetas":
                    return "Plaquetas";

                case "expre":
                    return "Examen Prenupcial";

                case "dcreat":
                    return "Depuración de Creatinina";

                case "phepatico":
                    return "Perfil Hepático";

                case "hepat":
                    return "Anti-Hepatitis Viral";

                case "reac":
                    return "Reacciones Febriles";

                case "retic":
                    return "Reticulocitos";

                case "pembarazo2":
                    return "Prueba Inmunológica de Embarazo en Sangre";

                case "gineco":
                    return "Perfil Ginecológico";

                case "prosta":
                    return "Perfil Prostático";

                case "tamiz":
                    return "Tamiz Neonatal";

                case "cultivo":
                    return "Cultivo";

                case "esperma":
                    return "Espermatobioscopía";

                default:
                    break;
            }
            return tipo;
        }


        static MySqlDataAdapter mySqlDataAdapter;
        static string cliente_global;

        public Historial(String cliente, String mail, String nombre, String sexo, String edad)
        {
            InitializeComponent();
            timer1.Start();
            analisisTodos.Add(new Biometria_Hematica());
            analisisTodos.Add(new Multiquimica());
            analisisTodos.Add(new P_Tiroideo());
            analisisTodos.Add(new Coprologico());
            analisisTodos.Add(new Coproparasitoscopico());
            analisisTodos.Add(new DAbuso());
            analisisTodos.Add(new EGO());
            analisisTodos.Add(new ExPrenup());
            analisisTodos.Add(new Glicohemoglobina());
            analisisTodos.Add(new PCardiaco());
            analisisTodos.Add(new PHierro());
            analisisTodos.Add(new PLipidos());
            analisisTodos.Add(new PReumatoide());
            analisisTodos.Add(new Plaquetas());
            analisisTodos.Add(new CGHC());
            analisisTodos.Add(new T_Sanguineo());
            analisisTodos.Add(new T_Coagulacion());
            analisisTodos.Add(new PEmbarazo());
            analisisTodos.Add(new DCO24());
            analisisTodos.Add(new PHepatico());
            analisisTodos.Add(new AHV());
            analisisTodos.Add(new RFebriles());
            analisisTodos.Add(new Reticulocitos());
            analisisTodos.Add(new Cultivo());
            analisisTodos.Add(new TNeonatal());
            analisisTodos.Add(new PProstatico());
            analisisTodos.Add(new PGinecol());
            analisisTodos.Add(new Esperma());
            analisisTodos.Add(new Coprologico3());

            ultimo = bases.ultimo_analisis();
            label1.Text = "Análisis practicados a " + nombre;
            this.mail = mail;
            this.nombre = nombre;
            this.edad = edad;
            this.sexo = sexo;
            cliente_global = cliente;
            conectar();
            llenar_tabla(cliente);
            set_table_headers();
            tabla.AutoResizeColumns();
            conexion.Close();
            sort();

        }


        private void button1_Click(object sender, EventArgs e)
        {

            Resul_Analisis.ANALISIS.Clear();
            try
            {
                if (tabla.SelectedCells.Count != 0)
                {
                    ArrayList idanalisis = new ArrayList();
                    ArrayList tipo = new ArrayList();

                    try
                    {
                        DataGridViewSelectedCellCollection seleccionados = tabla.SelectedCells;

                        foreach (DataGridViewCell cell in seleccionados)
                        {
                            foreach (Analisis analisis in analisisTodos)
                            {
                                if (cell.Value.ToString().Equals(analisis.Name))
                                {
                                    Resul_Analisis.ANALISIS.Add(analisis.Clone());
                                }
                            }

                            for (int i = 0; i < seleccionados.Count; i += 3)
                            {
                                idanalisis.Add(seleccionados[i].Value.ToString());
                                tipo.Add(ver_tabla(seleccionados[i + 2].Value.ToString()));
                            }

                        }
                        idanalisis.Reverse();
                        tipo.Reverse();
                        Resul_Analisis.ANALISIS.Reverse();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string[] doctores = new string[Resul_Analisis.ANALISIS.Count];
                   
                        Resul_Analisis detalles = new Resul_Analisis(idanalisis, tipo, mail, nombre, sexo, edad, doctores.ToList<string>());
                        detalles.validacion = false;
                        detalles.ShowDialog();
                        Resul_Analisis.ANALISIS.Clear();
                  
                }
                else
                {
                    MessageBox.Show("No se han seleccionado análisis", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "Ocurrió un error en la base de datos, por favor, compruebe la conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static string ver_tabla(string tipo)
        {
            switch (tipo)
            {
                case "Glicohemoglobina":
                    return "glh";

                case "Tiempos de Coagulación":
                    return "tcoag";

                case "Tipeo Sanguíneo":
                    return "tsang";

                case "Coproparasitoscópico":
                    return "copropara";

                case "Biometría Hemática":
                    return "bh";

                case "Examen Gral. de Orina":
                    return "ego";

                case "Multiquímica":
                    return "mq";

                case "Perfil Tiroideo":
                    return "ptiroideo";

                case "Prueba Inmunológica de Embarazo en Sangre":
                    return "pembarazo2";

                case "Coprológico":
                    return "copro";

                case "Drogas de Abuso":
                    return "dabuso";

                case "Perfil Reumatoide":
                    return "preumatoide";

                case "Perfil Cardiaco":
                    return "pcardiaco";

                case "Perfil de Hierro":
                    return "phierro";

                case "Perfil de Lípidos":
                    return "plipidos";

                case "Plaquetas":
                    return "plaquetas";

                case "Examen Prenupcial":
                    return "expre";

                case "Cuantificación de HGC":
                    return "pembarazo";

                case "Depuración de Creatinina":
                    return "dcreat";

                case "Perfil Hepático":
                    return "phepatico";

                case "Anti-Hepatitis Viral":
                    return "hepat";

                case "Reacciones Febriles":
                    return "reac";

                case "Reticulocitos":
                    return "retic";

                case "Perfil Ginecológico":
                    return "gineco";

                case "Perfil Prostático":
                    return "prosta";

                case "Tamiz Neonatal":
                    return "tamiz";

                case "Cultivo":
                    return "cultivo";

                case "Espermatobioscopía":
                    return "esperma";

                case "Coprológico III":
                    return "copro3";


                default:
                    MessageBox.Show("No se encontró la tabla en la base de datos");
                    break;
            }
            return tipo;
        }


        void calendario_ValueChanged(object sender, EventArgs e)
        {
            string fecha_selec = calendario.Value.ToString("yyyy-MM-dd");
            busqueda_por_fecha(fecha_selec, cliente_global);
            if (tabla.RowCount == 0)
            {
                tabla.Hide();
            }
            else
            {
                tabla.Show();
                sort();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (tabla.Visible == false)
            {
                tabla.Visible = true;
            }
            conectar();
            llenar_tabla(cliente_global);
            set_table_headers();
            conexion.Close();
            sort();
        }

        private void sort()
        {
            tabla.Sort(tabla.Columns[0], ListSortDirection.Descending);
        }

        private void select_todos_Click(object sender, EventArgs e)
        {
            tabla.ClearSelection();
            tabla.SelectAll();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ultimo) != int.Parse(bases.ultimo_analisis()))
                {
                    if (conexion.getConnection().State == ConnectionState.Closed)
                    {
                        conexion.getConnection().Open();
                    }
                    llenar_tabla(cliente_global);
                    ultimo = bases.ultimo_analisis();
                }
            }
            catch { }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("¿Está seguro de querer borrar los análisis seleccionados?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                conectar();
                try
                {
                    DataGridViewSelectedCellCollection seleccionados = tabla.SelectedCells;

                    for (int i = 0; i < seleccionados.Count; i += 3)
                    {
                        conexion.delete("documento", seleccionados[i].Value.ToString());
                    }


                    if (tabla.Visible == false)
                    {
                        tabla.Visible = true;
                    }

                    conectar();
                    llenar_tabla(cliente_global);
                    set_table_headers();
                    conexion.Close();
                    sort();
                    tabla.ClearSelection();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string val1 = tabla.SelectedRows[0].Cells[0].Value.ToString();
                string val2 = tabla.SelectedRows[0].Cells[1].Value.ToString();
                string val3 = tabla.SelectedRows[0].Cells[2].Value.ToString();

                int index = tabla.SelectedCells[0].RowIndex;

                tabla.Rows[index].Cells[0].Value = tabla.Rows[index - 1].Cells[0].Value;
                tabla.Rows[index].Cells[1].Value = tabla.Rows[index - 1].Cells[1].Value;
                tabla.Rows[index].Cells[2].Value = tabla.Rows[index - 1].Cells[2].Value;

                tabla.Rows[index - 1].Cells[0].Value = val1;
                tabla.Rows[index - 1].Cells[1].Value = val2;
                tabla.Rows[index - 1].Cells[2].Value = val3;

                tabla.Rows[index].Selected = false;
                tabla.Rows[index - 1].Selected = true;

            }
            catch { }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string val1 = tabla.SelectedRows[0].Cells[0].Value.ToString();
                string val2 = tabla.SelectedRows[0].Cells[1].Value.ToString();
                string val3 = tabla.SelectedRows[0].Cells[2].Value.ToString();

                int index = tabla.SelectedCells[0].RowIndex;

                tabla.Rows[index].Cells[0].Value = tabla.Rows[index + 1].Cells[0].Value;
                tabla.Rows[index].Cells[1].Value = tabla.Rows[index + 1].Cells[1].Value;
                tabla.Rows[index].Cells[2].Value = tabla.Rows[index + 1].Cells[2].Value;

                tabla.Rows[index + 1].Cells[0].Value = val1;
                tabla.Rows[index + 1].Cells[1].Value = val2;
                tabla.Rows[index + 1].Cells[2].Value = val3;

                tabla.Rows[index].Selected = false;
                tabla.Rows[index + 1].Selected = true;

            }
            catch { }
        }
    }
}