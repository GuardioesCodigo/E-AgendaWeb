CREATE TABLE [dbo].[TBContatos] (
    [Id]       UNIQUEIDENTIFIER NOT NULL,
    [Nome]     NVARCHAR (100)   NOT NULL,
    [Email]    NVARCHAR (256)   NOT NULL,
    [Telefone] NVARCHAR (15)    NOT NULL,
    [Cargo]    NVARCHAR (100)   NULL,
    [Empresa]  NVARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

