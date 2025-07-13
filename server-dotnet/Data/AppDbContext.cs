using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server_dotnet.Models;

namespace server_dotnet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ferias> Ferias { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<SalarioFuncionario> SalarioFuncionarios { get; set; }
        public DbSet<HistoricoAlteracao> HistoricoAlteracoes { get; set; }

    }
}
