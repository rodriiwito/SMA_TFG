
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Add Values ******/
UPDATE Meteoro
SET FechaHora = CAST(TRIM(CAST(Meteoro.Fecha as char)) + ' ' + Substring(Meteoro.Hora,1,12) as datetime);
GO

UPDATE Puntos_ZWO
SET FechaHora = CAST(TRIM(CAST(Puntos_ZWO.Fecha as char)) + ' ' + Substring(Puntos_ZWO.Hora,1,12) as datetime);
GO

INSERT INTO TiposInforme
VALUES  (0, 'TwoStations')
		,(1, 'OneStation')
		,(2,'Photometry')
GO
