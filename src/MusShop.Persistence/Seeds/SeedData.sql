DECLARE @TestCategoryId UNIQUEIDENTIFIER = NEWID();

INSERT INTO [Categories] ([Id], [CategoryName]) VALUES (@TestCategoryId, 'Hello');