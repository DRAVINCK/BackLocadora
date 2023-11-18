using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Models;

public class Aviao : Veiculo
{
    public int? CapacidadeDePessoas { get; set;}
  
}