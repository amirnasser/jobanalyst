using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAnalyzer.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "JobObjects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    company = table.Column<string>(type: "varchar(50)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyUrl = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyLinkedIn = table.Column<string>(type: "varchar(2500)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobPost = table.Column<string>(type: "varchar(1500)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Coverletter = table.Column<string>(type: "varchar(2500)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MissingRequirements = table.Column<string>(type: "varchar(1500)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Reason = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Matched = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JobRequirements = table.Column<string>(type: "varchar(1500)", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTitle = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Applied = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    JobFile = table.Column<byte[]>(type: "blob", nullable: false),
                    ResumeFile = table.Column<byte[]>(type: "blob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobObjects", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    postid = table.Column<long>(type: "bigint(10)", nullable: false),
                    title = table.Column<string>(type: "varchar(66)", maxLength: 66, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    company = table.Column<string>(type: "varchar(88)", maxLength: 88, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    logo = table.Column<string>(type: "varchar(242)", maxLength: 242, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    company_linkedin = table.Column<string>(type: "varchar(119)", maxLength: 119, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    location = table.Column<string>(type: "varchar(28)", maxLength: 28, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    postdate = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    when = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    jobpost = table.Column<string>(type: "varchar(304)", maxLength: 304, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.postid);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "job_processed",
                columns: table => new
                {
                    postid = table.Column<long>(type: "bigint(10)", nullable: false),
                    file_id = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    jobpost = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    result = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    company = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    coverletter = table.Column<string>(type: "varchar(2500)", maxLength: 2500, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    missing_requirements = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    job_requrirements = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reason = table.Column<string>(type: "text", nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    matched = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    job_title = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, collation: "utf8mb4_uca1400_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    applied = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    postdate = table.Column<DateOnly>(type: "date", nullable: false),
                    apply_date = table.Column<DateOnly>(type: "date", nullable: false),
                    location = table.Column<sbyte>(type: "tinyint(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.postid);
                    table.ForeignKey(
                        name: "FK_job_processed_jobs",
                        column: x => x.postid,
                        principalTable: "jobs",
                        principalColumn: "postid");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_uca1400_ai_ci");

            migrationBuilder.CreateIndex(
                name: "jobpost",
                table: "jobs",
                column: "jobpost",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_processed");

            migrationBuilder.DropTable(
                name: "JobObjects");

            migrationBuilder.DropTable(
                name: "jobs");
        }
    }
}
