using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webforms_client.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public bool Status { get; set; }
    }
}