using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab_Biotec
{
    public partial class AgregarAbono : Form
    {
        public AgregarAbono()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guardar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
