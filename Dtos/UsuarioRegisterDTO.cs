using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Dtos
{
    public class UsuarioRegisterDTO
    {
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Password { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
        public UsuarioRegisterDTO()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}
