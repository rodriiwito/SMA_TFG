SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE UserLogin (
    UserName        [varchar](200)	NOT NULL,
    UserPassword    [varchar](200)  NOT NULL,
    PRIMARY KEY (UserName)
);
GO

CREATE PROCEDURE [dbo].[Create_User]
	@UserName			as varchar(200)
	,@UserPassword		as varchar(200)
AS
BEGIN
	Insert into UserLogin
	Values(@UserName,@UserPassword)
END
GO

CREATE PROCEDURE [dbo].[Check_Credentials]
	@UserName			as varchar(200)
	,@UserPassword		as varchar(200)
	,@IsValid			as bit output
AS
BEGIN
	IF Exists(Select 1 FROM UserLogin where UserName = @UserName and UserPassword = @UserPassword)
      BEGIN
            SET @IsValid = 1
      END
      ELSE
      BEGIN
            SET @IsValid = 0
      END
	Select @IsValid
END

GO