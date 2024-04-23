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
            SearchFilter.SelectedIndex = 4;
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
                    //Delete
                    connectionManager = new MySQLConnectionManager("vetclinic");
                    animalManager = new AnimalManager(connectionManager);
                    
                    MessageBox.Show("Registo apagado com sucesso!",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Cancel
                    MessageBox.Show("Ação cancelada!",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                //Deactivate
                if (string.IsNullOrEmpty(IdTxtBox.Text))
                {
                    MessageBox.Show("Por favor, selecione o animal que deseja desativar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Prompt the user for confirmation
                    DialogResult confirmResult = MessageBox.Show("Tem a certeza que deseja desativar este animal?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Deactivate the animal
                        connectionManager = new MySQLConnectionManager("vetclinic");
                        animalManager = new AnimalManager(connectionManager);
                        animalManager.DeactivateAnimal(int.Parse(IdTxtBox.Text));
                        MessageBox.Show("O animal foi desativado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
                        ClearAllInputs();
                    }
                }
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
                if (string.IsNullOrEmpty(SearchTxtBox.Text) && SearchFilter.SelectedIndex!=4)
                {
                    MessageBox.Show("Por favor, insira um termo de pesquisa.", "Consulta Vazia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SearchTxtBox.Focus();
                }
                else
                {
                    if (!int.TryParse(SearchTxtBox.Text, out _) && SearchFilter.SelectedIndex==0)
                    {
                        MessageBox.Show("Por favor, insira um número válido.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SearchTxtBox.Clear();
                        SearchTxtBox.Focus();
                    }
                    else
                    {
                        dataGridView1.DataSource=animalManager.SearchAnimals(SearchFilter.SelectedIndex, SearchTxtBox.Text);
                    }
                }
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
                AddNewUpdateBtn.Size = new Size(107, 46); //Default
                AddNewUpdateBtn.Location = new Point(625, 148); // Default
                
                UpdateMode = true;
                DelDeactiveBtn.Visible = true;
                DelConfirm.Visible = true;
            }
            else
            {
                //Add New
                AddNewUpdateBtn.Text = "Adicionar novo";
                AddNewUpdateBtn.Size = new Size(272, 46); // Altered
                AddNewUpdateBtn.Location = new Point(460, 148); // Altered
                
                UpdateMode = false;
                DelDeactiveBtn.Visible = false;
                DelConfirm.Visible = false;
            }
        }

        private void AddNewUpdateBtn_Click(object sender, EventArgs e)
        {
            if (UpdateMode)
            {
                if (!string.IsNullOrEmpty(IdTxtBox.Text))
                {
                    int selectedAnimalId = Convert.ToInt32(IdTxtBox.Text);
                    
                    DialogResult result = MessageBox.Show("Tem certeza que deseja atualizar este registro?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        if(CheckInputs())
                        {
                            // Retrieve the updated information from the input fields
                            string nomeDono = OwnerNameTxtBox.Text;
                            string contatoDono = OwnerContactTxtBox.Text;
                            DateTime dataNascimento = AnimalBirth.Value;
                            DateTime dataUltimaConsulta = DateTime.Now;
                            string tipoAnimal = AnimalTypeTxt.Text;
                            string raca = BreedTxtBox.Text;
                            string sexo = SexM.Checked ? "M" : "F";
                            decimal peso = decimal.Parse(WeightTxtBox.Text);

                            // Update the animal record
                            connectionManager = new MySQLConnectionManager("vetclinic");
                            animalManager = new AnimalManager(connectionManager);
                            animalManager.UpdateAnimal(selectedAnimalId, nomeDono, contatoDono, dataNascimento,
                                dataUltimaConsulta, tipoAnimal, raca, sexo, peso);

                            // Show a success message
                            MessageBox.Show("Registro atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            ClearAllInputs();
                        }
                    }
                }
                else
                {
                    // If no animal is selected, display an error message
                    MessageBox.Show("Por favor, selecione um animal para atualizar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Add new
                if (CheckInputs())
                {
                    try
                    {
                        connectionManager = new MySQLConnectionManager("vetclinic");
                        animalManager = new AnimalManager(connectionManager);
                        decimal peso = decimal.Parse(WeightTxtBox.Text.Replace(",", "").Replace(".", "")) / 100;
                        animalManager.AddAnimal(OwnerNameTxtBox.Text, OwnerContactTxtBox.Text, AnimalBirth.Value,
                            DateTime.Now, AnimalTypeTxt.Text, BreedTxtBox.Text, SexM.Checked ? "M" : "F", peso);

                        ClearAllInputs();

                        MessageBox.Show("Novo registo adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocorreu um erro ao adicionar o novo registo: " + ex.Message, "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        
        private bool CheckInputs(bool checkOwnerName = true, bool checkOwnerContact = true, bool checkAnimalType = true, bool checkBreed = true, bool checkWeight = true)
        {
            if (checkOwnerName && string.IsNullOrEmpty(OwnerNameTxtBox.Text))
            {
                MessageBox.Show("Por favor, preencha o nome do dono.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OwnerNameTxtBox.Focus(); // Focus the OwnerNameTxtBox
                return false;
            }

            if (checkOwnerContact && string.IsNullOrEmpty(OwnerContactTxtBox.Text))
            {
                MessageBox.Show("Por favor, preencha o contato do dono.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OwnerContactTxtBox.Focus(); // Focus the OwnerContactTxtBox
                return false;
            }

            if (checkAnimalType && AnimalTypeTxt.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecione o tipo de animal.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AnimalTypeTxt.Focus(); // Focus the AnimalTypeTxt
                return false;
            }

            if (checkBreed && string.IsNullOrEmpty(BreedTxtBox.Text))
            {
                MessageBox.Show("Por favor, preencha a raça do animal.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BreedTxtBox.Focus(); // Focus the BreedTxtBox
                return false;
            }

            if (checkWeight && string.IsNullOrEmpty(WeightTxtBox.Text))
            {
                MessageBox.Show("Por favor, preencha o peso do animal.", "Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WeightTxtBox.Focus(); // Focus the WeightTxtBox
                return false;
            }

            return true;
        }
        
        private void OwnerContactTxtBox_Validating(object sender, CancelEventArgs e)
        {
            // Check if the contact number has exactly 9 digits
            if (OwnerContactTxtBox.Text.Length != 9)
            {
                MessageBox.Show("O número de contato deve ter 9 dígitos.", "Número Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the double-clicked cell is not a header cell and that a row is selected
            if (e.RowIndex >= 0 && dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Retrieve data from the selected row
                IdTxtBox.Text = selectedRow.Cells["id"].Value.ToString();
                OwnerNameTxtBox.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                OwnerContactTxtBox.Text = selectedRow.Cells["contato_dono"].Value.ToString();
                BreedTxtBox.Text = selectedRow.Cells["raca"].Value.ToString();
                
                string weightValue = selectedRow.Cells["peso"].Value.ToString();
                decimal pesoDecimal = decimal.Parse(weightValue);
                string formattedWeight = pesoDecimal.ToString("00000.00");
                WeightTxtBox.Text = formattedWeight;
                
                StateTxtBox.Text = selectedRow.Cells["estado"].Value.ToString();
                RegisterDateTxtBox.Text = selectedRow.Cells["data_registro"].Value.ToString();
                AnimalTypeTxt.Text = selectedRow.Cells["tipo_animal"].Value.ToString();
                LastConsultDateTxtBox.Text = selectedRow.Cells["data_ultima_consulta"].Value.ToString();
                
                switch (selectedRow.Cells["sexo"].Value.ToString())
                {
                    case "M":
                        SexM.Checked = true;
                        break;
                    case "F":
                        SexF.Checked = true;
                        break;
                    default:
                        throw new Exception("Erro ao carregar o sexo do animal");
                }
                
            }
        }
        
        private void ClearAllInputs()
        {
            // Clear text boxes
            IdTxtBox.Text = "";
            OwnerNameTxtBox.Text = "";
            OwnerContactTxtBox.Text = "";
            BreedTxtBox.Text = "";
            WeightTxtBox.Text = "";
            StateTxtBox.Text = "";
            AnimalTypeTxt.SelectedIndex=0;
            LastConsultDateTxtBox.Text = "";

            dataGridView1.DataSource = new DataTable();

            // Reset radio buttons
            SexM.Checked = true;

            // Reset date pickers
            AnimalBirth.Value = DateTime.Now;
        }
    }
}