using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelectahClinic.Migrations
{
    /// <inheritdoc />
    public partial class Agendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_AspNetUsers_PacienteId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Especialidades_EspecialidadeId",
                table: "Agendamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamento_Unidades_UnidadeId",
                table: "Agendamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento");

            migrationBuilder.RenameTable(
                name: "Agendamento",
                newName: "Agendamentos");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_UnidadeId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_UnidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_PacienteId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamento_EspecialidadeId",
                table: "Agendamentos",
                newName: "IX_Agendamentos_EspecialidadeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_AspNetUsers_PacienteId",
                table: "Agendamentos",
                column: "PacienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Especialidades_EspecialidadeId",
                table: "Agendamentos",
                column: "EspecialidadeId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamentos_Unidades_UnidadeId",
                table: "Agendamentos",
                column: "UnidadeId",
                principalTable: "Unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_AspNetUsers_PacienteId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Especialidades_EspecialidadeId",
                table: "Agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Agendamentos_Unidades_UnidadeId",
                table: "Agendamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agendamentos",
                table: "Agendamentos");

            migrationBuilder.RenameTable(
                name: "Agendamentos",
                newName: "Agendamento");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_UnidadeId",
                table: "Agendamento",
                newName: "IX_Agendamento_UnidadeId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_PacienteId",
                table: "Agendamento",
                newName: "IX_Agendamento_PacienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Agendamentos_EspecialidadeId",
                table: "Agendamento",
                newName: "IX_Agendamento_EspecialidadeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agendamento",
                table: "Agendamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_AspNetUsers_PacienteId",
                table: "Agendamento",
                column: "PacienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Especialidades_EspecialidadeId",
                table: "Agendamento",
                column: "EspecialidadeId",
                principalTable: "Especialidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agendamento_Unidades_UnidadeId",
                table: "Agendamento",
                column: "UnidadeId",
                principalTable: "Unidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
