using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace VetClinic_UI
{
    public class ConsultManager
    {
        private MySQLConnectionManager connectionManager;

        public ConsultManager(MySQLConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public void AddFichaMedica(int idFichamedica,int idAnimal, DateTime dataAtendimento, string tipoConsulta, int codigoColaborador, string diagnostico, string observacoes, string prescricaoMedica, DateTime proximaVisita)
        {
            string query = "INSERT INTO FichaMedica (id_ficha_medica,id_animal, data_ato_medico, tipo_consulta, codigo_colaborador, diagnostico, observacoes, prescricao_medica, proxima_visita, estado) " +
                           "VALUES (@idFichamedica,@idAnimal, @dataAtendimento, @tipoConsulta, @codigoColaborador, @diagnostico, @observacoes, @prescricaoMedica, @proximaVisita, 'ativo')";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idFichamedica", idFichamedica);
                    command.Parameters.AddWithValue("@idAnimal", idAnimal);
                    command.Parameters.AddWithValue("@dataAtendimento", dataAtendimento);
                    command.Parameters.AddWithValue("@tipoConsulta", tipoConsulta);
                    command.Parameters.AddWithValue("@codigoColaborador", codigoColaborador);
                    command.Parameters.AddWithValue("@diagnostico", diagnostico);
                    command.Parameters.AddWithValue("@observacoes", observacoes);
                    command.Parameters.AddWithValue("@prescricaoMedica", prescricaoMedica);
                    command.Parameters.AddWithValue("@proximaVisita", proximaVisita);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(@"Erro ao adicionar consulta: " + e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        public void RemoveConsultation(int consultationId)
        {
            string removeQuery = "DELETE FROM FichaMedica WHERE id_ficha_medica = @consultationId";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(removeQuery, connection))
                {
                    command.Parameters.AddWithValue("@consultationId", consultationId);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show(@"Consulta removida com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Erro ao remover consulta: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        
        public int GetNextFichaMedicaID()
        {
            int nextID = 1; // Valor padrão caso não haja registros

            string query = "SELECT MAX(id_ficha_medica) FROM FichaMedica";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            nextID = Convert.ToInt32(result) + 1;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return nextID;
        }
        
        public DataTable GetAtoMedicoByFichaMedicaID(int fichaMedicaID)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT id_ato_medico, ato_medico FROM AtosMedicos WHERE id_ficha_medica = @fichaMedicaID";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fichaMedicaID", fichaMedicaID);

                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dataTable;
        }
        
        public DataTable GetConsultasByAnimalID(int animalID)
        {
            DataTable dataTable = new DataTable();

            string query = "SELECT * FROM FichaMedica WHERE id_animal = @animalID";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@animalID", animalID);

                    try
                    {
                        connection.Open();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return dataTable;
        }
        
        public void UpdateFichaMedica(int id, DateTime dataAtoMedico, string tipoConsulta, int codigoColaborador, string diagnostico, string observacoes, string prescricaoMedica, DateTime proximaVisita)
        {
            string query = "UPDATE FichaMedica SET data_ato_medico = @dataAtoMedico, tipo_consulta = @tipoConsulta, codigo_colaborador = @codigoColaborador, " +
                           "diagnostico = @diagnostico, observacoes = @observacoes, prescricao_medica = @prescricaoMedica, proxima_visita = @proximaVisita " +
                           "WHERE id_ficha_medica = @id";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@dataAtoMedico", dataAtoMedico);
                    command.Parameters.AddWithValue("@tipoConsulta", tipoConsulta);
                    command.Parameters.AddWithValue("@codigoColaborador", codigoColaborador);
                    command.Parameters.AddWithValue("@diagnostico", diagnostico);
                    command.Parameters.AddWithValue("@observacoes", observacoes);
                    command.Parameters.AddWithValue("@prescricaoMedica", prescricaoMedica);
                    command.Parameters.AddWithValue("@proximaVisita", proximaVisita);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
        public bool CheckMedicalRecordExists(int medicalRecordId)
        {
            bool exists = false;

            string query = "SELECT COUNT(*) FROM FichaMedica WHERE id_ficha_medica = @MedicalRecordId";

            using (MySqlConnection connection = connectionManager.GetConnection())
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MedicalRecordId", medicalRecordId);

                    try
                    {
                        connection.Open();
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        exists = (count > 0);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro: " + e);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            return exists;
        }
    }
}
