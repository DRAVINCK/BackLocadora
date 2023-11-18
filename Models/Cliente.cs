namespace LocadoraAPI.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Cliente
{
    [Key]
    public string? Cpf{get;set;}

    public string? Nome{get;set;}

    

}