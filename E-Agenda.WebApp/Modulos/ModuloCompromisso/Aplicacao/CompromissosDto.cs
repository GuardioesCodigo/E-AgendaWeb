using System;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;
namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Aplicacao;

// DTO para a criação de um novo compromisso
public record CadastrarCompromissoDto(
    string Assunto, 
    DateTime Data, 
    TimeSpan HoraInicio, 
    TimeSpan HoraTermino, 
    TipoCompromisso Tipo, 
    string? Local
);

// DTO para a edição de um compromisso existente
public record EditarCompromissoDto(
    Guid Id, 
    string Assunto, 
    DateTime Data, 
    TimeSpan HoraInicio, 
    TimeSpan HoraTermino, 
    TipoCompromisso Tipo, 
    string? Local
);

// DTO de saída (opcional, mas recomendado se você quiser ocultar campos do domínio)
public record CompromissoDto(
    Guid Id, 
    string Assunto, 
    DateTime Data, 
    TimeSpan HoraInicio, 
    TimeSpan HoraTermino, 
    TipoCompromisso Tipo, 
    string? Local
);