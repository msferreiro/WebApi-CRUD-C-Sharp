using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Data.Interfaces;
using TestWebApi.Models;

namespace TestWebApi.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> ExisteUsuario(string correo)
        {
            if (await _context.Usuarios.AnyAsync(c => c.CorreoElectronico == correo))
            return true;

            return false;
        }

        public async Task<Usuario> Login(string correo, string password)
        {
            // Obtenemos el usuario correspondiente al correo 
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);
            if (usuario == null)
                return null;

            // Verificamos que le password es igual al password que tiene guardado el usuario
            if (!VerifyPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
                return null;

            return usuario;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordsalt)
        {
            // Obtenemos el hmac con el algoritmo SHA512 con el key (passwordsalt) creado al crear el usuario
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordsalt))
            {
                // Hacemos la conversion a bytes del password
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                // Comparamos byte a byte el password con la que tenemos en la BD
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordhash[i])
                        return false;
                }

                return true;
            }
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}
