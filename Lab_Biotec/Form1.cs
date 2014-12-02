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

namespace SeleccionExamen
{
    public partial class Form1 : Form
    {


        List<string> examen = new List<string>();
        public Form1()
        {
            InitializeComponent();
         //   listBox1.SelectedIndex = 0;
            // listBox2.SelectedIndex = 0;



    



            // Ejemplos de los examenes que sacaíamos de la BD
            examen.Add("Color");
            examen.Add("Aspect");
            examen.Add("Densidad");
            examen.Add("PH");
            examen.Add("Albumina");
            examen.Add("Glucosa");
            examen.Add("Cetona");
            examen.Add("Billirubina");
            examen.Add("Leucositos");
            examen.Add("PH");
            examen.Add("Albumina");
            examen.Add("Glucosa");

            for (int i = 0; i < examen.Count; i++)
            {
                listBox1.Items.Add(examen[i]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            if (listBox1.SelectedItem != null)
            {
                int selecto = listBox1.Items.IndexOf(listBox1.SelectedItem);

                listBox1.Items.RemoveAt(selecto);
                listBox2.Sorted = true;
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


            //MessageBox.Show("" + listBox2.Items.IndexOf(listBox2.SelectedItem));

            if (listBox2.SelectedItem != null)
            {
                int selecto = listBox2.Items.IndexOf(listBox2.SelectedItem);

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


        private void verarray_Click(object sender, EventArgs e)
        {
            ArrayList list = new ArrayList();
            foreach (object o in listBox2.Items)
            {
                list.Add(o);
            }

            var seleccionados = 0;
            foreach (object u in listBox2.Items)
            {
                seleccionados++;
            }

            StringBuilder sb = new StringBuilder();

            foreach (string item in listBox2.Items)
            {

                sb.Append(item);

            }

            MessageBox.Show(sb.ToString());
            textBox1.Text = seleccionados.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
