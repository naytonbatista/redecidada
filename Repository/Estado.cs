using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeEstado = Entidades.Estado;

namespace Repository
{
    public class Estado
    {
        private MySqlConnection connection = null;

        public Estado()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }
                
        public List<EntidadeEstado> Listar()
        {
            List<EntidadeEstado> estados = new List<EntidadeEstado>();

            MySqlCommand command = new MySqlCommand("select id, nome  from estado order by nome", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    estados.Add(new EntidadeEstado
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString()

                    });
                }

                return estados;
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

        public Int32? BuscarPorMunicipio(int municipio)
        {
            
            MySqlCommand command = new MySqlCommand("select e.id from estado e, municipio m where m.estado = e.id and m.id = @municipio", connection);

            command.Parameters.Add("municipio", MySqlDbType.Int32);
            command.Parameters["municipio"].Value = municipio;
            try
            {
                connection.Open();

                var obj = command.ExecuteScalar();

                if (obj == null || obj == DBNull.Value)
                {
                    return null;

                }
                   
                return Convert.ToInt32(obj);
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
