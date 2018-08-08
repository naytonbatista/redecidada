using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeProfessor = Entidades.Professor;

namespace Repository
{
    public class Professor
    {
        private MySqlConnection connection = null;

        public Professor()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }

        public void Cadastrar(EntidadeProfessor professor)
        {
            MySqlCommand command = new MySqlCommand(@"INSERT INTO professor (nome, polo, data_cadastro) VALUES 
            (@nome, @polo, current_timestamp());", connection);

            command.Parameters.Add("nome", MySqlDbType.String);
            command.Parameters["nome"].Value = professor.Nome;

            command.Parameters.Add("polo", MySqlDbType.String);
            command.Parameters["polo"].Value = professor.Polo;

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

        public List<EntidadeProfessor> Listar()
        {
            List<EntidadeProfessor> professores = new List<EntidadeProfessor>();

            MySqlCommand command = new MySqlCommand("select id, nome, polo, data_cadastro from professor order by nome", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    professores.Add(new EntidadeProfessor
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString(),
                        Polo = reader["polo"].ToString(),
                        DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString())

                    });
                }

                return professores;
            }
            catch (Exception exc)
            {
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
    }
}
