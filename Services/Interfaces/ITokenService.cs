using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(Usuario usuario);
    }
}
