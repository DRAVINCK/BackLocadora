namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CarroController : ControllerBase
{


    private LocadoraDbContext _context;
    public CarroController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> cadastar(Carro carro)
    {
        await _context.AddAsync(carro);
        await _context.SaveChangesAsync();
        return Created("", carro);
    }

    [HttpGet()]
    [Route("Listar")]
    public async Task<ActionResult<IEnumerable<Carro>>> Listar()
    {
        if (_context.Carro is null)
            return NotFound();
        return await _context.Carro.ToListAsync();
    }

    [HttpGet()]
    [Route("Buscar/{placa}")]
    public async Task<ActionResult<Carro>> Buscar(string placa)
    {
        if (_context.Carro is null)
            return NotFound();
        var carro = await _context.Carro.FindAsync(placa);
        return Ok(carro);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Carro carro)
    {
        _context.Carro.Update(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificardescricao/{placa}")]
    public async Task<IActionResult> ModificarDescricao(string placa, [FromForm] string descricao)
    {
        var carro = await _context.Carro.FindAsync(placa);
        if (carro is null) return NotFound();
        carro.Descricao = descricao;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<ActionResult> Excluir(string placa)
    {

        var carro = await _context.Carro.FindAsync(placa);
        if (carro is null) return NotFound();
        _context.Carro.Remove(carro);
        await _context.SaveChangesAsync();
        return Ok();
    }


}
