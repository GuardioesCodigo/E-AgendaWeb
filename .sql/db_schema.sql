IF DB_ID('EAgendaWeb') IS NULL
BEGIN 
    CREATE DATABASE [EAgendaWeb];
END;

USE [EAgendaWeb]
GO

CREATE TABLE [dbo].[TBCategoria] (
[Id] uniqueidentifier NOT NULL,
[Titulo] nvarchar(100) NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBDespesa] (
[Id] uniqueidentifier NOT NULL,
[Descrição] nvarchar(100) NOT NULL,
[Data de ocorrência] datetime NOT NULL,
[Valor] decimal(18,0) NOT NULL,
[Forma de Pagamento] nvarchar(7) NOT NULL,
[CategoriaId] uniqueidentifier NOT NULL,
PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[TBTarefa] (
[Id] uniqueidentifier NOT NULL,
[Título] nvarchar(100) NOT NULL,
[Prioridade da Tarefa] nvarchar(6) NOT NULL,
[Data de Criação] datetime NOT NULL,
[Data de Conclusão] datetime,
[Status de Conclusão] nvarchar(50) NOT NULL,
[Percentual Concluído] int NOT NULL,
[Item de Tarefa] uniqueidentifier NOT NULL,
PRIMARY KEY ([Id])
);


ALTER TABLE [dbo].[TBDespesa]
ADD CONSTRAINT [FK_TBDespesas_TBCategoria]
FOREIGN KEY ([CategoriaId]) 
REFERENCES [dbo].[TBCategoria]([Id])
ON DELETE NO ACTION
ON UPDATE NO ACTION;



