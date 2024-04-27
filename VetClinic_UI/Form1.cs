using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace VetClinic_UI
{
    public partial class Form1 : Form
    {
        private bool _eraseMode;
        private bool _updateMode;
        private bool _updateMode2;
        MySQLConnectionManager _connectionManager = new MySQLConnectionManager("vetclinic");
        
        private AnimalManager _animalManager;
        private MedicActManager _medicActManager;
        
        public Form1()
        {
            InitializeComponent();
            IdTxtBox_TextChanged(null,null);
            SexM.Checked = true;
            AnimalTypeTxt.SelectedIndex=0;
            SearchFilter.SelectedIndex = 4;
            SearchFilterTxt2.SelectedIndex = 3;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (DelConfirm.Checked)
            {
                MessageBox.Show(@"Atencão esta opção permite remover completamente um registo da base de dados!",
                    @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DelDeactiveBtn.Text = @"Remover registo";
                _eraseMode = true;
            }
            else
            {
                DelDeactiveBtn.Text = @"Desativar";
                _eraseMode = false;
            }
        }

        private void DelDeactiveBtn_Click(object sender, EventArgs e)
        {
            if (_eraseMode)
            {
                DialogResult choice = MessageBox.Show(@"Tem a certesa que quer apagar definitivamente este registo! (esta ação nao pode ser revertida)",
                    @"Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (choice == DialogResult.Yes)
                {
                    //Delete
                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _animalManager = new AnimalManager(_connectionManager);
                    
                    MessageBox.Show(@"Registo apagado com sucesso!",
                        @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //Cancel
                    MessageBox.Show(@"Ação cancelada!",
                        @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                
                //Deactivate
                if (string.IsNullOrEmpty(IdTxtBox.Text))
                {
                    MessageBox.Show(@"Por favor, selecione o animal que deseja desativar.", @"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Prompt the user for confirmation
                    DialogResult confirmResult = MessageBox.Show(@"Tem a certeza que deseja desativar este animal?", @"Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    
                    if (confirmResult == DialogResult.Yes)
                    {
                        // Deactivate the animal
                        _connectionManager = new MySQLConnectionManager("vetclinic");
                        _animalManager = new AnimalManager(_connectionManager);
                        _animalManager.DeactivateAnimal(int.Parse(IdTxtBox.Text));
                        MessageBox.Show(@"O animal foi desativado com sucesso.", @"Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
                        ClearAllInputs(0);
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
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _animalManager = new AnimalManager(_connectionManager);
            
            if (SearchFilter.SelectedIndex == 3)
            {
                dataGridView1.DataSource=_animalManager.SearchAnimals(3, SearchAnimalType.Text);
            }
            else
            {
                if (string.IsNullOrEmpty(SearchTxtBox.Text) && SearchFilter.SelectedIndex!=4)
                {
                    MessageBox.Show(@"Por favor, insira um termo de pesquisa.", @"Consulta Vazia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SearchTxtBox.Focus();
                }
                else
                {
                    if (!int.TryParse(SearchTxtBox.Text, out _) && SearchFilter.SelectedIndex==0)
                    {
                        MessageBox.Show(@"Por favor, insira um número válido.", @"Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        SearchTxtBox.Clear();
                        SearchTxtBox.Focus();
                    }
                    else
                    {
                        dataGridView1.DataSource=_animalManager.SearchAnimals(SearchFilter.SelectedIndex, SearchTxtBox.Text);
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
                AddNewUpdateBtn.Text = @"Atualizar registo";
                AddNewUpdateBtn.Size = new Size(107, 46); //Default
                AddNewUpdateBtn.Location = new Point(625, 148); // Default
                
                _updateMode = true;
                DelDeactiveBtn.Visible = true;
                DelConfirm.Visible = true;
            }
            else
            {
                //Add New
                AddNewUpdateBtn.Text = @"Adicionar novo";
                AddNewUpdateBtn.Size = new Size(272, 46); // Altered
                AddNewUpdateBtn.Location = new Point(460, 148); // Altered
                
                _updateMode = false;
                DelDeactiveBtn.Visible = false;
                DelConfirm.Visible = false;
            }
        }

        private void AddNewUpdateBtn_Click(object sender, EventArgs e)
        {
            if (_updateMode)
            {
                if (!string.IsNullOrEmpty(IdTxtBox.Text))
                {
                    DialogResult result = MessageBox.Show(@"Tem certeza que deseja atualizar este registro?", @"Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        if(CheckInputs())
                        {
                            // Update the animal record
                            _connectionManager = new MySQLConnectionManager("vetclinic");
                            _animalManager = new AnimalManager(_connectionManager);
                            _animalManager.UpdateAnimal(Convert.ToInt32(IdTxtBox.Text), AnimalNameTxtBox.Text,OwnerNameTxtBox.Text, OwnerContactTxtBox.Text, AnimalBirth.Value,
                                DateTime.Now, AnimalTypeTxt.Text, BreedTxtBox.Text, SexM.Checked ? "M" : "F", decimal.Parse(WeightTxtBox.Text));

                            // Show a success message
                            MessageBox.Show(@"Registro atualizado com sucesso!", @"Sucesso", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                            ClearAllInputs(0);
                        }
                    }
                }
                else
                {
                    // If no animal is selected, display an error message
                    MessageBox.Show(@"Por favor, selecione um animal para atualizar.", @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Add new
                if (CheckInputs())
                {
                    try
                    {
                        _connectionManager = new MySQLConnectionManager("vetclinic");
                        _animalManager = new AnimalManager(_connectionManager);
                        decimal peso = decimal.Parse(WeightTxtBox.Text.Replace(",", "").Replace(".", "")) / 100;
                        _animalManager.AddAnimal(AnimalNameTxtBox.Text, OwnerNameTxtBox.Text, OwnerContactTxtBox.Text,
                            AnimalBirth.Value,
                            DateTime.Now, AnimalTypeTxt.Text, BreedTxtBox.Text, SexM.Checked ? "M" : "F", peso);

                        ClearAllInputs(0);

                        MessageBox.Show(@"Novo registo adicionado com sucesso!", @"Sucesso", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Ocorreu um erro ao adicionar o novo registo: " + ex.Message, @"Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _connectionManager.CloseConnection();
                    }
                }
            }
        }
        
        private bool CheckInputs(bool checkOwnerName = true, bool checkOwnerContact = true, bool checkAnimalType = true, bool checkBreed = true, bool checkWeight = true)
        {
            if (checkOwnerName && string.IsNullOrEmpty(OwnerNameTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha o nome do dono.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OwnerNameTxtBox.Focus(); // Focus the OwnerNameTxtBox
                return false;
            }

            if (checkOwnerContact && string.IsNullOrEmpty(OwnerContactTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha o contato do dono.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                OwnerContactTxtBox.Focus(); // Focus the OwnerContactTxtBox
                return false;
            }

            if (checkAnimalType && AnimalTypeTxt.SelectedIndex == -1)
            {
                MessageBox.Show(@"Por favor, selecione o tipo de animal.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AnimalTypeTxt.Focus(); // Focus the AnimalTypeTxt
                return false;
            }

            if (checkBreed && string.IsNullOrEmpty(BreedTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha a raça do animal.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BreedTxtBox.Focus(); // Focus the BreedTxtBox
                return false;
            }

            if (checkWeight && string.IsNullOrEmpty(WeightTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha o peso do animal.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                WeightTxtBox.Focus(); // Focus the WeightTxtBox
                return false;
            }

            return true;
        }
        
        private bool CheckInputs2(bool checkAnimalId = true, bool checkMedicActId = true, bool checkMedicAct = true, bool checkDescMedicAct = true, bool checkPrice = true)
        {
            if (checkAnimalId && string.IsNullOrEmpty(AnimalIDTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, selecione o ID do animal.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 0;
                SearchTxtBox.Focus();
                return false;
            }

            if (checkMedicActId && string.IsNullOrEmpty(MedicActIDTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, selecione o ID da atividade médica.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SearchTxtBox2.Focus(); // Focus the MedicActIDTxtBox
                return false;
            }

            if (checkMedicAct && string.IsNullOrEmpty(MedicActTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha a atividade médica.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MedicActTxtBox.Focus(); // Focus the MedicActTxtBox
                return false;
            }

            if (checkDescMedicAct && string.IsNullOrEmpty(DescMedicActTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha a descrição da atividade médica.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DescMedicActTxtBox.Focus(); // Focus the DescMedicActTxtBox
                return false;
            }

            if (checkPrice && string.IsNullOrEmpty(PriceTxtBox.Text))
            {
                MessageBox.Show(@"Por favor, preencha o preço.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                PriceTxtBox.Focus(); // Focus the PriceTxtBox
                return false;
            }

            return true;
        }
        
        private void OwnerContactTxtBox_Validating(object sender, CancelEventArgs e)
        {
            // Check if the contact number has exactly 9 digits
            if (OwnerContactTxtBox.Text.Length != 9)
            {
                MessageBox.Show(@"O número de contato deve ter 9 dígitos.", @"Número Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                AnimalIDTxtBox.Text = selectedRow.Cells["id"].Value.ToString();

                AnimalNameTxtBox.Text = selectedRow.Cells["nome_animal"].Value.ToString();
                AnimalNameTxtBox2.Text = selectedRow.Cells["nome_animal"].Value.ToString();
                
                OwnerNameTxtBox.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                OwnerNameTxtBox2.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                
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
        
        private void ClearAllInputs(int page)
        {
            switch (page)
            {
                case 0:
                    // Clear text boxes
                    IdTxtBox.Clear();
                    OwnerNameTxtBox.Clear();
                    OwnerContactTxtBox.Clear();
                    BreedTxtBox.Clear();
                    WeightTxtBox.Clear();
                    StateTxtBox.Clear();
                    AnimalNameTxtBox.Clear();
                    AnimalTypeTxt.SelectedIndex = 0;
                    LastConsultDateTxtBox.Clear();

                    dataGridView1.DataSource = new DataTable();

                    // Reset radio buttons
                    SexM.Checked = true;

                    // Reset date pickers
                    AnimalBirth.Value = DateTime.Now;
                    break;
                case 1:
                    AnimalIDTxtBox.Clear();
                    AnimalNameTxtBox.Clear();
                    OwnerNameTxtBox2.Clear();
                    MedicActIDTxtBox.Clear();
                    MedicActTxtBox.Clear();
                    MedicActDateTxtBox.Clear();
                    LastUpdateTxtBox.Clear();
                    DescMedicActTxtBox.Clear();
                    SearchTxtBox2.Clear();
                    PriceTxtBox.Clear();

                    dataGridView2.DataSource = new DataTable();

                    SearchFilterTxt2.SelectedIndex = 0;
                    break;
                default:
                    ClearAllInputs(0);
                    ClearAllInputs(1);
                    break;
                
            }
        }

        private void SelectDummyBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            SearchTxtBox.Focus();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllInputs(0);
        }

        private void ClearBtn2_Click(object sender, EventArgs e)
        {
            ClearAllInputs(1);
        }

        private void SearchBtn2_Click(object sender, EventArgs e)
        {
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _medicActManager = new MedicActManager(_connectionManager);
                
            if (string.IsNullOrEmpty(SearchTxtBox2.Text) && SearchFilterTxt2.SelectedIndex!=3)
            {
                MessageBox.Show(@"Por favor, insira um termo de pesquisa.", @"Consulta Vazia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                SearchTxtBox.Focus();
            }
            else
            {
                if (!int.TryParse(SearchTxtBox2.Text, out _) && SearchFilterTxt2.SelectedIndex==0)
                {
                    MessageBox.Show(@"Por favor, insira um número válido.", @"Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    SearchTxtBox.Clear();
                    SearchTxtBox.Focus();
                }
                else
                {
                    dataGridView2.DataSource=_medicActManager.SearchMedicActs(SearchFilterTxt2.SelectedIndex,SearchTxtBox2.Text);
                }
            }
        }

        private void NewUpdateActBtn_Click(object sender, EventArgs e)
        {
            if (_updateMode2)
            {
                if (CheckInputs2())
                {
                    
                }
            }
            else
            {
                if (CheckInputs2(checkMedicActId:false))
                {
                    try
                    {
                        _connectionManager = new MySQLConnectionManager("vetclinic");
                        _medicActManager = new MedicActManager(_connectionManager);
                    
                        _medicActManager.AddMedicAct(int.Parse(AnimalIDTxtBox.Text),MedicActTxtBox.Text,DescMedicActTxtBox.Text,decimal.Parse(PriceTxtBox.Text));

                        ClearAllInputs(1);

                        MessageBox.Show(@"Novo registo adicionado com sucesso!", @"Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Ocorreu um erro ao adicionar o novo registo: " + ex.Message, @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        _connectionManager.CloseConnection();
                    }
                }
            }
        }

        private void DelActBtn_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
        
        private void DataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the double-clicked cell is not a header cell and that a row is selected
            if (e.RowIndex >= 0 && dataGridView2.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];

                // Retrieve data from the selected row
                MedicActIDTxtBox.Text = selectedRow.Cells["id_ato_medico"].Value.ToString();
                AnimalIDTxtBox.Text = selectedRow.Cells["id_animal"].Value.ToString();
                AnimalNameTxtBox2.Text = "";
                OwnerNameTxtBox2.Text = "";
                
                MedicActTxtBox.Text = selectedRow.Cells["ato_medico"].Value.ToString();
                DescMedicActTxtBox.Text = selectedRow.Cells["descricao_ato_medico"].Value.ToString();
                PriceTxtBox.Text = selectedRow.Cells["custo_unitario"].Value.ToString();
                MedicActDateTxtBox.Text = selectedRow.Cells["data_insercao"].Value.ToString();
                LastUpdateTxtBox.Text = selectedRow.Cells["data_ultima_alteracao"].Value.ToString();
            }
        }

        private void AnimalIDTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MedicActIDTxtBox.Text))
            {
                _updateMode2 = false;
            }
            else
            {
                _updateMode2 = true;
            }
        }
    }
}