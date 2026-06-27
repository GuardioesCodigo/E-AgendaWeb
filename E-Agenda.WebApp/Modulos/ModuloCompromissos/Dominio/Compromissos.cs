using System;
using System.Collections.Generic;
using E_Agenda.WebApp.Compartilhado.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;

public class Contatos : EntidadeBase<Contatos>
{
    // Propriedades (Fora do construtor)
    public string Assunto { get; set; } = string.Empty;
    public DateTime Data { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraTermino { get; set; }
    public TipoCompromisso Tipo { get; set; }
    public string? Local { get; set; }

    // Construtor padrão necessário para frameworks (ex: EF ou Serialização)
    public Contatos() { }

    // Construtor para novas instâncias
    public Contatos(string assunto, DateTime data, TimeSpan horaInicio, TimeSpan horaTermino, TipoCompromisso tipo, string? local)
    {
        Assunto = assunto;
        Data = data;
        HoraInicio = horaInicio;
        HoraTermino = horaTermino;
        Tipo = tipo;
        Local = local;
    }

    public override void Atualizar(Contatos entidadeAtualizada)
    {
        Assunto = entidadeAtualizada.Assunto;
        Data = entidadeAtualizada.Data;
        HoraInicio = entidadeAtualizada.HoraInicio;
        HoraTermino = entidadeAtualizada.HoraTermino;
        Tipo = entidadeAtualizada.Tipo;
        Local = entidadeAtualizada.Local;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        // Validação de Assunto
        if (string.IsNullOrWhiteSpace(Assunto))
            erros.Add("O campo \"Assunto\" deve ser preenchido.");
        else if (Assunto.Length < 2 || Assunto.Length > 100)
            erros.Add("O campo \"Assunto\" deve conter entre 2 e 100 caracteres.");

        // Validação de Data
        if (Data == default)
            erros.Add("A \"Data de Ocorrência\" deve ser informada.");

        // Validação de Local (Obrigatório apenas se for Presencial)
        if (Tipo == TipoCompromisso.Presencial && string.IsNullOrWhiteSpace(Local))
            erros.Add("O \"Local\" deve ser preenchido para compromissos presenciais.");

        // Validação de Horários (Regra básica: Inicio antes de Término)
        if (HoraInicio >= HoraTermino)
            erros.Add("A \"Hora de Início\" deve ser anterior à \"Hora de Término\".");

        return erros;
    }
}

// O Enum pode ficar aqui dentro ou em um arquivo separado
public enum TipoCompromisso
{
    Remoto = 0,
    Presencial = 1
}