using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusShop.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitDataDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string sql = @"
CREATE TABLE [LogEventsData] (
   [Id] int IDENTITY(1,1) NOT NULL,
   [Message] nvarchar(max) NULL,
   [MessageTemplate] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetime NOT NULL,
   [Exception] nvarchar(max) NULL,
   [Properties] nvarchar(max) NULL

   CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE ROLE [SerilogAutoCreate];
GRANT SELECT ON sys.tables TO [SerilogAutoCreate];
GRANT SELECT ON sys.schemas TO [SerilogAutoCreate];
GRANT ALTER ON SCHEMA::[dbo] TO [SerilogAutoCreate]
GRANT CREATE TABLE ON DATABASE::[MusShop.Infrastructure] TO [SerilogAutoCreate];

CREATE ROLE [SerilogWriter];
GRANT SELECT TO [SerilogWriter];
GRANT INSERT TO [SerilogWriter];

CREATE LOGIN [Serilog] WITH PASSWORD = '123456a@';

CREATE USER [Serilog] FOR LOGIN [Serilog] WITH DEFAULT_SCHEMA = dbo;
GRANT CONNECT TO [Serilog];

ALTER ROLE [SerilogAutoCreate] ADD MEMBER [Serilog];
ALTER ROLE [SerilogWriter] ADD MEMBER [Serilog];

GRANT SELECT ON [dbo].[LogEventsData] TO [SerilogWriter];
GRANT INSERT ON [dbo].[LogEventsData] TO [SerilogWriter];
GO

CREATE LOGIN [HangFireUser] WITH PASSWORD = '123456a@';
CREATE USER [HangFireUser] FOR LOGIN [HangFireUser] WITH DEFAULT_SCHEMA = dbo;
EXEC sp_addrolemember 'db_owner', 'HangFireUser';
";

            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
