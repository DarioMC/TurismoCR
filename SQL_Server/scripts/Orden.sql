use TurismoCR

CREATE TABLE [dbo].[Orden] (
    [IdOrden]     INT      IDENTITY (1, 1) NOT NULL,
	[IdCarrito]   VARCHAR(20)      NOT NULL,
    [Fecha]       DATETIME NOT NULL,
    [Usuario]   VARCHAR(20)      NOT NULL,
    [Tarifa] INT      NOT NULL,
	[Categoria] VARCHAR(20)      NULL,
	[Descripción] VARCHAR(500)      NOT NULL
    
    CONSTRAINT [PK_IdOrden] PRIMARY KEY CLUSTERED ([IdOrden] ASC)
);

CREATE TABLE [dbo].[Resenia]
(
	[IdResenia] INT IDENTITY(1,1) NOT NULL, 
    [IdServicio] INT NOT NULL, 
    [Usuario] VARCHAR(20) NOT NULL, 
    [Fecha] DATETIME NOT NULL, 
	[Calificacion] INT NOT NULL,
    [Comentario] VARCHAR(500) NULL
    
	CONSTRAINT [PK_IdResenia] PRIMARY KEY (IdResenia)
);
