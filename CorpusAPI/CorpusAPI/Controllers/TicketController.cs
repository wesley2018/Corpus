using CorpusAPI.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CorpusAPI.Controllers
{
    public class TicketController : ApiController
    {
        //Listar todos os tickets com determinado código de usuario
        public List<Ticket> Get(string codusuario)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "SELECT * FROM TICKET " +
                "WHERE CODUSUARIO = @CODUSUARIO";
            query.Parameters.AddWithValue("@CODUSUARIO", codusuario);
            var lista_ticket = new List<Ticket>();

            try
            {
                conn.Open();
                MySqlDataReader fetch_query = query.ExecuteReader();
                while (fetch_query.Read())
                {
                    lista_ticket.Add(new Ticket(
                        Convert.ToInt16(fetch_query["ID"]),
                        fetch_query["CODUSUARIO"].ToString(),
                        fetch_query["CODENDERECO"].ToString(),
                        fetch_query["INF"].ToString(),
                        fetch_query["DATA"].ToString(),
                        fetch_query["STATUS"].ToString()));
                }
                conn.Close();
            }
            catch (MySqlException ex)
            {
                
            }

            return lista_ticket;
        }


        //Adicionar Ticket
        public string Post(string variavel, 
            string codigoUsuario, 
            string codigoEndereco, 
            string inf,
            string data,
            string status)
        {
            switch (variavel)
            {
                case "post":
                    if (AdicionarTicket(
                        codigoUsuario, 
                        codigoEndereco, 
                        inf, 
                        data, 
                        status) == "sucesso")
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

        public string AdicionarTicket(
            string codigoUsuario, 
            string codigoEndereco, 
            string inf,
            string data,
            string status)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText =
                "INSERT INTO TICKET (CODUSUARIO, CODENDERECO, INF, DATA, STATUS) " +
                "VALUES (@CODUSUARIO, @CODENDERECO, @INF, @DATA, @STATUS);";

            query.Parameters.AddWithValue("@CODUSUARIO", codigoUsuario);
            query.Parameters.AddWithValue("@CODENDERECO", codigoEndereco);
            query.Parameters.AddWithValue("@INF", inf);
            query.Parameters.AddWithValue("@DATA", data);
            query.Parameters.AddWithValue("@STATUS", status);            

            try
            {
                conn.Open();
                query.ExecuteReader();
                conn.Close();
                return "sucesso";
            }
            catch (MySqlException)
            {
                return "erro";
            }

        }



    }
}
