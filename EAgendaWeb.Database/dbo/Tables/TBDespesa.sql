CREATE TABLE [dbo].[TBDespesa] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Descricao]      NVARCHAR (100)   NOT NULL,
    [DataOcorrencia] DATETIME2 (0)    NOT NULL,
    [Valor]          DECIMAL (18)     NOT NULL,
    [FormaPagamento] NVARCHAR (7)     NOT NULL,
    [CategoriaId]    UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE [dbo].[TBDespesa]
    ADD CONSTRAINT [PK_TBDespesa] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

ALTER TABLE [dbo].[TBDespesa]
    ADD CONSTRAINT [FK_TBDespesas_TBCategoria] FOREIGN KEY ([CategoriaId]) REFERENCES [dbo].[TBCategoria] ([Id]);
GO

