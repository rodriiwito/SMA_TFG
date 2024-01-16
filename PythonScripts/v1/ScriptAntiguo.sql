SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ecuacion_parametrica](
	[IdEc] [int] NOT NULL,
	[a] [decimal](21, 18) NULL,
	[b] [decimal](21, 18) NULL,
	[c] [decimal](21, 18) NULL,
	[Inicio_Estacion_1] [varchar](200) NULL,
	[Fin_Estacion_1] [varchar](200) NULL,
	[Inicio_Estacion_2] [varchar](200) NULL,
	[Fin_Estacion_2] [varchar](200) NULL,
 CONSTRAINT [Ecuacion_parametrica_PK] PRIMARY KEY CLUSTERED 
(
	[IdEc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Datos_meteoro_fotometria](
	[X_Inicio] [decimal](8, 3) NOT NULL,
	[Y_Inicio] [decimal](8, 3) NOT NULL,
	[Maire_Inicio] [decimal](6, 3) NOT NULL,
	[distInicio] [decimal](6, 2) NOT NULL,
	[X_Final] [decimal](8, 3) NOT NULL,
	[Y_Final] [decimal](8, 3) NOT NULL,
	[Maire_Final] [decimal](6, 3) NOT NULL,
	[dist_Final] [decimal](6, 2) NOT NULL,
	[Informe_Fotometria_Identificador] [int] NOT NULL,
 CONSTRAINT [Datos_meteoro_fotometria_PK] PRIMARY KEY CLUSTERED 
(
	[Informe_Fotometria_Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Datos_meteoro_fotometria]  WITH CHECK ADD  CONSTRAINT [Datos_meteoro_fotometria_Informe_Fotometria_FK] FOREIGN KEY([Informe_Fotometria_Identificador])
REFERENCES [dbo].[Informe_Fotometria] ([Identificador])
GO

ALTER TABLE [dbo].[Datos_meteoro_fotometria] CHECK CONSTRAINT [Datos_meteoro_fotometria_Informe_Fotometria_FK]
GO


CREATE TABLE [dbo].[Elementos_Orbitales](
	[Informe_Z_IdInforme] [int] NOT NULL,
	[Calculados_con] [varchar](200) NOT NULL,
	[Vel__Inf] [varchar](250) NULL,
	[Vel__Geo] [varchar](250) NULL,
	[Ar] [varchar](250) NULL,
	[De] [varchar](250) NULL,
	[i] [varchar](250) NULL,
	[p] [varchar](250) NULL,
	[a] [varchar](250) NULL,
	[e] [varchar](250) NULL,
	[q] [varchar](250) NULL,
	[T] [varchar](250) NULL,
	[omega] [varchar](250) NULL,
	[Omega_grados_votos_max_min] [varchar](250) NULL,
 CONSTRAINT [Elementos_Orbitales_PK] PRIMARY KEY CLUSTERED 
(
	[Informe_Z_IdInforme] ASC,
	[Calculados_con] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Elementos_Orbitales]  WITH CHECK ADD  CONSTRAINT [Elementos_Orbitales_Informe_Z_FK] FOREIGN KEY([Informe_Z_IdInforme])
REFERENCES [dbo].[Informe_Z] ([IdInforme])
GO

ALTER TABLE [dbo].[Elementos_Orbitales] CHECK CONSTRAINT [Elementos_Orbitales_Informe_Z_FK]
GO


CREATE TABLE [dbo].[Estrellas_usadas_para_regresión](
	[Identificador] [int] NOT NULL,
	[Id_estrella] [varchar](200) NULL,
	[Masa_de_aire] [decimal](6, 3) NULL,
	[Magnitud_de_catalogo] [decimal](5, 2) NULL,
	[Magnitud_instrumental] [decimal](5, 2) NULL,
	[Informe_Fotometria_Identificador] [int] NOT NULL,
 CONSTRAINT [Estrellas_usadas_para_regresión_PK] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Estrellas_usadas_para_regresión]  WITH CHECK ADD  CONSTRAINT [Estrellas_usadas_para_regresión_Informe_Fotometria_FK] FOREIGN KEY([Informe_Fotometria_Identificador])
REFERENCES [dbo].[Informe_Fotometria] ([Identificador])
GO

ALTER TABLE [dbo].[Estrellas_usadas_para_regresión] CHECK CONSTRAINT [Estrellas_usadas_para_regresión_Informe_Fotometria_FK]
GO

--
CREATE TABLE [dbo].[Informe_Fotometria](
	[Identificador] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [varchar](200) NOT NULL,
	[Estrellas_visibles] [int] NOT NULL,
	[Estrellas_usadas_para_regresion] [int] NOT NULL,
	[Coeficiente_externo_Recta_de_Bouger] [decimal](18, 15) NULL,
	[Punto_cero_Recta_de_Bouger] [decimal](18, 15) NULL,
	[Error_tipico_regresion] [decimal](18, 15) NULL,
	[Error_tipico_punto_cero] [decimal](18, 15) NULL,
	[Error_tipico_coeficiente_externo] [decimal](18, 15) NULL,
	[Coeficientes_parabola_trayectoria] [varchar](200) NULL,
	[MagMax] [decimal](21, 18) NULL,
	[MagMin] [decimal](21, 18) NULL,
	[Masa_fotometrica] [decimal](6, 3) NULL,
	[Meteoro_Identificador] [int] NOT NULL,
 CONSTRAINT [Informe_Fotometria_PK] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Informe_Fotometria]  WITH CHECK ADD  CONSTRAINT [Informe_Fotometria_Meteoro_FK] FOREIGN KEY([Meteoro_Identificador])
REFERENCES [dbo].[Meteoro] ([Identificador])
GO

ALTER TABLE [dbo].[Informe_Fotometria] CHECK CONSTRAINT [Informe_Fotometria_Meteoro_FK]
GO

CREATE TABLE [dbo].[Informe_Radiante](
	[Identificador] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [varchar](200) NOT NULL,
	[Velocidad_Lluvia_Asociada] [int] NULL,
	[Trayectorias_estimadas_para] [varchar](200) NULL,
	[Distancia_angular_radianes] [decimal](10, 6) NULL,
	[Distancia_angular_grados] [decimal](10, 6) NULL,
	[Velocidad_angular_grad_sec] [decimal](7, 3) NULL,
	[Meteoro_Identificador] [int] NOT NULL,
	[Observatorio_Número] [int] NULL,
	[Lluvia_Asociada] [varchar](200) NULL,
 CONSTRAINT [Informe_Radiante_PK] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Informe_Radiante]  WITH CHECK ADD  CONSTRAINT [Informe_Radiante_Meteoro_FK] FOREIGN KEY([Meteoro_Identificador])
REFERENCES [dbo].[Meteoro] ([Identificador])
GO

ALTER TABLE [dbo].[Informe_Radiante] CHECK CONSTRAINT [Informe_Radiante_Meteoro_FK]
GO

ALTER TABLE [dbo].[Informe_Radiante]  WITH CHECK ADD  CONSTRAINT [Informe_Radiante_Observatorio_FK] FOREIGN KEY([Observatorio_Número])
REFERENCES [dbo].[Observatorio] ([Número])
GO

ALTER TABLE [dbo].[Informe_Radiante] CHECK CONSTRAINT [Informe_Radiante_Observatorio_FK]
GO


CREATE TABLE [dbo].[Informe_Z](
	[IdInforme] [int] NOT NULL,
	[Observatorio_Número2] [int] NOT NULL,
	[Observatorio_Número] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [varchar](200) NOT NULL,
	[Error_cuadrático_de_ortogonalidad_en_la_esfera_celeste_1] [varchar](200) NULL,
	[Error_cuadrático_de_ortogonalidad_en_la_esfera_celeste_2] [varchar](200) NULL,
	[Fotogramas_usados] [int] NULL,
	[Ajuste_estación_2_Inicio] [varchar](200) NULL,
	[Ajuste_estación_2_Final] [varchar](200) NULL,
	[Ángulo_diedro_entre_planos_trayectoria] [decimal](21, 17) NULL,
	[Peso_estadístico] [decimal](23, 20) NULL,
	[Errores_AR_DE_radiante] [varchar](200) NULL,
	[Coordenadas_astronómicas_del_radiante_Eclíptica_de_la_fecha] [varchar](200) NULL,
	[Coordenadas_astronómicas_del_radiante_J200] [varchar](200) NULL,
	[Azimut] [decimal](15, 9) NULL,
	[Dist_Cenital] [decimal](15, 9) NULL,
	[Inicio_de_la_trayectoria_Estacion_1] [varchar](200) NULL,
	[Fin_de_la_trayectoria_Estacion_1] [varchar](200) NULL,
	[Inicio_de_la_trayectoria_Estacion_2] [varchar](200) NULL,
	[Fin_de_la_trayectoria_Estacion_2] [varchar](200) NULL,
	[Impacto_previsible] [varchar](200) NULL,
	[Distancia_recorrida_Estacion_1] [decimal](23, 18) NULL,
	[Error_distancia_Estacion_1] [decimal](21, 18) NULL,
	[Error_alturas_Estacion_1] [decimal](21, 18) NULL,
	[Distancia_recorrida_Estacion_2] [decimal](23, 18) NULL,
	[Error_distancia_Estacion_2] [decimal](21, 18) NULL,
	[Error_alturas_Estacion_2] [decimal](21, 18) NULL,
	[Tiempo_Estacion_1] [decimal](12, 9) NULL,
	[Velocidad_media] [decimal](16, 9) NULL,
	[Tiempo_trayectoria_en_estacion_2] [decimal](12, 9) NULL,
	[Ecuacion_del_movimiento_en_Kms] [varchar](200) NULL,
	[Ecuacion_del_movimiento_en_gs] [varchar](200) NULL,
	[Error_Velocidad] [decimal](18, 15) NULL,
	[Velocidad_Inicial_Estacion_2] [decimal](21, 15) NULL,
	[Aceleración_en_Kms] [decimal](18, 9) NULL,
	[Aceleración_en_gs] [decimal](18, 9) NULL,
	[Método_utilizado] [int] NULL,
	[Ruta_del_informe] [varchar](200) NOT NULL,
	[Ecuacion_parametrica_IdEc] [int] NOT NULL,
	[Meteoro_Identificador] [int] NOT NULL,
 CONSTRAINT [Informe_Z_PK] PRIMARY KEY CLUSTERED 
(
	[IdInforme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Informe_Z]  WITH CHECK ADD  CONSTRAINT [Informe_Z_Ecuacion_parametrica_FK] FOREIGN KEY([Ecuacion_parametrica_IdEc])
REFERENCES [dbo].[Ecuacion_parametrica] ([IdEc])
GO

ALTER TABLE [dbo].[Informe_Z] CHECK CONSTRAINT [Informe_Z_Ecuacion_parametrica_FK]
GO

ALTER TABLE [dbo].[Informe_Z]  WITH CHECK ADD  CONSTRAINT [Informe_Z_Meteoro_FK] FOREIGN KEY([Meteoro_Identificador])
REFERENCES [dbo].[Meteoro] ([Identificador])
GO

ALTER TABLE [dbo].[Informe_Z] CHECK CONSTRAINT [Informe_Z_Meteoro_FK]
GO

ALTER TABLE [dbo].[Informe_Z]  WITH CHECK ADD  CONSTRAINT [Informe_Z_Observatorio_FK] FOREIGN KEY([Observatorio_Número])
REFERENCES [dbo].[Observatorio] ([Número])
GO

ALTER TABLE [dbo].[Informe_Z] CHECK CONSTRAINT [Informe_Z_Observatorio_FK]
GO

ALTER TABLE [dbo].[Informe_Z]  WITH CHECK ADD  CONSTRAINT [Informe_Z_Observatorio_FKv2] FOREIGN KEY([Observatorio_Número2])
REFERENCES [dbo].[Observatorio] ([Número])
GO

ALTER TABLE [dbo].[Informe_Z] CHECK CONSTRAINT [Informe_Z_Observatorio_FKv2]
GO

CREATE TABLE [dbo].[Lluvia](
	[Identificador] [varchar](200) NOT NULL,
	[Año] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Fecha_Inicio] [date] NULL,
	[Fecha_Fin] [date] NULL,
	[Velocidad] [int] NULL,
 CONSTRAINT [Lluvia_PK] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC,
	[Año] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Lluvia_activa](
	[Distancia_mínima_entre_radianes_y_trayectoria] [varchar](200) NOT NULL,
	[Lluvia_Identificador] [varchar](200) NOT NULL,
	[Lluvia_Año] [int] NOT NULL,
	[Informe_Z_IdInforme] [int] NOT NULL,
 CONSTRAINT [Lluvia_activa_PK] PRIMARY KEY CLUSTERED 
(
	[Lluvia_Identificador] ASC,
	[Informe_Z_IdInforme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lluvia_activa]  WITH CHECK ADD  CONSTRAINT [Lluvia_activa_Informe_Z_FK] FOREIGN KEY([Informe_Z_IdInforme])
REFERENCES [dbo].[Informe_Z] ([IdInforme])
GO

ALTER TABLE [dbo].[Lluvia_activa] CHECK CONSTRAINT [Lluvia_activa_Informe_Z_FK]
GO

ALTER TABLE [dbo].[Lluvia_activa]  WITH CHECK ADD  CONSTRAINT [Lluvia_activa_Lluvia_FK] FOREIGN KEY([Lluvia_Identificador], [Lluvia_Año])
REFERENCES [dbo].[Lluvia] ([Identificador], [Año])
GO

ALTER TABLE [dbo].[Lluvia_activa] CHECK CONSTRAINT [Lluvia_activa_Lluvia_FK]
GO


CREATE TABLE [dbo].[Lluvia_Activa_InfRad](
	[Ar_de_la_fecha] [decimal](7, 2) NOT NULL,
	[De_de_la_fecha] [decimal](5, 2) NOT NULL,
	[Ar_más_cercano] [decimal](7, 2) NOT NULL,
	[De_más_cercano] [decimal](5, 2) NOT NULL,
	[Distancia] [decimal](8, 3) NOT NULL,
	[Informe_Radiante_Identificador] [int] NOT NULL,
	[Lluvia_Identificador] [varchar](200) NOT NULL,
	[Lluvia_Año] [int] NOT NULL,
 CONSTRAINT [Lluvia_Activa_InfRad_PK] PRIMARY KEY CLUSTERED 
(
	[Informe_Radiante_Identificador] ASC,
	[Lluvia_Identificador] ASC,
	[Lluvia_Año] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Lluvia_Activa_InfRad]  WITH CHECK ADD  CONSTRAINT [Lluvia_Activa_InfRad_Informe_Radiante_FK] FOREIGN KEY([Informe_Radiante_Identificador])
REFERENCES [dbo].[Informe_Radiante] ([Identificador])
GO

ALTER TABLE [dbo].[Lluvia_Activa_InfRad] CHECK CONSTRAINT [Lluvia_Activa_InfRad_Informe_Radiante_FK]
GO

ALTER TABLE [dbo].[Lluvia_Activa_InfRad]  WITH CHECK ADD  CONSTRAINT [Lluvia_Activa_InfRad_Lluvia_FK] FOREIGN KEY([Lluvia_Identificador], [Lluvia_Año])
REFERENCES [dbo].[Lluvia] ([Identificador], [Año])
GO

ALTER TABLE [dbo].[Lluvia_Activa_InfRad] CHECK CONSTRAINT [Lluvia_Activa_InfRad_Lluvia_FK]
GO


CREATE TABLE [dbo].[Meteoro](
	[Identificador] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Hora] [varchar](200) NOT NULL,
 CONSTRAINT [Meteoro_PK] PRIMARY KEY CLUSTERED 
(
	[Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Puntos_del_ajuste](
	[t] [decimal](6, 4) NOT NULL,
	[Dist] [decimal](6, 2) NOT NULL,
	[Mc] [decimal](4, 2) NOT NULL,
	[Ma] [decimal](4, 2) NOT NULL,
	[Informe_Fotometria_Identificador] [int] NOT NULL,
 CONSTRAINT [Puntos_del_ajuste_PK] PRIMARY KEY CLUSTERED 
(
	[t] ASC,
	[Informe_Fotometria_Identificador] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Puntos_del_ajuste]  WITH CHECK ADD  CONSTRAINT [Puntos_del_ajuste_Informe_Fotometria_FK] FOREIGN KEY([Informe_Fotometria_Identificador])
REFERENCES [dbo].[Informe_Fotometria] ([Identificador])
GO

ALTER TABLE [dbo].[Puntos_del_ajuste] CHECK CONSTRAINT [Puntos_del_ajuste_Informe_Fotometria_FK]
GO

CREATE TABLE [dbo].[Seccion](
	[Fecha] [date] NOT NULL,
	[Identificador] [int] NOT NULL,
	[Ascensión_recta_del_radiante] [int] NOT NULL,
	[Declinación_del_radiante] [int] NOT NULL,
	[Lluvia_Identificador] [varchar](200) NOT NULL,
	[Lluvia_Año] [int] NOT NULL,
 CONSTRAINT [Seccion_PK] PRIMARY KEY CLUSTERED 
(
	[Lluvia_Año] ASC,
	[Lluvia_Identificador] ASC,
	[Fecha] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Seccion]  WITH CHECK ADD  CONSTRAINT [Seccion_Lluvia_FK] FOREIGN KEY([Lluvia_Identificador], [Lluvia_Año])
REFERENCES [dbo].[Lluvia] ([Identificador], [Año])
GO

ALTER TABLE [dbo].[Seccion] CHECK CONSTRAINT [Seccion_Lluvia_FK]
GO

CREATE TABLE [dbo].[Trayectoria_estimada](
	[Velocidad] [decimal](5, 2) NULL,
	[Lon_Inicio] [varchar](200) NOT NULL,
	[Lat_Inicio] [varchar](200) NOT NULL,
	[Alt_Inicio] [decimal](6, 2) NOT NULL,
	[Dist_Inicio] [decimal](6, 2) NOT NULL,
	[Lon_Final] [varchar](200) NOT NULL,
	[Lat_Final] [varchar](200) NOT NULL,
	[Alt_Final] [decimal](6, 2) NOT NULL,
	[Dist_Final] [decimal](6, 2) NOT NULL,
	[Recor] [decimal](6, 2) NULL,
	[e] [decimal](6, 2) NULL,
	[t] [decimal](7, 3) NULL,
	[Informe_Radiante_Identificador] [int] NOT NULL,
 CONSTRAINT [Trayectoria_estimada_PK] PRIMARY KEY CLUSTERED 
(
	[Informe_Radiante_Identificador] ASC,
	[Lon_Inicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trayectoria_estimada]  WITH CHECK ADD  CONSTRAINT [Trayectoria_estimada_Informe_Radiante_FK] FOREIGN KEY([Informe_Radiante_Identificador])
REFERENCES [dbo].[Informe_Radiante] ([Identificador])
GO

ALTER TABLE [dbo].[Trayectoria_estimada] CHECK CONSTRAINT [Trayectoria_estimada_Informe_Radiante_FK]
GO


CREATE TABLE [dbo].[Trayectoria_medida](
	[Fecha] [date] NOT NULL,
	[Hora] [varchar](200) NOT NULL,
	[s] [decimal](12, 4) NOT NULL,
	[t] [decimal](9, 6) NULL,
	[v] [decimal](12, 4) NULL,
	[lambda] [varchar](200) NOT NULL,
	[phi] [varchar](200) NOT NULL,
	[AR_Estacion_1] [varchar](200) NOT NULL,
	[De_Estacion_1] [varchar](200) NOT NULL,
	[Ar_Estacion_2] [varchar](200) NOT NULL,
	[De_Estacion_2] [varchar](200) NOT NULL,
	[X] [decimal](9, 4) NOT NULL,
	[Y] [decimal](9, 4) NOT NULL,
	[Pix] [decimal](9, 4) NULL,
	[Pix_Seg] [decimal](9, 4) NULL,
	[Informe_Z_IdInforme] [int] NOT NULL,
 CONSTRAINT [Trayectoria_medida_PK] PRIMARY KEY CLUSTERED 
(
	[Hora] ASC,
	[Informe_Z_IdInforme] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Trayectoria_medida]  WITH CHECK ADD  CONSTRAINT [Trayectoria_medida_Informe_Z_FK] FOREIGN KEY([Informe_Z_IdInforme])
REFERENCES [dbo].[Informe_Z] ([IdInforme])
GO

ALTER TABLE [dbo].[Trayectoria_medida] CHECK CONSTRAINT [Trayectoria_medida_Informe_Z_FK]
GO