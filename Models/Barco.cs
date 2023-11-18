using System.ComponentModel.DataAnnotations; //pra key funcionar

namespace LocadoraAPI.Models;

public class Barco : Veiculo
{
    public int? Motores {get; set;}
}