﻿using System;
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
        private bool _updateMode3;
        MySQLConnectionManager _connectionManager = new MySQLConnectionManager("vetclinic");
        
        private AnimalManager _animalManager;
        private MedicActManager _medicActManager;
        private ConsultManager _consultManager;
        
        public Form1()
        {
            InitializeComponent();
            IdTxtBox_TextChanged(null,null);
            MedicActIDTxtBox_TextChanged(null,null);
            ClearAllInputs(-1);
            SexM.Checked = true;
            AnimalTypeTxt.SelectedIndex=0;
            SearchFilter.SelectedIndex = 4;
            SearchFilterTxt2.SelectedIndex = 3;
            
            _consultManager = new ConsultManager(_connectionManager);
            textBox4.Text=_consultManager.GetNextFichaMedicaID().ToString();
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
                dataGridView2.DataSource = new DataTable();
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
                        dataGridView2.DataSource = new DataTable();
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
                            try
                            {
                                // Update the animal record
                                _connectionManager = new MySQLConnectionManager("vetclinic");
                                _animalManager = new AnimalManager(_connectionManager);
                                _animalManager.UpdateAnimal(Convert.ToInt32(IdTxtBox.Text), AnimalNameTxtBox.Text,OwnerNameTxtBox.Text, OwnerContactTxtBox.Text, AnimalBirth.Value,
                                    DateTime.Now, AnimalTypeTxt.Text, BreedTxtBox.Text, SexM.Checked ? "M" : "F", Convert.ToDecimal(WeightTxtBox.Text));
  
                                // Show a success message
                                MessageBox.Show(@"Registro atualizado com sucesso!", @"Sucesso", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                                ClearAllInputs(0);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(@"Não foi possivel atualizar o registo!", @"Erro", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                            }
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
        
        private bool CheckInputs3(bool diagnostico = true, bool observacoes = true, bool tipoConsulta = true)
        {
            if (diagnostico && string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show(@"Por favor, preencha o diagnostico.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox8.Focus();
                return false;
            }

            if (observacoes && string.IsNullOrEmpty(textBox9.Text))
            {
                MessageBox.Show(@"Por favor, preencha as observações.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox9.Focus();
                return false;
            }

            if (tipoConsulta && string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show(@"Por favor, preencha o tipo de consulta.", @"Campo Obrigatório", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
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
                textBox2.Text = selectedRow.Cells["id"].Value.ToString();

                AnimalNameTxtBox.Text = selectedRow.Cells["nome_animal"].Value.ToString();
                AnimalNameTxtBox2.Text = selectedRow.Cells["nome_animal"].Value.ToString();
                textBox3.Text = selectedRow.Cells["nome_animal"].Value.ToString();
                
                OwnerNameTxtBox.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                OwnerNameTxtBox2.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                textBox1.Text = selectedRow.Cells["nome_dono"].Value.ToString();
                
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
                    AnimalNameTxtBox2.Clear();
                    OwnerNameTxtBox2.Clear();
                    MedicActIDTxtBox.Clear();
                    MedicActTxtBox.Clear();
                    MedicActDateTxtBox.Clear();
                    LastUpdateTxtBox.Clear();
                    DescMedicActTxtBox.Clear();
                    SearchTxtBox2.Clear();
                    PriceTxtBox.Clear();

                    dataGridView2.DataSource = new DataTable();

                    SearchFilterTxt2.SelectedIndex = 3;
                    break;
                case 2:
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    dateTimePicker1.Value=DateTime.Now;

                    comboBox1.SelectedIndex = 0;

                    dataGridView3.DataSource = new DataTable();
                    dataGridView4.DataSource = new DataTable();
                    break;
                default:
                    ClearAllInputs(0);
                    ClearAllInputs(1);
                    ClearAllInputs(2);
                    break;
                
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllInputs(-1);
        }

        private void ClearBtn2_Click(object sender, EventArgs e)
        {
            ClearAllInputs(1);
            tabControl1.SelectedIndex = 0;
            SearchTxtBox.Focus();
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
                    dataGridView2.DataSource = new DataTable();
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
                    if (!string.IsNullOrEmpty(MedicActIDTxtBox.Text))
                    {
                        DialogResult result = MessageBox.Show(@"Tem certeza que deseja atualizar este registro?", @"Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                        if (result == DialogResult.Yes)
                        {
                            if(CheckInputs2())
                            {
                                try
                                {
                                    // Update the act record
                                    _connectionManager = new MySQLConnectionManager("vetclinic");
                                    _medicActManager = new MedicActManager(_connectionManager);
                                    _medicActManager.UpdateMedicAct(int.Parse(MedicActIDTxtBox.Text),int.Parse(AnimalIDTxtBox.Text),MedicActTxtBox.Text,DescMedicActTxtBox.Text,decimal.Parse(PriceTxtBox.Text));
                                    
                                    // Show a success message
                                    MessageBox.Show(@"Registro atualizado com sucesso!", @"Sucesso", MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                    ClearAllInputs(1);
                                }
                                catch (Exception)
                                {
                                    MessageBox.Show(@"Não foi possivel atualizar o registo!", @"Erro", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    else
                    {
                        // If no animal is selected, display an error message
                        MessageBox.Show(@"Por favor, selecione um ato médico para atualizar.", @"Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                    
                        _medicActManager.AddMedicAct(int.Parse(AnimalIDTxtBox.Text),MedicActTxtBox.Text,int.Parse(textBox4.Text),DescMedicActTxtBox.Text,decimal.Parse(PriceTxtBox.Text));

                        ClearAllInputs(1);

                        MessageBox.Show(@"Novo registo adicionado com sucesso!", @"Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tabControl1.SelectedIndex = 1;
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
            DialogResult choice = MessageBox.Show(@"Tem a certesa que quer apagar definitivamente este registo! (esta ação nao pode ser revertida)",
                @"Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (choice == DialogResult.Yes)
            {
                //Delete
                if (!string.IsNullOrEmpty(MedicActIDTxtBox.Text))
                {
                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _medicActManager = new MedicActManager(_connectionManager);
                    
                    _medicActManager.DeleteMedicAct(int.Parse(MedicActIDTxtBox.Text));
                    MessageBox.Show(@"Registo apagado com sucesso!",
                        @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }else
                {
                    MessageBox.Show(@"Selecione um registo primeiro!",
                        @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SearchTxtBox2.Focus();
                }
            }
            else
            {
                //Cancel
                MessageBox.Show(@"Ação cancelada!",
                    @"Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

                _connectionManager = new MySQLConnectionManager("vetclinic");
                _animalManager = new AnimalManager(_connectionManager);
                AnimalNameTxtBox2.Text = _animalManager.GetAnimalName(int.Parse(AnimalIDTxtBox.Text));
                
                _connectionManager = new MySQLConnectionManager("vetclinic");
                _animalManager = new AnimalManager(_connectionManager);
                OwnerNameTxtBox2.Text = _animalManager.GetOwnerName(int.Parse(AnimalIDTxtBox.Text));
                
                MedicActTxtBox.Text = selectedRow.Cells["ato_medico"].Value.ToString();
                DescMedicActTxtBox.Text = selectedRow.Cells["descricao_ato_medico"].Value.ToString();
                
                string value = selectedRow.Cells["custo_unitario"].Value.ToString();
                decimal valueDecimal = decimal.Parse(value);
                string formattedValue = valueDecimal.ToString("00000000.00");
                
                PriceTxtBox.Text = formattedValue;
                MedicActDateTxtBox.Text = selectedRow.Cells["data_insercao"].Value.ToString();
                LastUpdateTxtBox.Text = selectedRow.Cells["data_ultima_alteracao"].Value.ToString();
            }
        }

        private void MedicActIDTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MedicActIDTxtBox.Text))
            {
                _updateMode2 = false;
                NewUpdateActBtn.Text = @"Inserir novo registo";
                NewUpdateActBtn.Location = new Point(553, 111);
                NewUpdateActBtn.Size = new Size(387, 100);
                
                DelActBtn.Visible = false;
            }
            else
            {
                _updateMode2 = true;
                NewUpdateActBtn.Text = @"Atualizar registo";
                NewUpdateActBtn.Location = new Point(753, 111);
                NewUpdateActBtn.Size = new Size(187, 100);
                
                DelActBtn.Visible = true;
            }
        }

        private void SearchFilterTxt2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchTxtBox2.Clear();
        }
        
        private void AdicionarBtn_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _consultManager = new ConsultManager(_connectionManager);

            _consultManager.AddFichaMedica(int.Parse(textBox4.Text), int.Parse(textBox2.Text), DateTime.Today,
                comboBox1.Text, 1, textBox8.Text, textBox9.Text, textBox7.Text, dateTimePicker1.Value);
            textBox4_TextChanged(null,null);
        }
        private void RemoverAtoBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nenhuma linha selecionada.");
                return;
            }
            
            DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir os registros selecionados?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                int totalRegistrosExcluidos = 0;

                foreach (DataGridViewRow selectedRow in dataGridView3.SelectedRows)
                {
                    int idAtoMedico = Convert.ToInt32(selectedRow.Cells["id_ato_medico"].Value);

                    try
                    {
                        totalRegistrosExcluidos++;
                        _connectionManager = new MySQLConnectionManager("vetclinic");
                        _medicActManager = new MedicActManager(_connectionManager);

                        _medicActManager.DeleteMedicAct(idAtoMedico);
                        
                    }
                    catch (Exception exception)
                    {
                        totalRegistrosExcluidos--;
                        MessageBox.Show("Erro ao remover o ato médico: " + exception.Message);
                    }
                }
                MessageBox.Show($@"Foram excluídos {totalRegistrosExcluidos} registros de atos médicos.", @"Exclusão Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void RemoveConsBtn_Click(object sender, EventArgs e)
        {
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _consultManager = new ConsultManager(_connectionManager);
                
            _consultManager.RemoveConsultation(int.Parse(textBox4.Text));
            
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _consultManager = new ConsultManager(_connectionManager);
            textBox4.Text=_consultManager.GetNextFichaMedicaID().ToString();
            tabControl1.SelectedIndex = 0;
            ClearAllInputs(-1);
        }
        private void AddUpdateConsBtn_Click(object sender, EventArgs e)
        {
            if (!_updateMode3)
            {
                if (CheckInputs3())
                {
                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _consultManager = new ConsultManager(_connectionManager);

                    _consultManager.AddFichaMedica(int.Parse(textBox4.Text), int.Parse(textBox2.Text), DateTime.Today,
                        comboBox1.Text, 1, textBox8.Text, textBox9.Text, textBox7.Text, dateTimePicker1.Value);

                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _consultManager = new ConsultManager(_connectionManager);
                    textBox4.Text = _consultManager.GetNextFichaMedicaID().ToString();
                    tabControl1.SelectedIndex = 0;
                    ClearAllInputs(-1);
                }
            }
            else
            {
                if(CheckInputs3())
                {
                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _consultManager = new ConsultManager(_connectionManager);

                    _consultManager.UpdateFichaMedica(int.Parse(textBox4.Text), DateTime.Today, comboBox1.Text, 1,
                        textBox8.Text, textBox9.Text, textBox7.Text, dateTimePicker1.Value);
                    tabControl1.SelectedIndex = 0;
                    ClearAllInputs(-1);
                }
            }
        }

        private void TabChange(object sener, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                _connectionManager = new MySQLConnectionManager("vetclinic");
                _consultManager = new ConsultManager(_connectionManager);
                
                if(!string.IsNullOrEmpty(textBox4.Text))
                {
                    dataGridView3.DataSource = _consultManager.GetAtoMedicoByFichaMedicaID(int.Parse(textBox4.Text));
                }
                
                if(!string.IsNullOrEmpty(textBox2.Text))
                {
                    _connectionManager = new MySQLConnectionManager("vetclinic");
                    _consultManager = new ConsultManager(_connectionManager);
                    dataGridView4.DataSource = _consultManager.GetConsultasByAnimalID(int.Parse(textBox2.Text));
                }
                else
                {
                    MessageBox.Show(@"Por favor selecione um animal antes de realizar quaisquer operações", @"Atenção");
                    tabControl1.SelectedIndex = 0;
                }
            }

            if (tabControl1.SelectedIndex == 2)
            {
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show(@"Por favor selecione um animal antes de realizar quaisquer operações", @"Atenção");
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    AnimalIDTxtBox.Text = textBox2.Text;
                    AnimalNameTxtBox2.Text = textBox3.Text;
                    OwnerNameTxtBox2.Text = textBox1.Text;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            SearchTxtBox.Focus();
            ClearAllInputs(2);
            ClearAllInputs(1);
            _connectionManager = new MySQLConnectionManager("vetclinic");
            _consultManager = new ConsultManager(_connectionManager);
            textBox4.Text=_consultManager.GetNextFichaMedicaID().ToString();
        }
        
        private void DataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the double-clicked cell is not a header cell and that a row is selected
            if (e.RowIndex >= 0 && dataGridView4.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView4.SelectedRows[0];

                // Retrieve data from the selected row
                textBox4.Text = selectedRow.Cells["id_ficha_medica"].Value.ToString();
                textBox2.Text = selectedRow.Cells["id_animal"].Value.ToString();

                _connectionManager = new MySQLConnectionManager("vetclinic");
                _animalManager = new AnimalManager(_connectionManager);
                textBox3.Text = _animalManager.GetAnimalName(int.Parse(textBox2.Text));
                
                _connectionManager = new MySQLConnectionManager("vetclinic");
                _animalManager = new AnimalManager(_connectionManager);
                textBox1.Text = _animalManager.GetOwnerName(int.Parse(textBox2.Text));
                
                textBox6.Text = selectedRow.Cells["data_ato_medico"].Value.ToString();
                comboBox1.Text = selectedRow.Cells["tipo_consulta"].Value.ToString();
                
                textBox5.Text = selectedRow.Cells["codigo_colaborador"].Value.ToString();
                
                textBox8.Text = selectedRow.Cells["diagnostico"].Value.ToString();
                textBox7.Text = selectedRow.Cells["prescricao_medica"].Value.ToString();
                textBox9.Text = selectedRow.Cells["observacoes"].Value.ToString();
                dateTimePicker1.Value=DateTime.Parse(selectedRow.Cells["proxima_visita"].Value.ToString());
                
                _connectionManager = new MySQLConnectionManager("vetclinic");
                _consultManager = new ConsultManager(_connectionManager);
                dataGridView3.DataSource = _consultManager.GetAtoMedicoByFichaMedicaID(int.Parse(textBox4.Text));
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox4.Text))
            {
                _connectionManager = new MySQLConnectionManager("vetclinic");
                _consultManager = new ConsultManager(_connectionManager);
                if (_consultManager.CheckMedicalRecordExists(int.Parse(textBox4.Text)))
                {
                    AddUpdateConsBtn.Text = "Atualizar registo";
                    _updateMode3 = true;
                }
                else
                {
                    AddUpdateConsBtn.Text = "Inserir novo registo";
                    _updateMode3 = false;
                }
            }
        }
    }
}