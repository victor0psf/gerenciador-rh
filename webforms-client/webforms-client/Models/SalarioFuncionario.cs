using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webforms_client.Models
{
    public class SalarioFuncionario
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }  
        public decimal SalarioMedio { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }

}