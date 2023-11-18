namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class AviaoController : ControllerBase
{


    private LocadoraDbContext _context;
    public AviaoController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> cadastar(Aviao aviao)
    {
        await _context.AddAsync(aviao);
        await _context.SaveChangesAsync();
        return Created("", aviao);
    }

    [HttpGet()]
    [Route("Listar")]
    public async Task<ActionResult<IEnumerable<Aviao>>> Listar()
    {
        if (_context.Aviao is null)
            return NotFound();
        return await _context.Aviao.ToListAsync();
    }

    [HttpGet()]
    [Route("Buscar/{placa}")]
    public async Task<ActionResult<Aviao>> Buscar(string placa)
    {
        if (_context.Aviao is null)
            return NotFound();
        var aviao = await _context.Aviao.FindAsync(placa);
        return Ok(aviao);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Aviao aviao)
    {
        _context.Aviao.Update(aviao);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificarCapacidadePessoas/{placa}")]
    public async Task<IActionResult> modificarCapacidadePessoas(string placa, [FromForm] int modificarCapacidadePessoas)
    {
        var aviao = await _context.Aviao.FindAsync(placa);
        if (aviao is null) return NotFound();
        aviao.CapacidadeDePessoas = modificarCapacidadePessoas;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<ActionResult> Excluir(string placa)
    {

        var aviao = await _context.Aviao.FindAsync(placa);
        if (aviao is null) return NotFound();
        _context.Aviao.Remove(aviao);
        await _context.SaveChangesAsync();
        return Ok();
    }


}
