namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class BarcoController : ControllerBase
{
    private LocadoraDbContext _context;

    public BarcoController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar(Barco barco)
    {
        await _context.AddAsync(barco); 
        await _context.SaveChangesAsync();
        return Created("" ,barco);
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Barco>>> Listar()
    {
        if(_context.Barco is null)
            return NotFound();
        return await _context.Barco.ToListAsync();
    }

    [HttpGet]
    [Route("buscar/{placa}")]
    public async Task<ActionResult<Barco>> Buscar(string placa)
    {
        if(_context.Barco is null)
          return NotFound();
          var barco = await _context.Barco.FindAsync(placa);
          return Ok(barco);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Barco barco)
    {
        _context.Barco.Update(barco);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificarmotores/{placa}")]
    public async Task<IActionResult> ModificarMotores(string placa, [FromForm] int motores)
    {
        var barco = await _context.Barco.FindAsync(placa);
        if (barco is null) return NotFound();
        barco.Motores = motores;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<IActionResult> Excluir(string placa){
        var barco = await _context.Barco.FindAsync(placa);
        if(barco is null) return NotFound();
         _context.Barco.Remove(barco);
        await _context.SaveChangesAsync();
        return Ok();
    }
}