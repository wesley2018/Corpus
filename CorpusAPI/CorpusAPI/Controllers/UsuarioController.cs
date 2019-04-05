using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CorpusAPI.Models;

namespace CorpusAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        //Listar todos os usuário com determinado código
        public List<Usuario> Get(string id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "SELECT * FROM USUARIO " +
                "WHERE ID = @ID";
            query.Parameters.AddWithValue("@ID", id);
            var lista_usuario = new List<Usuario>();            

            try
            {
                conn.Open();
                MySqlDataReader fetch_query = query.ExecuteReader();
                while (fetch_query.Read())
                {
                    lista_usuario.Add(new Usuario(Convert.ToInt16(fetch_query["ID"]),
                                                  fetch_query["NOME"].ToString()));
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                lista_usuario.Add(new Usuario(0,"ERRO"));
            }

            return lista_usuario;
        }

        //Atualizar Usuário
        public string Post(string variavel, string codigo, string usuario)
        {
            switch (variavel)
            {
                case "put":
                    if (AtualizarUsuario(codigo, usuario) == "sucesso")
                    {
                        return "sucesso";
                    }
                    else
                    {
                        return "erro";
                    }
                default:
                    return "erro";
            }
        }

        public string AtualizarUsuario(string codigo, string usuario)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "UPDATE USUARIO " +
                "SET NOME = @USUARIO " +
                "WHERE ID = @ID;";
            query.Parameters.AddWithValue("@ID", codigo);
            query.Parameters.AddWithValue("@USUARIO", usuario);

            try
            {
                conn.Open();
                query.ExecuteReader();
                conn.Close();
                return "sucesso";
            }
            catch (MySqlException)
            {
                //throw;
                return "erro";
            }

        }

        

        

        /*
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */
    }
}