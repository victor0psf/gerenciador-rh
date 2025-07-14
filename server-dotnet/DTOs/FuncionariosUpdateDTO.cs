#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_dotnet.DTOs
{
    public class FuncionariosUpdateDTO
    {
        public string? Nome { get; set; }
        public string? Cargo { get; set; }
        public DateTime? DataAdmissao { get; set; }
        public bool? Status { get; set; }
        public List<SalarioFuncionarioDTO>? Salarios { get; set; }
    }
}