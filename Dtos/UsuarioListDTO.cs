using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Dtos
{
    public class UsuarioListDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
    }
}
