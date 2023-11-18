namespace LocadoraAPI.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

public class Aluguel
{
    [Key]
    public string? Id { get; set; }
    public string? ClienteCpf { get;set;}
    public Cliente? Cliente { get; set; }
    public string? VeiculoPlaca { get; set; }
    public Veiculo? Veiculo { get; set; }
    
}