CREATE TABLE [dbo].[LikeUsuarioCategoria] (
    [IdLikeUserCategoria]    INT           IDENTITY (1, 1) NOT NULL,
    [Categoria]   VARCHAR (20)  NOT NULL,
    [Usuario]      VARCHAR (20)  NOT NULL,
    CONSTRAINT [PK_IdLikeUsuarioCategoria] PRIMARY KEY CLUSTERED ([IdLikeUserCategoria] ASC)
);