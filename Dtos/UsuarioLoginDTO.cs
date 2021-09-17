using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Dtos
{
    public class UsuarioLoginDTO
    {
       
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
    }
}
