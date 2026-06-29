CREATE TABLE [dbo].[TBCompromissos] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Assunto]     NVARCHAR (100)   NOT NULL,
    [Data]        DATETIME2 (0)    NOT NULL,
    [HoraInicio]  TIME (0)         NOT NULL,
    [HoraTermino] TIME (0)         NOT NULL,
    [Tipo]        NVARCHAR (10)    NOT NULL,
    [Local]       NVARCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

