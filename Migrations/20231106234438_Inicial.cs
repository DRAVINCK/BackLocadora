using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocadoraAPI.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Veiculo",
                columns: table => new
                {
                    Placa = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Aviao_CapacidadeDePessoas = table.Column<int>(type: "INTEGER", nullable: true),
                    Motores = table.Column<int>(type: "INTEGER", nullable: true),
                    CapacidadeCarga = table.Column<int>(type: "INTEGER", nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Cavalos = table.Column<int>(type: "INTEGER", nullable: true),
                    Onibus_CapacidadeDePessoas = table.Column<int>(type: "INTEGER", nullable: true),
                    CapacidadeDePessoas = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculo", x => x.Placa);
                });

            migrationBuilder.CreateTable(
                name: "Aluguel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ClienteCpf = table.Column<string>(type: "TEXT", nullable: true),
                    VeiculoPlaca = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluguel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluguel_Cliente_ClienteCpf",
                        column: x => x.ClienteCpf,
                        principalTable: "Cliente",
                        principalColumn: "Cpf");
                    table.ForeignKey(
                        name: "FK_Aluguel_Veiculo_VeiculoPlaca",
                        column: x => x.VeiculoPlaca,
                        principalTable: "Veiculo",
                        principalColumn: "Placa");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluguel_ClienteCpf",
                table: "Aluguel",
                column: "ClienteCpf");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguel_VeiculoPlaca",
                table: "Aluguel",
                column: "VeiculoPlaca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluguel");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Veiculo");
        }
    }
}
