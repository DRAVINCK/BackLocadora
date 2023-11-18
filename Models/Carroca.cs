using System.ComponentModel.DataAnnotations; 

namespace LocadoraAPI.Models;

public class Carroca : Veiculo
{
   public int? Cavalos {get; set;}
}