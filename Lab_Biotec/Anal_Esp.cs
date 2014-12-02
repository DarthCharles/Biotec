using System;
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
    public partial class Anal_Esp : Form
    {
        private int numcol;
        private string nombre;
        private List<string> colNames = new List<string>();

        public Anal_Esp()
        {

            InitializeComponent();

            this.numcol = (int)col.Value;

            dataGridView1.Rows.Add("Examen");
            dataGridView1.Rows.Add("Resultado");
            dataGridView1.Rows.Add("Unidades");
            dataGridView1.Rows.Add("Rango");
        }



        public int getNumColumnas()
        {
            return numcol;
        }

        public string getNombre()
        {
            return nombre;
        }


        public void setColNames()
        {
            for (int i = 0; i < numcol; i++)
            {
                colNames.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
            }
        }

        public List<string> getColNames()
        {
            return colNames;
        }


        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {


            if (col.Value >= 0)
            {
                if (col.Value > numcol)
                {
                    dataGridView1.Rows.Add("");
                    numcol = (int)col.Value;

                }
                else
                {
                    dataGridView1.Rows.RemoveAt(numcol - 1);
                    numcol = (int)col.Value;
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nomba.Text != "")
            {
                if (numcol != 0)
                {
                    this.numcol = (int)col.Value;
                    this.nombre = nomba.Text;
                    this.setColNames();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("El análisis debe tener al menos una columna.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Por favor escriba el nombre del análisis.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
