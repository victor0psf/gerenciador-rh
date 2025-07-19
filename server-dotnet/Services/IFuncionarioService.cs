using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server_dotnet.DTOs;

namespace server_dotnet.Services
{
    public interface IFuncionarioService
    {
        Task<bool> EditarFuncionarioAsync(int id, FuncionariosUpdateDTO updateFuncionario);
    }
}