using LocadoraAPI.Models;
using Microsoft.EntityFrameworkCore;

public class LocadoraDbContext : DbContext
{
    public DbSet<Carro> Carro {get; set; }
    public DbSet<Onibus> Onibus {get; set;}
    public DbSet<Aluguel> Aluguel {get;set;}
    public DbSet<Cliente> Cliente {get;set;}
    public DbSet<Barco> Barco {get; set;}
    public DbSet<Carroca> Carroca {get; set;}
    public DbSet<Van> Van {get; set;}
    public DbSet<Aviao> Aviao {get; set;}
    public DbSet<Caminhao> Caminhao {get; set;}
    public DbSet<Veiculo> Veiculo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: "DataSource=locadora.db;cache=Shared");
    }
}