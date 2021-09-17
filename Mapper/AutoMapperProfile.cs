using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Dtos;
using TestWebApi.Models;

namespace TestWebApi.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // POST Create
            CreateMap<Producto, ProductoCreateDTO>();
            // PUT Update
            CreateMap<ProductoUpdateDTO, Producto>();

            // GET
            CreateMap<Producto,ProductoToListDTO>();

            CreateMap<UsuarioRegisterDTO, Usuario>();
            CreateMap<UsuarioLoginDTO, Usuario>();
            CreateMap<Usuario,UsuarioListDTO>();
            CreateMap<Usuario, UsuarioRegisterDTO>();
            CreateMap<Usuario, UsuarioLoginDTO>();

        }
    }
}
