using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_dotnet.Models
{
    public class HistoricoAlteracao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataHoraAlteracao { get; set; }

        [Required]
        public string CampoAlterado { get; set; }

        [Required]
        public string ValorAntigo { get; set; }

        [Required]
        public string ValorNovo { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        public Funcionario Funcionario { get; set; }

    }
}