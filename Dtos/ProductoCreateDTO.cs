using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApi.Dtos
{
    public class ProductoCreateDTO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }

        public ProductoCreateDTO()
        {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }

    }
}
