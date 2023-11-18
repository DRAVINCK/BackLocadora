using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Models;

public class Caminhao : Veiculo
{
    public int? CapacidadeCarga { get; set;}
  
}