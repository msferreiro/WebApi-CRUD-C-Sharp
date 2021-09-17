using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string correo, string password);

        Task<bool> ExisteUsuario(string correo);
    }

}
