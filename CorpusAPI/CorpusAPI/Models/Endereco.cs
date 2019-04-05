using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorpusAPI.Models
{
    public class Endereco
    {
        public int _id { get; set; }
        public int _iduser { get; set; }
        public string _latitude { get; set; }
        public string _longitude { get; set; }

        public Endereco (int id, int iduser, string latitude, string longitude)
        {
            this._id = id;
            this._iduser = iduser;
            this._latitude = latitude;
            this._longitude = longitude;
        }
    }
}