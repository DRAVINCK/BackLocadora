using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Models;

public class Onibus : Veiculo
{
    public int? CapacidadeDePessoas { get; set;}
  
}