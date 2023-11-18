namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class OnibusController : ControllerBase
{
    private LocadoraDbContext _context;

    public OnibusController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Cadastrar")]
    public async Task<IActionResult> Cadastrar(Onibus onibus)
    {
        _context.Onibus.Add(onibus);
        await _context.SaveChangesAsync();
        return Created("", onibus);
    }

    [HttpGet]
    [Route("Listar")]
    public async Task<ActionResult<IEnumerable<Onibus>>> Listar()
    {
        var onibusList = await _context.Onibus.ToListAsync();
        return Ok(onibusList);
    }

    [HttpGet]
    [Route("Buscar/{placa}")]
    public async Task<ActionResult<Onibus>> Buscar(string placa)
    {
        if (_context.Onibus is null)
        {
            return NotFound();
        }
        var onibus = await _context.Onibus.FindAsync(placa);
        return Ok(onibus);
    }


    [HttpPut]
    [Route("Alterar")]
    public async Task<IActionResult> Alterar(Onibus onibus)
    {
        _context.Onibus.Update(onibus);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("ModificarCapacidade/{placa}")]
    public async Task<IActionResult> ModificarCapacidade(string placa, [FromForm] int capacidade)
    {
        var onibus = await _context.Onibus.FindAsync(placa);
        if (onibus is null) return NotFound();
        onibus.CapacidadeDePessoas = capacidade;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("Excluir/{placa}")]
    public async Task<IActionResult> Excluir(string placa)
    {
        var onibus = await _context.Onibus.FindAsync(placa);
        if (onibus == null)
        {
            return NotFound();
        }

        _context.Onibus.Remove(onibus);
        await _context.SaveChangesAsync();
        return Ok();
    }

    private bool OnibusExists(string placa)
    {
        return _context.Onibus.Any(e => e.Placa == placa);
    }
}
