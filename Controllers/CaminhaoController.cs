namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CaminhaoController : ControllerBase
{


    private LocadoraDbContext _context;
    public CaminhaoController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> cadastar(Caminhao caminhao)
    {
        await _context.AddAsync(caminhao);
        await _context.SaveChangesAsync();
        return Created("", caminhao);
    }

    [HttpGet()]
    [Route("Listar")]
    public async Task<ActionResult<IEnumerable<Caminhao>>> Listar()
    {
        if (_context.Caminhao is null)
            return NotFound();
        return await _context.Caminhao.ToListAsync();
    }

    [HttpGet()]
    [Route("Buscar/{placa}")]
    public async Task<ActionResult<Caminhao>> Buscar(string placa)
    {
        if (_context.Caminhao is null)
            return NotFound();
        var caminhao = await _context.Caminhao.FindAsync(placa);
        return Ok(caminhao);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<IActionResult> Alterar(Caminhao caminhao)
    {
        _context.Caminhao.Update(caminhao);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificarCapacidadeCarga/{placa}")]
    public async Task<IActionResult> modificarCapacidadeCarga(string placa, [FromForm] int modificarCapacidadeCarga)
    {
        var caminhao = await _context.Caminhao.FindAsync(placa);
        if (caminhao is null) return NotFound();
        caminhao.CapacidadeCarga = modificarCapacidadeCarga;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<ActionResult> Excluir(string placa)
    {

        var caminhao = await _context.Caminhao.FindAsync(placa);
        if (caminhao is null) return NotFound();
        _context.Caminhao.Remove(caminhao);
        await _context.SaveChangesAsync();
        return Ok();
    }


}
