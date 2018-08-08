using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeMunicipio = Entidades.Municipio;

namespace Repository
{
    public class Municipio
    {
        private MySqlConnection connection = null;

        public Municipio()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");

            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }

        public List<EntidadeMunicipio> Listar(int estadoId)
        {
            List<EntidadeMunicipio> municipios = new List<EntidadeMunicipio>();

            MySqlCommand command = new MySqlCommand("select id, nome from municipio where estado = @estado order by nome", connection);

            command.Parameters.Add("estado", MySqlDbType.Int32);
            command.Parameters["estado"].Value = estadoId;

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    municipios.Add(new EntidadeMunicipio
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString()
                    });
                }

                return municipios;
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
