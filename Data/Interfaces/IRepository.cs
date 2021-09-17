using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Models;

namespace TestWebApi.Data.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIDAsync(int id);
        Task<Usuario> GetUsuarioByNombreAsync(string nombre);

        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIDAsync(int id);
        Task<Producto> GetProductoByNombreAsync(string nombre);


    }
}
