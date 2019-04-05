using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorpusAPI.Models
{
    public class Usuario
    {
        public int _id { get; set; }
        public string _nome { get; set; }
        

        public Usuario(int id, string nome)
        {
            this._id = id;
            this._nome = nome;            
        }
    }
}