DECLARE @TestCategoryId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[blog_categories] ([Id], [CategoryName], [CreatedDate], [UpdatedDate])
VALUES (@TestCategoryId, 'Hello', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO [dbo].[blog_posts] ([Id], [Title], [Description], [CreatedBy], [CategoryId], [CreatedDate], [UpdatedDate])
VALUES
    (NEWID(), 'title', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title2', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title3', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title4', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title5', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title6', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title7', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title8', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title9', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title10', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title11', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title12', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
    (NEWID(), 'title13', 'description', 'something', @TestCategoryId, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

/*
const string sqlSeedFileName = "SeedData.sql";
            string sqlFilePath = Path.Combine("/app/Seeds", sqlSeedFileName);
            string sqlFileText = File.ReadAllText(sqlFilePath);
            migrationBuilder.Sql(sqlFileText);
*/