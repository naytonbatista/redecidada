using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeDemanda = Entidades.Demanda;

namespace Repository
{
    public class Demanda
    {
        private MySqlConnection connection = null;

        public Demanda()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
        }

        public void Cadastrar(EntidadeDemanda demanda)
        {
            MySqlCommand command = new MySqlCommand(@"INSERT INTO demanda (nome) VALUES 
            (@nome);", connection);

            command.Parameters.Add("nome", MySqlDbType.String);
            command.Parameters["nome"].Value = demanda.Nome;
                        
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

        public List<EntidadeDemanda> Listar()
        {
            List<EntidadeDemanda> demandas = new List<EntidadeDemanda>();

            MySqlCommand command = new MySqlCommand("select id, nome from demanda order by nome", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    demandas.Add(new EntidadeDemanda
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString()
                    });
                }

                return demandas;
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
