using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorpusAPI.Models
{
    public class Ticket
    {
        public int _id { get; set; }
        public string _codusuario { get; set; }
        public string _codendereco { get; set; }
        public string _inf { get; set; }
        public string _data { get; set; }
        public string _status { get; set; }

        public Ticket(
            int id, 
            string codusuario, 
            string codendereco, 
            string inf,
            string data,
            string status )
        {
            this._id = id;
            this._codusuario = codusuario;
            this._codendereco = codendereco;
            this._inf = inf;
            this._data = data;
            this._status = status;
        }
    }
}