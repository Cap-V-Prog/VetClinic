namespace VetClinic_UI
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.SearchAnimalType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SearchFilter = new System.Windows.Forms.ComboBox();
            this.SearchTxtBox = new System.Windows.Forms.TextBox();
            this.SearchBtn = new System.Windows.Forms.Button();
            this.DelConfirm = new System.Windows.Forms.CheckBox();
            this.DelDeactiveBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.IdTxtBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AddNewUpdateBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SexM = new System.Windows.Forms.RadioButton();
            this.SexF = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(14, 14);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(956, 645);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.DelConfirm);
            this.tabPage1.Controls.Add(this.DelDeactiveBtn);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.AddNewUpdateBtn);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(948, 619);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Animais";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.SearchAnimalType);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.dataGridView1);
            this.groupBox5.Controls.Add(this.SearchFilter);
            this.groupBox5.Controls.Add(this.SearchTxtBox);
            this.groupBox5.Controls.Add(this.SearchBtn);
            this.groupBox5.Location = new System.Drawing.Point(8, 239);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(934, 374);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Consulta";
            // 
            // SearchAnimalType
            // 
            this.SearchAnimalType.DisplayMember = "0";
            this.SearchAnimalType.FormattingEnabled = true;
            this.SearchAnimalType.ItemHeight = 13;
            this.SearchAnimalType.Items.AddRange(new object[] { "Cão", "Gato", "Ave" });
            this.SearchAnimalType.Location = new System.Drawing.Point(224, 25);
            this.SearchAnimalType.Name = "SearchAnimalType";
            this.SearchAnimalType.Size = new System.Drawing.Size(500, 21);
            this.SearchAnimalType.TabIndex = 11;
            this.SearchAnimalType.ValueMember = "0";
            this.SearchAnimalType.Visible = false;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(6, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Procurar por";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 62);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(922, 306);
            this.dataGridView1.TabIndex = 3;
            // 
            // SearchFilter
            // 
            this.SearchFilter.DisplayMember = "0";
            this.SearchFilter.FormattingEnabled = true;
            this.SearchFilter.Items.AddRange(new object[] { "ID", "Nome do dono", "Contacto do dono", "Tipo de Animal" });
            this.SearchFilter.Location = new System.Drawing.Point(97, 25);
            this.SearchFilter.Name = "SearchFilter";
            this.SearchFilter.Size = new System.Drawing.Size(121, 21);
            this.SearchFilter.TabIndex = 2;
            this.SearchFilter.ValueMember = "0";
            this.SearchFilter.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // SearchTxtBox
            // 
            this.SearchTxtBox.Location = new System.Drawing.Point(224, 26);
            this.SearchTxtBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 5);
            this.SearchTxtBox.Name = "SearchTxtBox";
            this.SearchTxtBox.Size = new System.Drawing.Size(500, 20);
            this.SearchTxtBox.TabIndex = 1;
            // 
            // SearchBtn
            // 
            this.SearchBtn.Location = new System.Drawing.Point(730, 25);
            this.SearchBtn.Margin = new System.Windows.Forms.Padding(3, 3, 5, 3);
            this.SearchBtn.Name = "SearchBtn";
            this.SearchBtn.Size = new System.Drawing.Size(75, 24);
            this.SearchBtn.TabIndex = 0;
            this.SearchBtn.Text = "Procurar";
            this.SearchBtn.UseVisualStyleBackColor = true;
            this.SearchBtn.Click += new System.EventHandler(this.SearchBtn_Click);
            // 
            // DelConfirm
            // 
            this.DelConfirm.Location = new System.Drawing.Point(460, 199);
            this.DelConfirm.Name = "DelConfirm";
            this.DelConfirm.Size = new System.Drawing.Size(238, 25);
            this.DelConfirm.TabIndex = 15;
            this.DelConfirm.Text = "Remover registo do animal";
            this.DelConfirm.UseVisualStyleBackColor = true;
            this.DelConfirm.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DelDeactiveBtn
            // 
            this.DelDeactiveBtn.Location = new System.Drawing.Point(460, 148);
            this.DelDeactiveBtn.Name = "DelDeactiveBtn";
            this.DelDeactiveBtn.Size = new System.Drawing.Size(107, 45);
            this.DelDeactiveBtn.TabIndex = 14;
            this.DelDeactiveBtn.Text = "Desativar";
            this.DelDeactiveBtn.UseVisualStyleBackColor = true;
            this.DelDeactiveBtn.Click += new System.EventHandler(this.DelDeactiveBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.IdTxtBox);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.textBox2);
            this.groupBox4.Controls.Add(this.textBox6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(460, 8);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(238, 132);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Informação secundária";
            // 
            // IdTxtBox
            // 
            this.IdTxtBox.Location = new System.Drawing.Point(132, 26);
            this.IdTxtBox.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.IdTxtBox.Name = "IdTxtBox";
            this.IdTxtBox.ReadOnly = true;
            this.IdTxtBox.Size = new System.Drawing.Size(100, 20);
            this.IdTxtBox.TabIndex = 9;
            this.IdTxtBox.TextChanged += new System.EventHandler(this.IdTxtBox_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(6, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Estado";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(132, 52);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 5;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(132, 104);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Data da ultima consulta";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "ID";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(132, 78);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Data de registo";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AddNewUpdateBtn
            // 
            this.AddNewUpdateBtn.Location = new System.Drawing.Point(591, 148);
            this.AddNewUpdateBtn.Name = "AddNewUpdateBtn";
            this.AddNewUpdateBtn.Size = new System.Drawing.Size(107, 45);
            this.AddNewUpdateBtn.TabIndex = 4;
            this.AddNewUpdateBtn.Text = "Adicionar novo";
            this.AddNewUpdateBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.maskedTextBox2);
            this.groupBox3.Controls.Add(this.textBox3);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(8, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(444, 85);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informação do dono";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Contacto do dono";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maskedTextBox2.Location = new System.Drawing.Point(114, 52);
            this.maskedTextBox2.Mask = "000000000";
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(63, 22);
            this.maskedTextBox2.TabIndex = 9;
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(114, 26);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(324, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Nome do dono";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.maskedTextBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 132);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informação do animal";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SexM);
            this.groupBox2.Controls.Add(this.SexF);
            this.groupBox2.Location = new System.Drawing.Point(374, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(64, 80);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sexo";
            // 
            // SexM
            // 
            this.SexM.Location = new System.Drawing.Point(6, 19);
            this.SexM.Name = "SexM";
            this.SexM.Size = new System.Drawing.Size(52, 24);
            this.SexM.TabIndex = 0;
            this.SexM.TabStop = true;
            this.SexM.Text = "M";
            this.SexM.UseVisualStyleBackColor = true;
            // 
            // SexF
            // 
            this.SexF.Location = new System.Drawing.Point(6, 49);
            this.SexF.Name = "SexF";
            this.SexF.Size = new System.Drawing.Size(52, 24);
            this.SexF.TabIndex = 1;
            this.SexF.TabStop = true;
            this.SexF.Text = "F";
            this.SexF.UseVisualStyleBackColor = true;
            this.SexF.CheckedChanged += new System.EventHandler(this.SexF_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Data de nascimento";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(114, 105);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(324, 20);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Peso (Kg)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(114, 52);
            this.maskedTextBox1.Mask = "#####.##";
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(63, 20);
            this.maskedTextBox1.TabIndex = 7;
            this.maskedTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tipo de Animal";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "Raça";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 26);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(254, 20);
            this.textBox1.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "0";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.ItemHeight = 13;
            this.comboBox1.Items.AddRange(new object[] { "Cão", "Gato", "Ave" });
            this.comboBox1.Location = new System.Drawing.Point(114, 78);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(254, 21);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.ValueMember = "0";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(948, 619);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 673);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox SearchAnimalType;

        private System.Windows.Forms.Button DelDeactiveBtn;
        private System.Windows.Forms.Button AddNewUpdateBtn;

        private System.Windows.Forms.Label label11;

        private System.Windows.Forms.DataGridView dataGridView1;

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button SearchBtn;
        private System.Windows.Forms.TextBox SearchTxtBox;
        private System.Windows.Forms.ComboBox SearchFilter;

        private System.Windows.Forms.CheckBox DelConfirm;

        private System.Windows.Forms.Button button2;

        private System.Windows.Forms.GroupBox groupBox4;

        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox IdTxtBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label10;

        private System.Windows.Forms.Label label7;

        private System.Windows.Forms.MaskedTextBox maskedTextBox2;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.TextBox textBox3;

        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Label label3;

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;

        private System.Windows.Forms.GroupBox groupBox1;

        private System.Windows.Forms.RadioButton SexM;
        private System.Windows.Forms.RadioButton SexF;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

        #endregion
    }
}