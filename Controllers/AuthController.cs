using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Data.Interfaces;
using TestWebApi.Dtos;
using TestWebApi.Models;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Controllers
{
    [ApiController]  // Se debe agregar a cada controlador
    [Route("api/[controller]")] // indica la ruta, osea, en el localhost sería /api/Productos. [controller] toma el }
                                // el nombre del Controlador.
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;


        public AuthController(IAuthRepository repo, ITokenService tokenService, IMapper mapper)
        {
            _repo = repo;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegisterDTO usuarioRegisterDTO)
        {
            usuarioRegisterDTO.CorreoElectronico = usuarioRegisterDTO.CorreoElectronico.ToLower();
            if (await _repo.ExisteUsuario(usuarioRegisterDTO.CorreoElectronico))
                return BadRequest("Ya existe un usuario registrado con ese correo electrónico");

            var usuarioNuevo = _mapper.Map<Usuario>(usuarioRegisterDTO);
            var usuarioCreado = await _repo.Registrar(usuarioNuevo, usuarioRegisterDTO.Password);
            var usuarioResult = _mapper.Map<UsuarioRegisterDTO>(usuarioCreado);

            return Ok(usuarioResult);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            var usuario = await _repo.Login(usuarioLoginDTO.CorreoElectronico, usuarioLoginDTO.Password);

            if (usuario == null)
                return Unauthorized("El usuario no se encuentra registrado.");

            var usuarioResult = _mapper.Map<UsuarioListDTO>(usuario);
            var token = _tokenService.CreateToken(usuario);

            return Ok(new { token=token, usuario=usuarioResult });
                
        }
    }
}
