using IntelectahClinic.Models.enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntelectahClinic.Migrations
{
    /// <inheritdoc />
    public partial class PopularMaisEspecialidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Especialidades",
            columns: new[] { "NomeEspecialidade", "Tipo" },
            values: new object[,]
            {
                { "Dermatologia", TipoServico.CONSULTA_MEDICA.ToString() },
                { "Neurologia", TipoServico.CONSULTA_MEDICA.ToString() },
                { "Ginecologia", TipoServico.CONSULTA_MEDICA.ToString() },
                { "Psiquiatria", TipoServico.CONSULTA_MEDICA.ToString() },
                { "Endocrinologia", TipoServico.CONSULTA_MEDICA.ToString() },

                { "Glicemia", TipoServico.EXAME_LABORATORIAL.ToString() },
                { "Colesterol", TipoServico.EXAME_LABORATORIAL.ToString() },
                { "Ureia e Creatinina", TipoServico.EXAME_LABORATORIAL.ToString() },

                { "Ultrassonografia", TipoServico.EXAME_IMAGEM.ToString() },
                { "Mamografia", TipoServico.EXAME_IMAGEM.ToString() },
                { "Tomografia Computadorizada", TipoServico.EXAME_IMAGEM.ToString() }
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DELETE FROM Especialidades 
                WHERE NomeEspecialidade IN (
                    'Dermatologia','Neurologia','Ginecologia','Psiquiatria','Endocrinologia',
                    'Glicemia','Colesterol','Ureia e Creatinina',
                    'Ultrassonografia','Mamografia','Tomografia Computadorizada'
                )
            ");
        }
    }
}
