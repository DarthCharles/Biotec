namespace Lab_Biotec
{
    partial class Resul_Analisis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Resul_Analisis));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbdoctor = new System.Windows.Forms.TextBox();
            this.tbtitulo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.guardar = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.b_correo = new System.Windows.Forms.Button();
            this.b_pdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Location = new System.Drawing.Point(13, 100);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(807, 389);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(608, 522);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(201, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 9;
            // 
            // tbdoctor
            // 
            this.tbdoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbdoctor.Location = new System.Drawing.Point(153, 57);
            this.tbdoctor.Name = "tbdoctor";
            this.tbdoctor.Size = new System.Drawing.Size(268, 22);
            this.tbdoctor.TabIndex = 13;
            this.tbdoctor.Text = "A QUIEN CORRESPONDA";
            this.tbdoctor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbdoctor_KeyUp);
            // 
            // tbtitulo
            // 
            this.tbtitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbtitulo.Location = new System.Drawing.Point(88, 57);
            this.tbtitulo.Name = "tbtitulo";
            this.tbtitulo.Size = new System.Drawing.Size(59, 22);
            this.tbtitulo.TabIndex = 12;
            this.tbtitulo.Text = "SR. DR.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(85, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Doctor al que se dirige:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Image = global::Lab_Biotec.Properties.Resources.mail;
            this.button2.Location = new System.Drawing.Point(251, 505);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 61);
            this.button2.TabIndex = 15;
            this.button2.Text = "Generar Sobre";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Image = global::Lab_Biotec.Properties.Resources.sin_formato;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(131, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 61);
            this.button1.TabIndex = 14;
            this.button1.Text = "Generar PDF sin Formato";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // guardar
            // 
            this.guardar.Image = global::Lab_Biotec.Properties.Resources.button1_Image;
            this.guardar.Location = new System.Drawing.Point(489, 504);
            this.guardar.Name = "guardar";
            this.guardar.Size = new System.Drawing.Size(113, 61);
            this.guardar.TabIndex = 8;
            this.guardar.Text = "Guardar";
            this.guardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.guardar.UseVisualStyleBackColor = true;
            this.guardar.Click += new System.EventHandler(this.guardar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Lab_Biotec.Properties.Resources.small;
            this.pictureBox2.Location = new System.Drawing.Point(609, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(206, 61);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Lab_Biotec.Properties.Resources.flechita1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 50);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // b_correo
            // 
            this.b_correo.Image = global::Lab_Biotec.Properties.Resources.sendmailo;
            this.b_correo.Location = new System.Drawing.Point(370, 504);
            this.b_correo.Name = "b_correo";
            this.b_correo.Size = new System.Drawing.Size(113, 61);
            this.b_correo.TabIndex = 5;
            this.b_correo.Text = "Enviar por correo";
            this.b_correo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b_correo.UseVisualStyleBackColor = true;
            this.b_correo.Click += new System.EventHandler(this.b_correo_Click);
            // 
            // b_pdf
            // 
            this.b_pdf.Image = global::Lab_Biotec.Properties.Resources.pdf;
            this.b_pdf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.b_pdf.Location = new System.Drawing.Point(11, 505);
            this.b_pdf.Name = "b_pdf";
            this.b_pdf.Size = new System.Drawing.Size(114, 61);
            this.b_pdf.TabIndex = 4;
            this.b_pdf.Text = "Generar PDF";
            this.b_pdf.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.b_pdf.UseVisualStyleBackColor = true;
            this.b_pdf.Click += new System.EventHandler(this.b_pdf_Click);
            // 
            // Resul_Analisis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 577);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbdoctor);
            this.Controls.Add(this.tbtitulo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.guardar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.b_correo);
            this.Controls.Add(this.b_pdf);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Resul_Analisis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Análisis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Resul_Analisis_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button b_pdf;
        private System.Windows.Forms.Button b_correo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button guardar;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox tbdoctor;
        private System.Windows.Forms.TextBox tbtitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

