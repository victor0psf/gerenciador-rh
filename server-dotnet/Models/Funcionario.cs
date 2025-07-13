using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace server_dotnet.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o Nome")]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Insira o cargo!")]
        [MaxLength(50)]
        public string Cargo { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }

        [Required]
        public bool Status { get; set; } = false;

        public ICollection<SalarioFuncionario> Salarios { get; set; } = new List<SalarioFuncionario>();

        public ICollection<HistoricoAlteracao> HistoricosAlteracao { get; set; } = new List<HistoricoAlteracao>();
    }
}