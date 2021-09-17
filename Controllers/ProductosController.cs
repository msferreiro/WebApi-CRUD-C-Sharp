using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApi.Data.Interfaces;
using TestWebApi.Dtos;
using TestWebApi.Models;

namespace TestWebApi.Controllers
{
    [Authorize]
    [ApiController]  // Se debe agregar a cada controlador
    [Route("api/[controller]")] // indica la ruta, osea, en el localhost sería /api/Productos. [controller] toma el }
                                // el nombre del Controlador.
    public class ProductosController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        public ProductosController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var producto = await _repo.GetProductoByIDAsync(id);

            if (producto == null)
                return NotFound("Producto no encontrado");

            return Ok(producto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var productos = await _repo.GetProductosAsync();

            List<ProductoToListDTO> ProductosToList = new List<ProductoToListDTO>();

            foreach (Producto p in productos)
            {
                ProductoToListDTO pToList = new ProductoToListDTO();
                pToList = _mapper.Map<ProductoToListDTO>(p);
                ProductosToList.Add(pToList);
            }

            //_mapper.Map(productos, ProductosToList);

            return Ok(ProductosToList);

        }


        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDTO productoDTO)
        {
            var productoNuevo = _mapper.Map<Producto>(productoDTO);

            _repo.Add(productoNuevo);
            if (await _repo.SaveAll())
                return Ok(productoNuevo);
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductoUpdateDTO productoDTO)
        {
            if (id != productoDTO.Id)
                return BadRequest("Los Id no coinciden.");

            Producto productoToUpdate = await _repo.GetProductoByIDAsync(productoDTO.Id);

            if (productoToUpdate == null)
                return NotFound("No se encuentra el producto indicado.");

            _mapper.Map(productoDTO, productoToUpdate);
       
          
            if (!await _repo.SaveAll())
                return NoContent();

            return Ok(productoToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productoToDelete = await _repo.GetProductoByIDAsync(id);

            if (productoToDelete == null)
                return NotFound("No se encuentra el producto indicado.");

            _repo.Delete(productoToDelete); 
            if (! await _repo.SaveAll())
                return BadRequest("No se pudo eliminar el producto.");

            return Ok("Producto eliminado.");
        }
    }
}
