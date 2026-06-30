CREATE TABLE [dbo].[TBTarefa] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Titulo]              NVARCHAR (100)   NOT NULL,
    [PrioridadeTarefa]    NVARCHAR (6)     NOT NULL,
    [DataCriacao]         DATETIME2 (0)    NOT NULL,
    [DataConclusao]       DATETIME2 (0)    NULL,
    [StatusConclusao]     NVARCHAR (50)    NOT NULL,
    [PercentualConcluido] INT              NOT NULL,
    [ItemTarefa]          UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE [dbo].[TBTarefa]
    ADD CONSTRAINT [PK_TBTarefa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

