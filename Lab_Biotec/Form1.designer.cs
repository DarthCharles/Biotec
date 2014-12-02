namespace SeleccionExamen
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verarray = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Deseleccionar = new System.Windows.Forms.Button();
            this.Seleccionar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // verarray
            // 
            this.verarray.Location = new System.Drawing.Point(360, 492);
            this.verarray.Name = "verarray";
            this.verarray.Size = new System.Drawing.Size(75, 23);
            this.verarray.TabIndex = 4;
            this.verarray.Text = "button1";
            this.verarray.UseVisualStyleBackColor = true;
            this.verarray.Click += new System.EventHandler(this.verarray_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(442, 492);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 5;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 25;
            this.listBox1.Location = new System.Drawing.Point(30, 105);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(182, 354);
            this.listBox1.TabIndex = 6;
            // 
            // listBox2
            // 
            this.listBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 25;
            this.listBox2.Location = new System.Drawing.Point(360, 105);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(182, 354);
            this.listBox2.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SeleccionExamen.Properties.Resources.small;
            this.pictureBox1.Location = new System.Drawing.Point(343, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(199, 77);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // Deseleccionar
            // 
            this.Deseleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Deseleccionar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Deseleccionar.Image = global::SeleccionExamen.Properties.Resources.flechita1;
            this.Deseleccionar.Location = new System.Drawing.Point(254, 297);
            this.Deseleccionar.Name = "Deseleccionar";
            this.Deseleccionar.Size = new System.Drawing.Size(60, 51);
            this.Deseleccionar.TabIndex = 3;
            this.Deseleccionar.UseVisualStyleBackColor = true;
            this.Deseleccionar.Click += new System.EventHandler(this.Deseleccionar_Click);
            // 
            // Seleccionar
            // 
            this.Seleccionar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Seleccionar.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Seleccionar.Image = global::SeleccionExamen.Properties.Resources.flechita2;
            this.Seleccionar.Location = new System.Drawing.Point(254, 190);
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.Size = new System.Drawing.Size(60, 51);
            this.Seleccionar.TabIndex = 2;
            this.Seleccionar.UseVisualStyleBackColor = true;
            this.Seleccionar.Click += new System.EventHandler(this.Seleccionar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 546);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.verarray);
            this.Controls.Add(this.Deseleccionar);
            this.Controls.Add(this.Seleccionar);
            this.Name = "Form1";
            this.Text = "Selección de Análisis";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Seleccionar;
        private System.Windows.Forms.Button Deseleccionar;
        private System.Windows.Forms.Button verarray;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

