-------------------------------------------------------------------
-------- Users
-------------------------------------------------------------------
CREATE TABLE [dbo].[UserData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[AvatarName] [nvarchar](100) NULL,
	[Gender] [int] NOT NULL,
	[Rating] [int] NOT NULL,
	[MessageCount] [int] NOT NULL,
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

ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_Gender]  DEFAULT ((1)) FOR [Gender]
GO

ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_Rating]  DEFAULT ((0)) FOR [Rating]
GO

ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_MessageCount]  DEFAULT ((0)) FOR [MessageCount]
GO

ALTER TABLE [dbo].[UserData] ADD  CONSTRAINT [DF_UserData_AvatarName]  DEFAULT (N'default.png') FOR [AvatarName]
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
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Source] [nvarchar](max) NOT NULL,
	[PostType] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[AuthorName] [nvarchar](100) NOT NULL,
	[Rating] [int] NOT NULL
 CONSTRAINT [PK_PostData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[PostData] ADD  CONSTRAINT [DF_PostData_Rating]  DEFAULT ((0)) FOR [Rating]
GO

ALTER TABLE [dbo].[PostData]  WITH CHECK ADD  CONSTRAINT [FK_PostData_UserData] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[UserData] ([Id])
GO

ALTER TABLE [dbo].[PostData] CHECK CONSTRAINT [FK_PostData_UserData]
GO

-------------------------------------------------------------------
-------- Messages
-------------------------------------------------------------------
CREATE TABLE [dbo].[MessageData](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserFromId] [int] NULL,
	[UserToId] [int] NOT NULL,
	[MessageType] [int] NOT NULL,
	[Message] [nvarchar](MAX) NOT NULL,
	[IsReaded] [bit] NOT NULL,
 CONSTRAINT [PK_MessageData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MessageData]  WITH CHECK ADD  CONSTRAINT [FK_MessageData_UserData] FOREIGN KEY([UserToId])
REFERENCES [dbo].[UserData] ([Id])
GO

ALTER TABLE [dbo].[MessageData] CHECK CONSTRAINT [FK_MessageData_UserData]
GO

ALTER TABLE [dbo].[MessageData]  WITH CHECK ADD  CONSTRAINT [FK_MessageData_UserData1] FOREIGN KEY([UserFromId])
REFERENCES [dbo].[UserData] ([Id])
GO