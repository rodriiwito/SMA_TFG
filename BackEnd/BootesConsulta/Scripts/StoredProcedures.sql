
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Stored Procedures ******/

CREATE PROCEDURE [dbo].[Select_Observatorios]
	@Nombre			as VARCHAR(200)
	,@AlturaMinima		as int
	,@AlturaMaxima		as int
	
AS
BEGIN
	DECLARE @vAlturaMinima		as int			= 0;
	DECLARE @vAlturaMaxima		as int			= 10000;

	IF @AlturaMinima IS NOT NULL
		BEGIN
			SET @vAlturaMinima = @AlturaMinima
		END;
	
	IF @AlturaMaxima IS NOT NULL
		BEGIN
			SET @vAlturaMaxima = @AlturaMaxima
		END;
	SELECT 
	Ob.Número
	,Ob.Nombre_Observatorio
	,Ob.Longitud_Sexagesimal
	,Ob.Latitud_Sexagesimal
	,Ob.Altitud
	,Ob.Créditos
	From Observatorio Ob
	where ((Ob.Altitud is Null or Ob.Altitud = 0) or  (Ob.Altitud >= @vAlturaMinima and Ob.Altitud <= @vAlturaMaxima))
		and Ob.Nombre_Observatorio like CONCAT('%',@Nombre,'%')
END
GO

CREATE PROCEDURE [dbo].[Select_Meteoros]
	@MinimunDate		as datetime
	,@MaximumDate		as datetime
	,@ReportTypes				as [dbo].IntCollectionType READONLY
AS
BEGIN
	--Inicializamos la colección ya que estamos obligados a ponerla como readonly
	DECLARE @vReportTypes as dbo.IntCollectionType
	IF EXISTS(SELECT 1 FROM @ReportTypes)
		BEGIN
			INSERT INTO @vReportTypes
			SELECT [ID]
			FROM @ReportTypes
		END;
	ELSE 
		BEGIN
			INSERT INTO @vReportTypes
			SELECT [ID]
			FROM TiposInforme
		END;

	SELECT 
	Meteoro.Identificador as Id
	,Meteoro.FechaHora as Fecha
	,TwoStationsReports = (SELECT Count(*) FROM Informe_Z where Informe_Z.Meteoro_Identificador = Meteoro.Identificador)
	,OneStationReports	= (SELECT Count(*) FROM Informe_Radiante where Informe_Radiante.Meteoro_Identificador = Meteoro.Identificador)
	,PhotometryReports	= (SELECT Count(*) FROM Informe_Fotometria where Informe_Fotometria.Meteoro_Identificador = Meteoro.Identificador)
	From Meteoro
	where (@MinimunDate is null or Meteoro.FechaHora >= @MinimunDate)
		and (@MaximumDate is null or Meteoro.FechaHora <= @MaximumDate)
		and ((Exists(SELECT * FROM @vReportTypes WHERE ID = 0) and (EXISTS(SELECT * FROM Informe_Z where Informe_Z.Meteoro_Identificador = Meteoro.Identificador)))
			or (Exists(SELECT * FROM @vReportTypes WHERE ID = 1) and (EXISTS(SELECT * FROM Informe_Radiante where Informe_Radiante.Meteoro_Identificador = Meteoro.Identificador)))
			or (Exists(SELECT * FROM @vReportTypes WHERE ID = 2) and (EXISTS(SELECT * FROM Informe_Fotometria where Informe_Fotometria.Meteoro_Identificador = Meteoro.Identificador))))
	order by Meteoro.FechaHora desc
END
GO