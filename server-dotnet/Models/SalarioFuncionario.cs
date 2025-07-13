using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace server_dotnet.Models
{
    public class SalarioFuncionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o salário.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salario { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalarioMedio { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [JsonIgnore]
        public Funcionario Funcionario { get; set; }

    }
}