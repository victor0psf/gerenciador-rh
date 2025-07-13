using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server_dotnet.Migrations
{
    /// <inheritdoc />
    public partial class AjustesSalarioFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ferias_funcionarios_FuncionarioId",
                table: "ferias");

            migrationBuilder.DropForeignKey(
                name: "FK_historicoAlteracao_funcionarios_FuncionarioId",
                table: "historicoAlteracao");

            migrationBuilder.DropForeignKey(
                name: "FK_salario_funcionarios_funcionarios_FuncionarioId",
                table: "salario_funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ferias",
                table: "ferias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salario_funcionarios",
                table: "salario_funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_historicoAlteracao",
                table: "historicoAlteracao");

            migrationBuilder.RenameTable(
                name: "funcionarios",
                newName: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "ferias",
                newName: "Ferias");

            migrationBuilder.RenameTable(
                name: "salario_funcionarios",
                newName: "SalarioFuncionarios");

            migrationBuilder.RenameTable(
                name: "historicoAlteracao",
                newName: "HistoricoAlteracoes");

            migrationBuilder.RenameIndex(
                name: "IX_ferias_FuncionarioId",
                table: "Ferias",
                newName: "IX_Ferias_FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_salario_funcionarios_FuncionarioId",
                table: "SalarioFuncionarios",
                newName: "IX_SalarioFuncionarios_FuncionarioId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "HistoricoAlteracoes",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_historicoAlteracao_FuncionarioId",
                table: "HistoricoAlteracoes",
                newName: "IX_HistoricoAlteracoes_FuncionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "Funcionarios",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ferias",
                table: "Ferias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalarioFuncionarios",
                table: "SalarioFuncionarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HistoricoAlteracoes",
                table: "HistoricoAlteracoes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ferias_Funcionarios_FuncionarioId",
                table: "Ferias",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoAlteracoes_Funcionarios_FuncionarioId",
                table: "HistoricoAlteracoes",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalarioFuncionarios_Funcionarios_FuncionarioId",
                table: "SalarioFuncionarios",
                column: "FuncionarioId",
                principalTable: "Funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ferias_Funcionarios_FuncionarioId",
                table: "Ferias");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoAlteracoes_Funcionarios_FuncionarioId",
                table: "HistoricoAlteracoes");

            migrationBuilder.DropForeignKey(
                name: "FK_SalarioFuncionarios_Funcionarios_FuncionarioId",
                table: "SalarioFuncionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ferias",
                table: "Ferias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalarioFuncionarios",
                table: "SalarioFuncionarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HistoricoAlteracoes",
                table: "HistoricoAlteracoes");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "funcionarios");

            migrationBuilder.RenameTable(
                name: "Ferias",
                newName: "ferias");

            migrationBuilder.RenameTable(
                name: "SalarioFuncionarios",
                newName: "salario_funcionarios");

            migrationBuilder.RenameTable(
                name: "HistoricoAlteracoes",
                newName: "historicoAlteracao");

            migrationBuilder.RenameIndex(
                name: "IX_Ferias_FuncionarioId",
                table: "ferias",
                newName: "IX_ferias_FuncionarioId");

            migrationBuilder.RenameIndex(
                name: "IX_SalarioFuncionarios_FuncionarioId",
                table: "salario_funcionarios",
                newName: "IX_salario_funcionarios_FuncionarioId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "historicoAlteracao",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_HistoricoAlteracoes_FuncionarioId",
                table: "historicoAlteracao",
                newName: "IX_historicoAlteracao_FuncionarioId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cargo",
                table: "funcionarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ferias",
                table: "ferias",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_salario_funcionarios",
                table: "salario_funcionarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_historicoAlteracao",
                table: "historicoAlteracao",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ferias_funcionarios_FuncionarioId",
                table: "ferias",
                column: "FuncionarioId",
                principalTable: "funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_historicoAlteracao_funcionarios_FuncionarioId",
                table: "historicoAlteracao",
                column: "FuncionarioId",
                principalTable: "funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_salario_funcionarios_funcionarios_FuncionarioId",
                table: "salario_funcionarios",
                column: "FuncionarioId",
                principalTable: "funcionarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
