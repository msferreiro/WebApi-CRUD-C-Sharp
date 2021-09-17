using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Data.Interfaces;
using TestWebApi.Models;

namespace TestWebApi.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;

        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIDAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            return producto;
        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Nombre == nombre);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var productos = await _context.Productos.ToListAsync();
            return productos;
        }

        public async Task<Usuario> GetUsuarioByIDAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Nombre == nombre);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; 
        }
    }
}
