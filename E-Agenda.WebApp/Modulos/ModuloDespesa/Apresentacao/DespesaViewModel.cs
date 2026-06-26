using System;
using System.ComponentModel.DataAnnotations;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

public record OpcaoCategoriaViewModel(
    Guid Id,
    string Titulo
);

public record CadastrarDespesaViewModel(
    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,
 
    DateTime? DataOcorrencia,
 
    [Required(ErrorMessage = "O campo \"Valor\" deve ser preenchido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
    decimal Valor,
 
    [Required(ErrorMessage = "O campo \"Forma de Pagamento\" deve ser preenchido.")]
    FormaPagamento FormaPagamento,
 
    [Required(ErrorMessage = "Selecione uma Categoria.")]
    Guid CategoriaId,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record EditarDespesaViewModel(
    Guid Id,
 
    [Required(ErrorMessage = "O campo \"Descrição\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Descrição\" deve conter entre 2 e 100 caracteres.")]
    string Descricao,
 
    DateTime? DataOcorrencia,
 
    [Required(ErrorMessage = "O campo \"Valor\" deve ser preenchido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O campo \"Valor\" deve ser maior que zero.")]
    decimal Valor,
 
    [Required(ErrorMessage = "O campo \"Forma de Pagamento\" deve ser preenchido.")]
    FormaPagamento FormaPagamento,
 
    [Required(ErrorMessage = "Selecione uma Categoria.")]
    Guid CategoriaId,

    [ValidateNever]
    List<OpcaoCategoriaViewModel> Categorias
);

public record ExcluirDespesaViewModel(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    string CategoriaTitulo
);

public record DetalhesDespesaViewModel(
    Guid Id,
    string Descricao,
    DateTime DataOcorrencia,
    decimal Valor,
    FormaPagamento FormaPagamento,
    string CategoriaTitulo
);
