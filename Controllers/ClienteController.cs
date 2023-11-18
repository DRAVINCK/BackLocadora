using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {
        private LocadoraDbContext _context;

        public ClienteController(LocadoraDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Created("", cliente);
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> Listar()
        {
            var clienteList = await _context.Cliente.ToListAsync();
            return Ok(clienteList);
        }

        [HttpGet]
        [Route("Buscar/{cpf}")]
        public async Task<ActionResult<Cliente>> Buscar(string cpf)
        {
            var cliente = await _context.Cliente.FindAsync(cpf);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPatch]
        [Route("ModificarNome/{cpf}")]
        public async Task<IActionResult> ModificarCapacidade(string cpf, [FromForm] string nome)
        {
            var cliente = await _context.Cliente.FindAsync(cpf);
            if (cliente is null) return NotFound();
            cliente.Nome = nome;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Excluir/{cpf}")]
        public async Task<IActionResult> Excluir(string cpf)
        {
            var cliente = await _context.Cliente.FindAsync(cpf);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
