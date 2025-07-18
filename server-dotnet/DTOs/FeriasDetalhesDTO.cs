using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server_dotnet.Models;

namespace server_dotnet.DTOs
{
    public class FeriasDetalhesDTO
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int FuncionarioId { get; set; }
        public string FuncionarioNome { get; set; }
        public StatusFerias StatusFerias { get; set; }
    }
}