using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using server_dotnet.Models;

namespace server_dotnet.DTOs
{
    public class FuncionariosDTO
    {

        [Required(ErrorMessage = "Insira o Nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira o cargo!")]
        public string Cargo { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }

        [Required]
        public bool Status { get; set; } = false;

        public List<SalarioFuncionarioDTO> Salarios { get; set; }
    }
}