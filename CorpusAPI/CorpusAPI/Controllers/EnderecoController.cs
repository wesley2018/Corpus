using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CorpusAPI.Models;

namespace CorpusAPI.Controllers
{
    public class EnderecoController : ApiController
    {
        //Listar todos os endereços com determinado código
        public List<Endereco> Get(string id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "SELECT * FROM ENDERECO " +
                "WHERE ID = @ID";
            query.Parameters.AddWithValue("@ID", id);
            var lista_endereco = new List<Endereco>();

            try
            {
                conn.Open();
                MySqlDataReader fetch_query = query.ExecuteReader();
                while (fetch_query.Read())
                {
                    lista_endereco.Add(new Endereco(Convert.ToInt16(fetch_query["ID"]),
                        Convert.ToInt16(fetch_query["IDUSER"]),
                        fetch_query["LATITUDE"].ToString(),
                        fetch_query["LONGITUDE"].ToString()
                        ));
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                
            }

            return lista_endereco;
        }

        //Atualizar Endereco
        public string Post(string variavel, string codigo, string latitude, string longitude)
        {
            switch (variavel)
            {
                case "put":
                    if (AtualizarEndereco(codigo, latitude, longitude) == "sucesso")
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

        public string AtualizarEndereco(string codigo, string latitude, string longitude)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "UPDATE ENDERECO " +
                "SET LATITUDE = @LATITUDE " +
                ", LONGITUDE = @LONGITUDE " +
                "WHERE ID = @ID;";
            query.Parameters.AddWithValue("@ID", codigo);
            query.Parameters.AddWithValue("@LATITUDE", latitude);
            query.Parameters.AddWithValue("@LONGITUDE", longitude);

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
    }
}
