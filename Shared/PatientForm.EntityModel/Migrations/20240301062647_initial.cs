using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PatientForm.EntityModel.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disease",
                columns: table => new
                {
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiseaseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disease", x => x.DiseaseId);
                });

            migrationBuilder.CreateTable(
                name: "Ncd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ncd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientInfo",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Epilepsy = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientInfo", x => x.PatientId);
                });

            migrationBuilder.CreateTable(
                name: "AllergiesDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AllergiesID = table.Column<int>(type: "int", nullable: false),
                    PatientInfoPatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergiesDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllergiesDetail_PatientInfo_PatientInfoPatientId",
                        column: x => x.PatientInfoPatientId,
                        principalTable: "PatientInfo",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "NcdDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    NcdId = table.Column<int>(type: "int", nullable: false),
                    PatientInfoPatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcdDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NcdDetail_PatientInfo_PatientInfoPatientId",
                        column: x => x.PatientInfoPatientId,
                        principalTable: "PatientInfo",
                        principalColumn: "PatientId");
                });

            migrationBuilder.InsertData(
                table: "Allergie",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Drugs - Penicillin" },
                    { 2, "Drugs - Others" },
                    { 3, "Animals" },
                    { 4, "Food" },
                    { 5, "Oinments" },
                    { 6, "Plant" },
                    { 7, "Sprays" },
                    { 8, "Others" },
                    { 9, "No Allergies" }
                });

            migrationBuilder.InsertData(
                table: "Disease",
                columns: new[] { "DiseaseId", "DiseaseName" },
                values: new object[,]
                {
                    { 1, "Fever" },
                    { 2, "Brain" },
                    { 3, "Heart" },
                    { 4, "Kidney" }
                });

            migrationBuilder.InsertData(
                table: "Ncd",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Asthma" },
                    { 2, "Cancer" },
                    { 3, "Disorders of ear" },
                    { 4, "Disorder of eye" },
                    { 5, "Mental illness" },
                    { 6, "Oral health problems" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergiesDetail_PatientInfoPatientId",
                table: "AllergiesDetail",
                column: "PatientInfoPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_NcdDetail_PatientInfoPatientId",
                table: "NcdDetail",
                column: "PatientInfoPatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergie");

            migrationBuilder.DropTable(
                name: "AllergiesDetail");

            migrationBuilder.DropTable(
                name: "Disease");

            migrationBuilder.DropTable(
                name: "Ncd");

            migrationBuilder.DropTable(
                name: "NcdDetail");

            migrationBuilder.DropTable(
                name: "PatientInfo");
        }
    }
}
