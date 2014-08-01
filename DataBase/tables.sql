-------------------------------------------------------------------
-------- Users
-------------------------------------------------------------------
CREATE TABLE [dbo].[UserData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[AvatarPath] [nvarchar](100) NULL,
	[Gender] [int] NULL,
	[Rating] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-------------------------------------------------------------------
-------- Header images
-------------------------------------------------------------------
CREATE TABLE [dbo].[HeaderImageData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
	[ShiftByX] [int] NOT NULL,
	[ShiftByY] [int] NOT NULL,
 CONSTRAINT [PK_HeaderImageData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-------------------------------------------------------------------
-------- Posts
-------------------------------------------------------------------
CREATE TABLE [dbo].[PostData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Source] [nvarchar](max) NOT NULL,
	[PostType] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
 CONSTRAINT [PK_PostData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PostData]  WITH CHECK ADD  CONSTRAINT [FK_PostData_UserData] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[UserData] ([Id])
GO

ALTER TABLE [dbo].[PostData] CHECK CONSTRAINT [FK_PostData_UserData]
GO