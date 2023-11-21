SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Create Tables ******/
Create Table TiposInforme(
	[ID]			[tinyint]	NOT NULL,
	[Descripción]	varchar(20)	NOT NULL,
		CONSTRAINT PK_Informe PRIMARY KEY (ID)
)
GO

	--Utility
CREATE TYPE [dbo].[IntCollectionType] AS TABLE(
	[ID] [int] NULL
)
GO

/****** Add Fields ******/
ALTER TABLE Meteoro
ADD FechaHora DateTime;
GO

ALTER TABLE Puntos_ZWO
ADD FechaHora DateTime;
GO