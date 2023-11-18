namespace LocadoraAPI.Controllers;
using LocadoraAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


[ApiController]
[Route("[controller]")]
public class VeiculosController : ControllerBase
{
    private LocadoraDbContext _context;

    public VeiculosController(LocadoraDbContext context)
    {
        _context = context;
    }

    [HttpGet()]
    [Route("Buscar/{placa}")]
        public async Task<ActionResult<Veiculo>> Buscar(string placa)
        {

            if (_context.Veiculo is null)
            {
                return NotFound();
            }
            var veiculo = await _context.Veiculo.FindAsync(placa);

            return Ok(veiculo);
        }

}