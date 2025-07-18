using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webforms_client.Models
{
    public class FeriasDetalhes
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int FuncionarioId { get; set; }
        public string FuncionarioNome { get; set; }
        public string StatusFerias { get; set; }
    }
}