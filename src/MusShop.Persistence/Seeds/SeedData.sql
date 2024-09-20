DECLARE @TestCategoryId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[blog_categories] ([Id], [CategoryName], [CreatedDate], [UpdatedDate])
VALUES (@TestCategoryId, 'Hello', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO [dbo].[blog_posts] ([Id], [Title], [Description], [CreatedBy], [CategoryId], [CreatedDate], [UpdatedDate])
VALUES (NEWID(), 'title', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

/*
const string sqlSeedFileName = "SeedData.sql";
            string sqlFilePath = Path.Combine("/app/Seeds", sqlSeedFileName);
            string sqlFileText = File.ReadAllText(sqlFilePath);
            migrationBuilder.Sql(sqlFileText);
*/