using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;

public class ItensTarefa : EntidadeBase<ItensTarefa>
{
    public string Titulo { get; set; } = string.Empty;
    public bool StatusConclusao { get; set; }
    public Tarefa Tarefa { get; set; } = null!;

    protected ItensTarefa() { }

    public ItensTarefa(string titulo, Tarefa tarefa)
    {
        Titulo = titulo;
        StatusConclusao = false;
        Tarefa = tarefa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
            erros.Add("O campo \"Título\" do item deve ser preenchido.");
        else if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" do item deve conter entre 2 e 100 caracteres.");

        return erros;
    }

    public override void Atualizar(ItensTarefa entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        StatusConclusao = entidadeAtualizada.StatusConclusao;
    }
}
