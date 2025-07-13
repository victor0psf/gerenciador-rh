using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_dotnet.Models
{
    public class Ferias
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataInicio { get; set; }

        [Required]
        public DateTime DataTermino { get; set; }

        [NotMapped]
        public StatusFerias StatusFerias
        {
            get
            {
                var hoje = DateTime.Now.Date;
                if (DataInicio > hoje)
                    return StatusFerias.Pendente;
                if (DataInicio <= hoje && DataTermino >= hoje)
                    return StatusFerias.Andamento;
                return StatusFerias.Concluidas;
            }
        }
        [Required]
        public int FuncionarioId { get; set; }

        public Funcionario Funcionario { get; set; }
    }
}