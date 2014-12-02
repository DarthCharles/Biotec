using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab_Biotec
{
    public partial class Seleccion_Analisis : Form
    {

        String mail;
        String nombre;
        String cliente;
        String sexo;
        String edad;


        Analisis Biometria = new Biometria_Hematica();
        Analisis Multiquimica = new Multiquimica();
        Analisis EGO = new EGO();
        Analisis Coprologico3 = new Coprologico3();
        Analisis Coproparasitoscopico = new Coproparasitoscopico();
        Analisis CGHC = new CGHC();
        Analisis PCardiaco = new PCardiaco();
        Analisis PHierro = new PHierro();
        Analisis Glicohemoglobina = new Glicohemoglobina();


        public Seleccion_Analisis(String mail, String nombre, String cliente, string sexo, string edad)
        {


            InitializeComponent();
            this.cliente = cliente;
            this.mail = mail;
            this.nombre = nombre;
            this.sexo = sexo;
            this.edad = edad;

            listBox1.Items.Add(Biometria);
            listBox1.Items.Add(Multiquimica);
            listBox1.Items.Add(new P_Tiroideo());
            listBox1.Items.Add(new Coprologico());
            listBox1.Items.Add(Coproparasitoscopico);
            listBox1.Items.Add(new DAbuso());
            listBox1.Items.Add(EGO);
            listBox1.Items.Add(new ExPrenup());
            listBox1.Items.Add(Glicohemoglobina);
            listBox1.Items.Add(PCardiaco);
            listBox1.Items.Add(PHierro);
            listBox1.Items.Add(new PLipidos());
            listBox1.Items.Add(new PReumatoide());
            listBox1.Items.Add(new Plaquetas());
            listBox1.Items.Add(CGHC);
            listBox1.Items.Add(new T_Sanguineo());
            listBox1.Items.Add(new T_Coagulacion());
            listBox1.Items.Add(new PEmbarazo());
            listBox1.Items.Add(new DCO24());
            listBox1.Items.Add(new PHepatico());
            listBox1.Items.Add(new AHV());
            listBox1.Items.Add(new RFebriles());
            listBox1.Items.Add(new Reticulocitos());
            listBox1.Items.Add(new Cultivo());
            listBox1.Items.Add(new TNeonatal());
            listBox1.Items.Add(new PGinecol());
            listBox1.Items.Add(new PProstatico());
            listBox1.Items.Add(new Esperma());
            listBox1.Items.Add(Coprologico3);


            listBox1.Sorted = true;
            listBox1.FormattingEnabled = false;



            for (int i = 0; i < Resul_Analisis.ANALISIS.Count; i++)
            {

                listBox2.Items.Add(Resul_Analisis.ANALISIS[i]);
                listBox1.Items.Remove(Resul_Analisis.ANALISIS[i]);
            }
            listBox1.SelectedIndex = 0;

            if (listBox2.Items.Count != 0)
            {
                listBox1.SelectedIndex = 0;
            }
        }

        private void Seleccionar_Click(object sender, EventArgs e)
        {
            foreach (object obj in listBox1.SelectedItems)
            {
                if (!listBox2.Items.Contains(obj))
                {
                    listBox2.Items.Add(obj);
                }

            }

            if (listBox2.Items.Count == 1)
            {
                listBox2.SelectedIndex = 0;
            }
            if (listBox1.SelectedItem != null)
            {
                int selecto = listBox1.Items.IndexOf(listBox1.SelectedItem);

                listBox1.Items.RemoveAt(selecto);
                // listBox2.Sorted = true;
                if (selecto <= 0)
                {
                    if (listBox1.Items.Count != 0)
                    {
                        listBox1.SelectedIndex = 0;
                    }
                }
                else
                {
                    listBox1.SelectedIndex = selecto - 1;
                }
            }
            else
            {
                MessageBox.Show("Nada que agregar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void Deseleccionar_Click(object sender, EventArgs e)
        {
            foreach (object obj in listBox2.SelectedItems)
            {
                if (!listBox1.Items.Contains(obj))
                {
                    listBox1.Items.Add(obj);
                }
            }

            if (listBox1.Items.Count == 1)
            {
                listBox1.SelectedIndex = 0;
            }
            //MessageBox.Show("" + listBox2.Items.IndexOf(listBox2.SelectedItem));

            if (listBox2.SelectedItem != null)
            {
                int selecto = listBox2.Items.IndexOf(listBox2.SelectedItem);

                if (Resul_Analisis.ANALISIS.Contains(listBox2.Items[selecto]))
                {
                    Resul_Analisis.ANALISIS.Remove((Analisis)listBox2.Items[selecto]);
                }

                listBox2.Items.RemoveAt(selecto);


                listBox1.Sorted = true;
                if (selecto <= 0)
                {
                    if (listBox2.Items.Count != 0)
                    {
                        listBox2.SelectedIndex = 0;
                    }
                }
                else
                {
                    listBox2.SelectedIndex = selecto - 1;
                }
            }
            else
            {
                MessageBox.Show("Nada que eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void get_lista()
        {

            foreach (Analisis item in listBox2.Items)
            {

                if (!Resul_Analisis.ANALISIS.Contains(item))
                {
                    Resul_Analisis.ANALISIS.Add(item);
                    
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_lista();
            if (Resul_Analisis.ANALISIS.Count == 0)
            {
                MessageBox.Show("No hay examenes seleccionados", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string[] doctores = new string[listBox2.Items.Count];
                Resul_Analisis cu = new Resul_Analisis(mail, cliente, nombre, sexo, edad, doctores.ToList<string>());
                cu.ShowDialog();
            }
        }


        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Resul_Analisis.ANALISIS.Clear();
            this.Close();
        }

        private void Seleccion_Analisis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Resul_Analisis.ANALISIS.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Anal_Esp a = new Anal_Esp();
            a.ShowDialog();

            if (a.getNombre() != null)
            {
                listBox2.Items.Add(new Especial(a.getNumColumnas(), a.getNombre(), a.getColNames()));
                if (listBox2.SelectedIndex == -1)
                {
                    listBox2.SelectedIndex = 0;
                }
            }
        }

        public List<Analisis> listaPaquete(params Analisis[] values)
        {

            List<Analisis> paquete = new List<Analisis>();

            foreach (Analisis item in values)
            {
                paquete.Add(item);
            }

            return paquete;
        }

        public void nuevoPaquete(List<Analisis> lista)
        {

            foreach (Analisis item in listBox2.Items)
            {

                if (lista.Contains(item))
                {
                    MessageBox.Show("El análisis " + item.Name + " ya se encuentra seleccionado.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    lista.Remove(item);
                }
            }

            foreach (Analisis item in lista)
            {

                listBox2.Items.Add(item);
                if (listBox2.Items.Count == 1)
                {
                    listBox2.SelectedIndex = 0;
                }
                listBox1.Items.Remove(item);
            }


            lista.Clear();

        }

        private void básicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nuevoPaquete(listaPaquete(Biometria, EGO, Multiquimica));

        }

        private void roñososToolStripMenuItem_Click(object sender, EventArgs e)
        {
       //     nuevoPaquete(listaPaquete(Coprologico3, Coproparasitoscopico, CGHC, PCardiaco, PHierro, Glicohemoglobina));

        }
    }
}
