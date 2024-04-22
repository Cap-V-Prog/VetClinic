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
        MySQLConnectionManager connectionManager = new MySQLConnectionManager("vetclinic");
        
        private AnimalManager animalManager;
        
        public Form1()
        {
            InitializeComponent();
            IdTxtBox_TextChanged(null,null);
            SexM.Checked = true;
            AnimalTypeTxt.SelectedIndex=0;
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
                    connectionManager = new MySQLConnectionManager("vetclinic");
                    animalManager = new AnimalManager(connectionManager);
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
                connectionManager = new MySQLConnectionManager("vetclinic");
                animalManager = new AnimalManager(connectionManager);
                //Desativar
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAnimalType.SelectedIndex = 0;
            SearchTxtBox.Clear();
            
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
            connectionManager = new MySQLConnectionManager("vetclinic");
            animalManager = new AnimalManager(connectionManager);
            
            if (SearchFilter.SelectedIndex == 3)
            {
                dataGridView1.DataSource=animalManager.SearchAnimals(3, SearchAnimalType.Text);
            }
            else
            {
                dataGridView1.DataSource=animalManager.SearchAnimals(SearchFilter.SelectedIndex, SearchTxtBox.Text);
            }
            
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

        private void AddNewUpdateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connectionManager = new MySQLConnectionManager("vetclinic");
                animalManager = new AnimalManager(connectionManager);
                decimal peso = decimal.Parse(WeightTxtBox.Text.Replace(",", "").Replace(".", "")) / 100;
                animalManager.AddAnimal(OwnerNameTxtBox.Text, OwnerContactTxtBox.Text, AnimalBirth.Value,
                    DateTime.Now, AnimalTypeTxt.Text, BreedTxtBox.Text, SexM.Checked ? "M" : "F", peso);

                // Clear all input fields
                OwnerNameTxtBox.Clear();
                OwnerContactTxtBox.Clear();
                SexM.Checked = true;
                AnimalBirth.Value = DateTime.Now;
                AnimalTypeTxt.SelectedIndex=0;
                BreedTxtBox.Clear();
                WeightTxtBox.Clear();

                MessageBox.Show("Novo registo adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao adicionar o novo registo: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}