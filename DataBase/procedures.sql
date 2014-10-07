----------------------------------------------------------------------------------------------------
------- Get email of user by id
----------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spUserGetEmail') AND type IN (N'P', N'PC'))
EXEC('CREATE PROCEDURE dbo.spUserGetEmail AS')
GO
ALTER PROCEDURE spUserGetEmail
(
	@id		int
)
AS
BEGIN
	SELECT Email FROM dbo.UserData WHERE Id = @id
END
GO

----------------------------------------------------------------------------------------------------
------- Get email of user by id
----------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spUserIsEmailExist') AND type IN (N'P', N'PC'))
EXEC('CREATE PROCEDURE dbo.spUserIsEmailExist AS')
GO
ALTER PROCEDURE spUserIsEmailExist
(
	@email	nvarchar(100)
)
AS
BEGIN
	If Exists (Select * from dbo.UserData Where Email = @email)
		Return 1;
	ELSE
		Return 0
END
GO

----------------------------------------------------------------------------------------------------
------- Send message to specified user
----------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spMessageInsert') AND type IN (N'P', N'PC'))
EXEC('CREATE PROCEDURE dbo.spMessageInsert AS')
GO
ALTER PROCEDURE dbo.spMessageInsert
(
	@userFromId		int=NULL,
	@userToId		int,
	@title			nvarchar(100),
	@messageType	int,
	@message		nvarchar(MAX),
	@isReaded		bit
)
AS
BEGIN
	INSERT INTO dbo.MessageData(
	UserFromId,
	UserToId,
	Title,
	MessageType,
	Message,
	IsReaded
	) VALUES(
	@userFromId,
	@userToId,
	@title,
	@messageType,
	@message,
	@isReaded
	)

	UPDATE dbo.UserData SET
		MessageCount = MessageCount + 1
	WHERE Id = @userToId
END
GO

----------------------------------------------------------------------------------------------------
------- Send message to specified user
----------------------------------------------------------------------------------------------------
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.spMessageGetListBySettings') AND type IN (N'P', N'PC'))
EXEC('CREATE PROCEDURE dbo.spMessageGetListBySettings AS')
GO
ALTER PROCEDURE dbo.spMessageGetListBySettings
(
	@userId				int,
	@indexFrom			bigint,
	@count				bigint,
	@isNewContent		bit,
	@isNotification		bit,
	@isUserMail			bit
)
AS
BEGIN
WITH OrderedOrders AS
(
    SELECT *,
    ROW_NUMBER() OVER (ORDER BY Id DESC) AS 'RowNumber'
    FROM dbo.MessageData
	WHERE UserToId = @userId AND (
		(@isNewContent = 1 AND MessageType = 0) OR
		(@isNotification = 1 AND MessageType = 1) OR
		(@isUserMail = 1 AND MessageType = 2))
) 
SELECT * 
FROM OrderedOrders 
WHERE RowNumber BETWEEN @indexFrom AND @indexFrom + @count - 1;
END
GO