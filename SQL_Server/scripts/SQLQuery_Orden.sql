CREATE TABLE [dbo].[Orden] (
    [IdOrden]     INT      IDENTITY (1, 1) NOT NULL,
	[IdCarrito]   INT      NOT NULL,
    [Fecha]       DATETIME NOT NULL,
	[FechaInicio]       DATETIME NOT NULL,
	[FechaFinal]       DATETIME NOT NULL,
    [Usuario]   INT      NOT NULL,
    [Tarifa] INT      NOT NULL,
	[Categoria] VARCHAR(20)      NULL,
	[Descripción] VARCHAR(500)      NOT NULL
    
    CONSTRAINT [PK_IdOrden] PRIMARY KEY CLUSTERED ([IdOrden] ASC)
);