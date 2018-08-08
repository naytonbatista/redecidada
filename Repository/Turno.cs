using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeTurno = Entidades.Turno;

namespace Repository
{
    public class Turno
    {
        private MySqlConnection connection = null;

        public Turno()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }

        public List<EntidadeTurno> Listar()
        {
            List<EntidadeTurno> turnos = new List<EntidadeTurno>();

            MySqlCommand command = new MySqlCommand("select id, nome  from turno order by nome", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    turnos.Add(new EntidadeTurno
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString()

                    });
                }

                return turnos;
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
