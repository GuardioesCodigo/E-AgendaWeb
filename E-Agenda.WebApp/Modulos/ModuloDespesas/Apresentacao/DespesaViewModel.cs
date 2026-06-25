using System;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Apresentacao;

public record ListarDespesasViewModel(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    Guid CategoriaId,
    string CategoriaTitulo
);
