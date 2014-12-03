namespace Lab_Biotec
{
    partial class PrecioAnalisis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrecioAnalisis));
            this.precios = new System.Windows.Forms.DataGridView();
            this.Analisis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.precios)).BeginInit();
            this.SuspendLayout();
            // 
            // precios
            // 
            this.precios.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.precios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.precios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Analisis,
            this.Precio});
            this.precios.Location = new System.Drawing.Point(12, 12);
            this.precios.Name = "precios";
            this.precios.Size = new System.Drawing.Size(385, 389);
            this.precios.TabIndex = 0;
            // 
            // Analisis
            // 
            this.Analisis.HeaderText = "Analisis";
            this.Analisis.Name = "Analisis";
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            // 
            // button1
            // 
            this.button1.Image = global::Lab_Biotec.Properties.Resources.button2_Image;
            this.button1.Location = new System.Drawing.Point(199, 418);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 48);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancelar";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // guardar
            // 
            this.guardar.Image = global::Lab_Biotec.Properties.Resources.aceptar;
            this.guardar.Location = new System.Drawing.Point(301, 418);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(96, 48);
            this.guardar.TabIndex = 10;
            this.guardar.Text = "Modificar";
            this.guardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.guardar.UseVisualStyleBackColor = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // PrecioAnalisis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 478);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.precios);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PrecioAnalisis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificar Precios";
            ((System.ComponentModel.ISupportInitialize)(this.precios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView precios;
        private System.Windows.Forms.DataGridViewTextBoxColumn Analisis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.Button guardar;
        private System.Windows.Forms.Button button1;
    }
}