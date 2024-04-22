using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VetClinic_UI
{
    public partial class Form1 : Form
    {
        private bool EraseMode = false;
        private bool UpdateMode = false;
        public Form1()
        {
            InitializeComponent();
            IdTxtBox_TextChanged(null,null);
        }

        private void SexF_CheckedChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (DelConfirm.Checked)
            {
                MessageBox.Show("Atencão esta opção permite remover completamente um registo da base de dados!",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DelDeactiveBtn.Text = "Remover registo";
                EraseMode = true;
            }
            else
            {
                DelDeactiveBtn.Text = "Desativar";
                EraseMode = false;
            }
        }

        private void DelDeactiveBtn_Click(object sender, EventArgs e)
        {
            if (EraseMode)
            {
                
                DialogResult choice = MessageBox.Show("Tem a certesa que quer apagar definitivamente este registo! (esta ação nao pode ser revertida)",
                    "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (choice == DialogResult.Yes)
                {
                    //Apagar
                    MessageBox.Show("Registo apagado com sucesso!",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Cancelar
                    MessageBox.Show("Ação cancelada!",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //Desativar
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (SearchFilter.SelectedIndex)
            {
                case 3:
                    SearchTxtBox.Visible = false;
                    SearchAnimalType.Visible = true;
                    break;
                default:
                    SearchTxtBox.Visible = true;
                    SearchAnimalType.Visible = false;
                    break;
            }
        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void IdTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdTxtBox.Text))
            {
                SetUpdateMode(false);
            }
            else
            {
                SetUpdateMode(true);
            }
        }

        private void SetUpdateMode(bool updatemode)
        {
            if (updatemode)
            {
                //Update 
                AddNewUpdateBtn.Text = "Atualizar registo";
                AddNewUpdateBtn.Size = new Size(107, 45); //Default
                AddNewUpdateBtn.Location = new Point(591, 148); // Default
                
                UpdateMode = true;
                DelDeactiveBtn.Visible = true;
                DelConfirm.Visible = true;
            }
            else
            {
                //Add New
                AddNewUpdateBtn.Text = "Adicionar novo";
                AddNewUpdateBtn.Size = new Size(238, 45); // Altered
                AddNewUpdateBtn.Location = new Point(460, 148); // Altered
                
                UpdateMode = false;
                DelDeactiveBtn.Visible = false;
                DelConfirm.Visible = false;
            }
        }
    }
}