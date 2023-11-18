namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class VanController : ControllerBase
{
    private LocadoraDbContext _context;

    public VanController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpPost()]
    [Route("cadastrar")]
    public async Task<IActionResult> Cadastrar (Van van)
    {
        await _context.AddAsync(van);
        await _context.SaveChangesAsync();
        return Created("",van);
    }

    [HttpGet()]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Van>>> Listar()
    {
        if(_context.Van is null)
            return NotFound();
        return await _context.Van.ToListAsync();
    }


    [HttpGet]
    [Route("buscar/{placa}")]
    public async Task<ActionResult<Van>> Buscar (string placa)
    {
        if(_context.Van is null)
            return NotFound();
            var van = await _context.Van.FindAsync(placa);
        return Ok(van);
    }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar(Van van)
    {
        _context.Van.Update(van);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPatch]
    [Route("modificarMotor/{placa}")]
    public async Task<IActionResult> ModificarMotor(string placa, [FromForm] int capacidadeDepessoas)
    {
        var van = await _context.Van.FindAsync(placa);
        if (van is null) return NotFound();
        van.CapacidadeDePessoas = capacidadeDepessoas;
        await _context.SaveChangesAsync();
        return Ok();
    }

        [HttpDelete]
    [Route("excluir/{placa}")]
    public async Task<IActionResult> Excluir(string placa){
        var van = await _context.Van.FindAsync(placa);
        if(van is null) return NotFound();
         _context.Van.Remove(van);
        await _context.SaveChangesAsync();
        return Ok();
    }

}