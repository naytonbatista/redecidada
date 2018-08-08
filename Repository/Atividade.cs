using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeAtividade = Entidades.Atividade;

namespace Repository
{
    public class Atividade
    {
        private MySqlConnection connection = null;

        public Atividade()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
        }

        public void Cadastrar(EntidadeAtividade atividade)
        {
            MySqlCommand command = new MySqlCommand("insert into atividade (nome, data_cadastro) values (@nome, current_timestamp())", connection);

            command.Parameters.Add("nome", MySqlDbType.String);
            command.Parameters["nome"].Value = atividade.Nome;

            try
            {
                connection.Open();

                command.Transaction = connection.BeginTransaction();

                command.ExecuteNonQuery();

                command.Transaction.Commit();
            }
            catch (Exception exc)
            {
                command.Transaction.Rollback();
                throw exc;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
                                
            }
           
        }

        public List<EntidadeAtividade> ListarAtividades()
        {
            List<EntidadeAtividade> atividades = new List<Entidades.Atividade>();

            MySqlCommand command = new MySqlCommand("select id, nome, data_cadastro from atividade", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    atividades.Add(new EntidadeAtividade {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString(),
                        DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString())

                    });
                }

                return atividades;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }
        }

        public List<int> ListarAtividades(int matricula)
        {
            List<EntidadeAtividade> atividades = new List<Entidades.Atividade>();

            MySqlCommand command = new MySqlCommand("select a.* from matricula_atividade ma, atividade a where ma.atividade = a.id and ma.matricula = @id", connection);

            command.Parameters.Add("id", MySqlDbType.Int32);
            command.Parameters["id"].Value = matricula;

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    atividades.Add(new EntidadeAtividade
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString(),
                        DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString())

                    });
                }

                return atividades.Select(x => x.Id).ToList();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }
        }
    }
}
