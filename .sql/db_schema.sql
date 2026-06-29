IF DB_ID('EAgendaWeb') IS NULL
BEGIN 
    CREATE DATABASE [EAgendaWeb];
END;

USE [EAgendaWeb]
GO

IF OBJECT_ID('dbo.TBCategoria', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBCategoria] (
        [Id] uniqueidentifier NOT NULL,
        [Titulo] nvarchar(100) NOT NULL,
        PRIMARY KEY ([Id])
    );
END;

IF OBJECT_ID('dbo.TBDespesa', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBDespesa] (
        [Id] uniqueidentifier NOT NULL,
        [Descricao] nvarchar(100) NOT NULL,
        [DataOcorrencia] datetime2(0) NOT NULL,
        [Valor] decimal(18,0) NOT NULL,
        [FormaPagamento] nvarchar(7) NOT NULL,
        [CategoriaId] uniqueidentifier NOT NULL,
        PRIMARY KEY ([Id])
    );
END;

IF OBJECT_ID('dbo.TBTarefa', 'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[TBTarefa] (
        [Id] uniqueidentifier NOT NULL,
        [Titulo] nvarchar(100) NOT NULL,
        [PrioridadeTarefa] nvarchar(6) NOT NULL,
        [DataCriacao] datetime2(0) NOT NULL,
        [DataConclusao] datetime2(0),
        [StatusConclusao] nvarchar(50) NOT NULL,
        [PercentualConcluido] int NOT NULL,
        [ItemTarefa] uniqueidentifier NOT NULL,
        PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_TBDespesas_TBCategoria'
)
BEGIN
    ALTER TABLE [dbo].[TBDespesa]
    ADD CONSTRAINT [FK_TBDespesas_TBCategoria]
    FOREIGN KEY ([CategoriaId]) 
    REFERENCES [dbo].[TBCategoria]([Id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION;
END;