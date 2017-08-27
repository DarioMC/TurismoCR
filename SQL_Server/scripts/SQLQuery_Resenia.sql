USE [TurismoCR];
CREATE TABLE [dbo].[Resenia]
(
	[IdResenia] INT IDENTITY(1,1) NOT NULL, 
    [IdServicio] INT NOT NULL, 
    [IdCliente] INT NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
	[Calificacion] INT NOT NULL,
    [Comentario] VARCHAR(500) NULL
    
	CONSTRAINT [PK_IdResenia] PRIMARY KEY (IdResenia)
)