using IntelectahClinic.Models.enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelectahClinic.Migrations
{
    /// <inheritdoc />
    public partial class PopularEspecialidadesEUnidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "Especialidades",
               columns: new[] { "NomeEspecialidade", "Tipo" },
               values: new object[,]
               {
                    { "Cardiologia", TipoServico.CONSULTA_MEDICA.ToString() },
                    { "Pediatria", TipoServico.CONSULTA_MEDICA.ToString() },
                    { "Ortopedia", TipoServico.CONSULTA_MEDICA.ToString() },
                    { "Hemograma", TipoServico.EXAME_LABORATORIAL.ToString() },
                    { "Raio-X", TipoServico.EXAME_IMAGEM.ToString() },
                    { "Ressonância", TipoServico.EXAME_IMAGEM.ToString() }
               });

            migrationBuilder.InsertData(
                table: "Unidades",
                columns: new[] { "NomeUnidade", "Endereco" },
                values: new object[,]
                {
                    { "Clínica Central", "Rua Principal, 100" },
                    { "Unidade Norte", "Av. Norte, 200" },
                    { "Unidade Sul", "Av. Sul, 300" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Especialidades", keyColumn: "NomeEspecialidade", keyValues: new object[] { "Cardiologia", "Pediatria", "Ortopedia", "Hemograma", "Raio-X", "Ressonância" });
            migrationBuilder.DeleteData(table: "Unidades", keyColumn: "NomeUnidade", keyValues: new object[] { "Clínica Central", "Unidade Norte", "Unidade Sul" });
        }
    }
}
