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
    public class AluguelController : ControllerBase
    {
        private LocadoraDbContext _context;

        public AluguelController(LocadoraDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Cadastrar")]
        public async Task<IActionResult> Cadastrar(Aluguel aluguel)
        {
            // Verificar se o Cliente existe no banco de dados com base no CPF

            var cliente = await _context.Cliente.FirstOrDefaultAsync(x => x.Cpf == aluguel.ClienteCpf);
            if (cliente == null)
            {
                return BadRequest("Cliente não encontrado.");
            }

            // Verificar se o Veículo existe no banco de dados com base na Placa
            var veiculo = await _context.Veiculo.FirstOrDefaultAsync(x => x.Placa == aluguel.VeiculoPlaca);
            if (veiculo == null)
            {
                return BadRequest("Veículo não encontrado.");
            }

            // Defina as propriedades de navegação para o cliente e veículo
            aluguel.ClienteCpf = cliente.Cpf;
            aluguel.Cliente = cliente;
            aluguel.VeiculoPlaca = veiculo.Placa;
            aluguel.Veiculo= veiculo;

            // Defina o Id para o novo aluguel (por exemplo, gerando um novo GUID)
            aluguel.Id = Guid.NewGuid().ToString();

            // Registre o novo aluguel
            _context.Aluguel.Add(aluguel);
            await _context.SaveChangesAsync();

            return Ok("Aluguel registrado com sucesso");
        }




        [HttpGet]
        [Route("Listar")]
        public async Task<ActionResult<IEnumerable<Aluguel>>> Listar()
        {
            var alugueisCompletos = await _context.Aluguel
                .Include(a => a.Cliente)
                .Include(a => a.Veiculo)
                .ToListAsync();

            var resultado = alugueisCompletos.Select(aluguel => new
            {
                Id = aluguel.Id,
                Cli = aluguel.Cliente?.Nome ?? "Cliente não encontrado",
                Veiculo = aluguel.Veiculo?.Placa ?? "Veículo não encontrado"
            });

            return Ok(resultado);
        }



        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<ActionResult<Aluguel>> Buscar(string id)
        {
            if (_context.Aluguel is null)
                return NotFound();

            var aluguel = await _context.Aluguel.FindAsync(id);
            return Ok(aluguel);
        }

        [HttpPut]
        [Route("Alterar")]
        public async Task<IActionResult> Alterar(Aluguel aluguel)
        {
            _context.Aluguel.Update(aluguel);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        [Route("ModificarVeiculoAlugado/{id}")]
        public async Task<IActionResult> ModificarVeiculoAlugado(string id, [FromForm] string veiculoAlugado)
        {
            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel is null) return NotFound();
            aluguel.Veiculo = new Veiculo { Placa = veiculoAlugado };

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Excluir/{id}")]
        public async Task<IActionResult> Excluir(string id)
        {
            var aluguel = await _context.Aluguel.FindAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }

            _context.Aluguel.Remove(aluguel);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

