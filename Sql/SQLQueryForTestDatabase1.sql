CREATE TABLE [dbo].[User] (
   [UserId]   INT           NOT NULL,
   [Name]     NVARCHAR (50) NOT NULL,
   [Password] NVARCHAR (50) NOT NULL
);

CREATE TABLE [dbo].[MarkingLog] (
   [MarkerId] INT           NOT NULL,
   [Name]     NVARCHAR (50) NOT NULL,
   [Color]    NCHAR (6)     NOT NULL
);

CREATE TABLE [dbo].[TextFilesList] (
   [FileId]   INT           NOT NULL,
   [FileName] NVARCHAR (50) NOT NULL,
   [UserId]   INT           NOT NULL,
   [Update]   DATETIME      NOT NULL,
   [Text]     NTEXT         NULL
);

CREATE TABLE [dbo].[Marker] (
   [MarkerId] INT        NOT NULL,
   [Name]     NCHAR (20) NOT NULL,
   [Color]    NCHAR (10) NOT NULL
);

CREATE TABLE [dbo].[CurrentSession] (
    [Id]       NTEXT      NOT NULL,
    [CreateAt] DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



