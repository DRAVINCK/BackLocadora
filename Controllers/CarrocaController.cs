namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class CarrocaController : ControllerBase
{
    private LocadoraDbContext _context;

    public CarrocaController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar (Carroca carroca)
    {
        await _context.AddAsync(carroca);
        await _context.SaveChangesAsync();
        return Created("",carroca);
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Carroca>>> Listar()
    {
        if(_context.Carroca is null)
            return NotFound();
        return await _context.Carroca.ToListAsync();
    }


    [HttpGet()]
    [Route("buscar/{placa}")]
    public async Task<ActionResult<Carroca>> Buscar (string placa)
    {
        if(_context.Carroca is null)
            return NotFound();
            var carroca = await _context.Carroca.FindAsync(placa);
        return Ok(carroca);
    }



    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Carroca carroca)
    {
        _context.Carroca.Update(carroca);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificarCavalos/{placa}")]
    public async Task<IActionResult> ModificarCavalo(string placa, [FromForm] int cavalos)
    {
        var carroca = await _context.Carroca.FindAsync(placa);
        if (carroca is null) return NotFound();
        carroca.Cavalos = cavalos;
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<IActionResult> Excluir(string placa){
        var carroca = await _context.Carroca.FindAsync(placa);
        if(carroca is null) return NotFound();
         _context.Carroca.Remove(carroca);
        await _context.SaveChangesAsync();
        return Ok();
    }

}