using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceUpdateRepository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompanyName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CompanyAddres = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UserPhone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Market = table.Column<int>(type: "int", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsAccessForbidden = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<int>(type: "int", nullable: true),
                    GenericID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Environment= table.Column<string>(type: "nvarchar(500)", maxLength:500, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "batch",
               columns: table => new
               {
                   batch_id = table.Column<int>(type: "int", nullable: false),
                   batch_name = table.Column<string>(type: "varchar(50)", maxLength:50, nullable: true),
                   status = table.Column<string>(type: "varchar(50)", maxLength:50, nullable: true),
                   comments = table.Column<string>(type: "varchar(1000)", maxLength:1000, nullable: true),
                   effectivedate = table.Column<DateTime>(type: "datetime", nullable: true),
                   currentstage = table.Column<string>(type: "varchar(50)", maxLength:50, nullable: true),
                   createdby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                   lastupdatedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                   createddttm = table.Column<DateTime>(type: "datetime",nullable: true),
                   lastupddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                   appliedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                   applieddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                   expirydttm = table.Column<DateTime>(type: "datetime", nullable: true),
                   IsActive= table.Column<DateTime>(type: "bit", nullable: true),
                   createduserid= table.Column<DateTime>(type: "int", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_batch", x => x.batch_id);
               });

            migrationBuilder.CreateTable(
              name: "batch_process",
              columns: table => new
              {
                  batch_id = table.Column<int>(type: "int", nullable: false),
                  process_id = table.Column<int>(type: "int", nullable: false),
                  environment = table.Column<string>(type: "nchar(500)", maxLength: 500, nullable: true),
                  comments = table.Column<string>(type: "nchar(1000)", maxLength: 1000, nullable: true),
                  status = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: true),
                  rundttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  appliedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  applieddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  runby = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: true),
                  effectivedate = table.Column<DateTime>(type: "datetime", nullable: true),
                  Log = table.Column<string>(type: "text", nullable: true),
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_batch_process", x => x.process_id);
              });
            migrationBuilder.CreateTable(
            name: "environment",
            columns: table => new
            {
                environment_id = table.Column<int>(type: "int", nullable: false),
                name = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: true),
                server = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: true),
                dbname = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: true),
                username = table.Column<string>(type: "nchar(100)", maxLength: 100, nullable: true),
                password = table.Column<string>(type: "nchar(50)", maxLength: 50, nullable: true),
                createdby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                createddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                lastupdatedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                lastupddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                url = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_environment", x => x.environment_id);
            });

            migrationBuilder.CreateTable(
              name: "batch_lines",
              columns: table => new
              {
                  batch_line_id = table.Column<int>(type: "int", nullable: false),
                  batch_id = table.Column<int>(type: "int", nullable: true),
                  retrievedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  identifier = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  matrix = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  effectivedate = table.Column<string>(type: "datetime", nullable: true),
                  createdby = table.Column<bool>(type: "varchar(100)", maxLength: 100, nullable: true),
                  lastupdatedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  createddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  lastupddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  matrix_field = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_batch_lines", x => x.batch_line_id);
              });

              migrationBuilder.CreateTable(
              name: "batch_items",
              columns: table => new
              {
                  batch_item_id = table.Column<int>(type: "int", nullable: false),
                  batch_id = table.Column<int>(type: "int", nullable: true),
                  batch_line_id = table.Column<int>(type: "int", nullable: true),
                  Matrix_id = table.Column<int>(type: "int", nullable: true),
                  entityid = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  xmldata = table.Column<string>(type: "xml", nullable: true),
                  oldprice = table.Column<string>(type: "nvarchar(2000)", maxLength:2000, nullable: true),
                  newprice = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                  changepercentage = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                  percentage = table.Column<bool>(type: "bit", nullable: true),
                  createdby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  lastupdatedby = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                  createddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  lastupddttm = table.Column<DateTime>(type: "datetime", nullable: true),
                  matrix_name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                  changemultiple = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                  description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                  usermarket = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_batch_items", x => x.batch_item_id);
              });

            migrationBuilder.CreateTable(
            name: "pagelogs",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false),
                userid = table.Column<int>(type: "int", nullable: true),
                username = table.Column<int>(type: "varchar(500)", maxLength: 500, nullable: true),
                pagename = table.Column<int>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                logintime = table.Column<string>(type: "datetime", nullable: true),
                logouttime = table.Column<string>(type: "datetime", nullable: true),
                IsLogin = table.Column<string>(type: "bit", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_pagelogs", x => x.id);
            });

            migrationBuilder.CreateTable(
              name: "batchlogs",
              columns: table => new
              {
                  Id = table.Column<int>(type: "int", nullable: false),
                  BatchId = table.Column<string>(type: "int", maxLength: 50, nullable: true),
                  Action = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                  Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                  TransactionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                  TransactionBy = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_batchlogs", x => x.Id);
              });

            migrationBuilder.CreateTable(
             name: "Batch_Approver",
             columns: table => new
             {
                 ID = table.Column<int>(type: "int", nullable: false),
                 BatchID = table.Column<string>(type: "int", nullable: true),
                 ApproverID = table.Column<string>(type: "int", nullable: true)
             },
             constraints: table =>
             {
                 table.PrimaryKey("PK_Batch_Approver", x => x.ID);
             });


        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArasLoginDetails");

            migrationBuilder.DropTable(
                name: "SearchLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
               name: "batch");

            migrationBuilder.DropTable(
              name: "batch_process");

            migrationBuilder.DropTable(
            name: "environment");

            migrationBuilder.DropTable(
            name: "batch_lines");

            migrationBuilder.DropTable(
             name: "batch_items");
        }
    }
}
