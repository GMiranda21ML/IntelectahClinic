using IntelectahClinic.Models.enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelectahClinic.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Agendamento",
                columns: new[] { "PacienteId", "EspecialidadeId", "UnidadeId", "DataHora", "Status", "Observacoes" },
                values: new object[,]
                {
                    { "134bc707-95ef-4aca-adb0-0b138d0cd1d9", 1, 1, new DateTime(2025, 10, 1, 9, 0, 0), StatusAgendamento.ATENDIDO.ToString(), "Primeira consulta" },
                    { "7ec179b4-8fae-4ab9-92e6-feb7113d2ea5", 2, 2, new DateTime(2025, 10, 2, 14, 0, 0), StatusAgendamento.CANCELADO.ToString(), "Consulta de retorno" },
                    { "134bc707-95ef-4aca-adb0-0b138d0cd1d9", 4, 3, new DateTime(2025, 10, 3, 10, 30, 0), StatusAgendamento.AGENDADO.ToString(), "Hemograma" }
            });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Agendamento",
                keyColumns: new[] { "PacienteId", "EspecialidadeId", "UnidadeId", "DataHora" },
                keyValues: new object[] { "134bc707-95ef-4aca-adb0-0b138d0cd1d9", 1, 1, new DateTime(2025, 10, 1, 9, 0, 0) });

            migrationBuilder.DeleteData(
                table: "Agendamento",
                keyColumns: new[] { "PacienteId", "EspecialidadeId", "UnidadeId", "DataHora" },
                keyValues: new object[] { "7ec179b4-8fae-4ab9-92e6-feb7113d2ea5", 2, 2, new DateTime(2025, 10, 2, 14, 0, 0) });

            migrationBuilder.DeleteData(
                table: "Agendamento",
                keyColumns: new[] { "PacienteId", "EspecialidadeId", "UnidadeId", "DataHora" },
                keyValues: new object[] { "134bc707-95ef-4aca-adb0-0b138d0cd1d9", 4, 3, new DateTime(2025, 10, 3, 10, 30, 0) });
        }
    }
}
