using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server_dotnet.DTOs
{
    public class SalarioFuncionarioDTO
    {
        [Required(ErrorMessage = "Informe o salário!")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salário deve ser maior que zero.")]
        public decimal Salario { get; set; }

        [Required(ErrorMessage = "Informe o salário médio!")]
        public decimal SalarioMedio { get; set; }
    }
}