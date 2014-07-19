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