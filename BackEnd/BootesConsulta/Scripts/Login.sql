SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user_login](
	[email] [varchar](200) NOT NULL,
	[password] [varchar](200) NOT NULL,
	[user_type] [tinyint] NOT NULL,
	[organization] [varchar](200) NULL,
	[country] [varchar](3) NOT NULL,
	[email_confirmed] [bit] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

Create PROCEDURE [dbo].[register_user]
	@Email			as varchar(200)
	,@Password		as varchar(200)
	,@UserType		as tinyint
	,@Organization	as varchar(200)
	,@Country		as varchar(3)
AS
BEGIN
	Insert into user_login
	Values(@Email,@Password,@UserType,@Organization,@Country,0)
END
GO

CREATE PROCEDURE [dbo].[check_credentials]
	@Email			as varchar(200)
	,@Password		as varchar(200)
	,@IsValid			as bit output
AS
BEGIN
	IF Exists(Select 1 FROM user_login where email = @Email and password = @Password)
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

CREATE PROCEDURE [dbo].[exists_user]
	@Email			as varchar(200)
AS
BEGIN
	Select email_confirmed
	From user_login
	where @Email = email
END
GO

CREATE PROCEDURE [dbo].[verify_user]
	@Email			as varchar(200)
AS
BEGIN
	Update user_login
	Set email_confirmed = 1
	where @Email = email
END
GO

CREATE PROCEDURE [dbo].[delete_user]
	@Email			as varchar(200)
AS
BEGIN
	Delete FROM user_login
	where @Email = email
END
GO

Create PROCEDURE [dbo].[update_user]
	@Email			as varchar(200)
	,@UserType		as tinyint
	,@Organization	as varchar(200)
	,@Country		as varchar(3)
AS
BEGIN
	update user_login
	set user_type = coalesce(@UserType, user_type), organization = coalesce(@Organization, organization), country = coalesce(@country, Country)
	where @Email = email
END
GO

Create PROCEDURE [dbo].[select_user]
	@Email			as varchar(200)
AS
BEGIN
	SELECT email
	,user_type
	,organization
	,country
	FROM user_login
END
GO