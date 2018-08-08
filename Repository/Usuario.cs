using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using Entidade = Entidades.Usuario;

namespace Repository
{
    public class Usuario
    {
        private MySqlConnection connection = null;

        public Usuario()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");

            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }

        public void Cadastrar(Entidade usuario)
        {
            MySqlCommand command = new MySqlCommand(@"INSERT INTO usuario (nome, login, senha, data_cadastro) 
            VALUES (@nome, @login, @senha, current_timestamp()); ", connection);

            command.Parameters.Add("nome", MySqlDbType.String);
            command.Parameters["nome"].Value = usuario.Nome;

            command.Parameters.Add("login", MySqlDbType.String);
            command.Parameters["login"].Value = usuario.Login;
            
            command.Parameters.Add("senha", MySqlDbType.String);
            command.Parameters["senha"].Value = usuario.Senha;

            
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
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
                                
            }
           
        }

        public Entidade Logar(string login, string senha)
        {
            Entidade usuario = null;

            MySqlCommand command = new MySqlCommand("SELECT id, nome, login, senha, data_cadastro FROM usuario where login = @login and senha = @senha", connection);

            command.Parameters.Add("login", MySqlDbType.String);
            command.Parameters["login"].Value = login;

            command.Parameters.Add("senha", MySqlDbType.String);
            command.Parameters["senha"].Value = senha;

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuario = new Entidade
                    {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString(),
                        Login = reader["login"].ToString(),
                        Senha = reader["senha"].ToString(),
                        DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString())
                    };
                }

                return usuario;
            }
            catch (Exception exc)
            {
                command.Transaction.Rollback();
                throw exc;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }

            }

            throw new NotImplementedException();
        }

        public List<Entidade> Listar()
        {
            List<Entidade> usuarios = new List<Entidade>();

            MySqlCommand command = new MySqlCommand("SELECT id, nome, login, senha, data_cadastro FROM usuario;", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Entidade {
                        Id = Convert.ToInt32(reader["id"].ToString()),
                        Nome = reader["nome"].ToString(),
                        Login = reader["login"].ToString(),
                        Senha = reader["senha"].ToString(),
                        DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString())

                    });
                }

                return usuarios;
            }
            catch (Exception exc)
            {
                command.Transaction.Rollback();
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
