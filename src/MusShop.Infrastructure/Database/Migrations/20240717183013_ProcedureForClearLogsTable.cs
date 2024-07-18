using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusShop.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureForClearLogsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            const string sql = @"
IF EXISTS (SELECT * FROM sysobjects 
    WHERE id = object_id(N'[dbo].[Infrastructure_TruncateLogsTable]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[Infrastructure_TruncateLogsTable]
END

GO
CREATE PROCEDURE [dbo].[Infrastructure_TruncateLogsTable] AS
    BEGIN
        TRUNCATE TABLE [dbo].[LogEventsData];
    END;
";
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
