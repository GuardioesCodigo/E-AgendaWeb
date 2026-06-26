using System;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Aplicacao;

public record ListarDespesaDto(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    Guid CategoriaId,
    string CategoriaTitulo
);

public record CadastrarDespesaDto(
    string Descricao,
    DateTime? DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    Guid CategoriaId
);

public record EditarDespesaDto(
    Guid Id,
    string Descricao,
    DateTime? DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    Guid CategoriaId
);

public record OpcaoCategoriaDto(
    Guid Id,
    string Titulo
);

public record DetalhesDespesaDto(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    string CategoriaTitulo
);