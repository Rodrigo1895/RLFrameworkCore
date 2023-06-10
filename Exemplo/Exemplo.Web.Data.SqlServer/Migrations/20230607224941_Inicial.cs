using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exemplo.Web.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "seq_cliente");

            migrationBuilder.CreateSequence<int>(
                name: "seq_pedido");

            migrationBuilder.CreateSequence<int>(
                name: "seq_produto");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR seq_cliente"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR seq_produto"),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR seq_pedido"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    DataHoraInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraConclusao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.IdPedido);
                    table.ForeignKey(
                        name: "FK_pedido_cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido_item",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    IdItem = table.Column<int>(type: "int", nullable: false),
                    IdProduto = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false),
                    ValorUnit = table.Column<decimal>(type: "decimal(12,4)", precision: 12, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_item", x => new { x.IdPedido, x.IdItem });
                    table.ForeignKey(
                        name: "FK_pedido_item_pedido_IdPedido",
                        column: x => x.IdPedido,
                        principalTable: "pedido",
                        principalColumn: "IdPedido",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_item_produto_IdProduto",
                        column: x => x.IdProduto,
                        principalTable: "produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cliente_CPF",
                table: "cliente",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_IdCliente",
                table: "pedido",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_item_IdProduto",
                table: "pedido_item",
                column: "IdProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_item");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropSequence(
                name: "seq_cliente");

            migrationBuilder.DropSequence(
                name: "seq_pedido");

            migrationBuilder.DropSequence(
                name: "seq_produto");
        }
    }
}
