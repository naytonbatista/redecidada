using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;
using EntidadeMatricula = Entidades.Matricula;

namespace Repository
{
    public class Matricula
    {
        private MySqlConnection connection = null;

        public Matricula()
        {
            connection = new MySqlConnection("Server=bd_rede_cidada.mysql.dbaas.com.br;Database=bd_rede_cidada;Uid=bd_rede_cidada;Pwd=bancorede;");
            //connection = new MySqlConnection("Server=localhost;Database=bd_rede_cidada;Uid=root;Pwd=1234;");
        }

        public void Cadastrar(EntidadeMatricula matricula)
        {
            MySqlCommand command = new MySqlCommand(@"INSERT INTO matricula (nome_aluno, data_nascimento, naturalidade, telefone, nome_pai, nome_mae, demanda, endereco,
                                                    escola, professor_responsavel, nucleo, data_cadastro, municipio, observacoes, turno, serie) VALUES (@nome_aluno, @data_nascimento, @naturalidade, @telefone,
                                                    @nome_pai, @nome_mae, @demanda, @endereco, @escola, @professor_responsavel, @nucleo, current_timestamp(), @municipio, @observacoes, @turno, @serie); 
                                                    select last_insert_id();", connection);
                
            command.Parameters.Add("nome_aluno", MySqlDbType.String);
            command.Parameters["nome_aluno"].Value = matricula.Nome;

            command.Parameters.Add("data_nascimento", MySqlDbType.Date);
            command.Parameters["data_nascimento"].Value = matricula.DataNascimento;

            command.Parameters.Add("naturalidade", MySqlDbType.String);
            command.Parameters["naturalidade"].Value = matricula.Naturalidade;

            command.Parameters.Add("telefone", MySqlDbType.String);
            command.Parameters["telefone"].Value = matricula.Telefone;

            command.Parameters.Add("nome_pai", MySqlDbType.String);
            command.Parameters["nome_pai"].Value = matricula.NomePai;

            command.Parameters.Add("nome_mae", MySqlDbType.String);
            command.Parameters["nome_mae"].Value = matricula.NomeMae;

            command.Parameters.Add("demanda", MySqlDbType.Int32);
            command.Parameters["demanda"].Value = matricula.Demanda;

            command.Parameters.Add("endereco", MySqlDbType.String);
            command.Parameters["endereco"].Value = matricula.Endereco;

            command.Parameters.Add("escola", MySqlDbType.String);
            command.Parameters["escola"].Value = matricula.Escola;

            command.Parameters.Add("professor_responsavel", MySqlDbType.Int32);
            command.Parameters["professor_responsavel"].Value = matricula.Professor;

            command.Parameters.Add("nucleo", MySqlDbType.String);
            command.Parameters["nucleo"].Value = matricula.Nucleo;

            command.Parameters.Add("municipio", MySqlDbType.Int32);
            command.Parameters["municipio"].Value = matricula.Municipio;

            command.Parameters.Add("turno", MySqlDbType.Int32);
            command.Parameters["turno"].Value = matricula.Turno;

            command.Parameters.Add("serie", MySqlDbType.String);
            command.Parameters["serie"].Value = matricula.Serie;

            command.Parameters.Add("observacoes", MySqlDbType.String);
            command.Parameters["observacoes"].Value = matricula.Observacoes;


            try
            {
                connection.Open();

                command.Transaction = connection.BeginTransaction();

                var obj = command.ExecuteScalar();

                if (obj == null)
                {
                    throw new Exception("Erro ao inserir a matrícula");
                }

                var matriculaId = Convert.ToInt32(obj);
                command.CommandText = "insert into matricula_atividade (matricula, atividade, data_cadastro) values (@matricula, @atividade, current_timestamp())";

                command.Parameters.Add("matricula", MySqlDbType.Int32);
                command.Parameters["matricula"].Value = matriculaId;

                command.Parameters.Add("atividade", MySqlDbType.Int32);


                foreach (int atividade in matricula.Atividades)
                {
                    command.Parameters["atividade"].Value = atividade;
                    command.ExecuteNonQuery();
                }

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

        public List<EntidadeMatricula> Listar()
        {
            List<EntidadeMatricula> matriculas = new List<EntidadeMatricula>();

            MySqlCommand command = new MySqlCommand(@"SELECT id, nome_aluno, data_nascimento, naturalidade, telefone, nome_pai, nome_mae, demanda, endereco,
            escola, professor_responsavel, nucleo, data_cadastro,municipio, observacoes, turno, serie FROM matricula; ", connection);
            
            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                EntidadeMatricula entidade;
                while (reader.Read())
                {
                    entidade = new EntidadeMatricula();

                    entidade.Id = Convert.ToInt32(reader["id"].ToString());
                    entidade.Nome = reader["nome_aluno"].ToString();
                    entidade.DataNascimento = DateTime.Parse(reader["data_nascimento"].ToString());
                    entidade.Naturalidade = reader["naturalidade"].ToString();
                    entidade.Telefone = reader["nome_aluno"].ToString();
                    entidade.NomePai = reader["nome_pai"].ToString();
                    entidade.NomeMae = reader["nome_mae"].ToString();
                    entidade.Demanda = Convert.ToInt32(reader["demanda"].ToString());
                    entidade.Endereco = reader["endereco"].ToString();
                    entidade.Escola = reader["endereco"].ToString();
                    entidade.Professor = Convert.ToInt32(reader["professor_responsavel"].ToString());
                    entidade.Nucleo = reader["nucleo"].ToString();
                    entidade.Municipio = reader["municipio"] != null && reader["municipio"] != DBNull.Value ? Convert.ToInt32(reader["municipio"].ToString()) : 0;
                    entidade.Serie = reader["serie"].ToString();
                    entidade.Turno = reader["turno"] != null && reader["turno"] != DBNull.Value ? Convert.ToInt32(reader["turno"].ToString()) : 0;
                    entidade.Observacoes = reader["observacoes"].ToString();
                    entidade.DataCadastro = DateTime.Parse(reader["data_cadastro"].ToString());


                    matriculas.Add(entidade);
                }

                return matriculas;
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
