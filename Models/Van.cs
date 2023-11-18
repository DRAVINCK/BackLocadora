using System.ComponentModel.DataAnnotations; 

namespace LocadoraAPI.Models;

public class Van : Veiculo
{
   public int? CapacidadeDePessoas {get; set;}  
}