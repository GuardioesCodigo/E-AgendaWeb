using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

public class Tarefa : EntidadeBase<Tarefa>
{
    public string Titulo {get; set;} = string.Empty;
    public PrioridadeTarefa PrioridadeTarefa {get; set;}
    public DateTime DataCriacao {get; set;}
    public DateTime DataConclusao {get; set;} = DateTime.Now;
    public bool StatusConclusao { get; set; }
    public decimal PercentualConcluido { get;set; }
    public List<ItensDeTarefas> Itens { get; set; } = new List<ItensDeTarefas>();


    public void CalcularPercentual()
    {
        if (Itens.Count == 0)
        {
            PercentualConcluido = 0;
            return;
        }

        int totalItens = Itens.Count;
        int itensConcluidos = Itens.Count(i => i.StatusConclusao);

        // Regra de 3: (Concluidos * 100) / Total
        PercentualConcluido = (int)(decimal)itensConcluidos * 100 / totalItens;
        
        // Se 100% concluído, muda status da tarefa
        if (PercentualConcluido == 100)
            StatusConclusao = true;
    }
    public Tarefa() { }

    public Tarefa(
        string titulo,
        PrioridadeTarefa prioridadeTarefa,
        DateTime dataConclusao,
        List<ItensDeTarefas>? itemTarefa = null
    )
    {
        Titulo = titulo;
        PrioridadeTarefa = prioridadeTarefa;
        DataCriacao = DateTime.Now;
        DataConclusao = dataConclusao;
        StatusConclusao = false;
        PercentualConcluido = 0;
        Itens = itemTarefa ?? new List<ItensDeTarefas>();
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
            erros.Add("O campo \"Título\" deve ser preenchido.");

        else if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" deve conter entre 2 e 100 caracteres.");

        if (DataCriacao == default)
            erros.Add("A \"Data de Criação\" deve ser preenchida.");

        if (DataConclusao == default)
            erros.Add("A \"Data de Conclusão\" deve ser preenchida.");

        else if (DataConclusao < DataCriacao)
            erros.Add("A \"Data de Conclusão\" não pode ser anterior à \"Data de Criação\".");

        if (PercentualConcluido < 0 || PercentualConcluido > 100)
            erros.Add("O \"Percentual Concluído\" deve estar entre 0 e 100.");

        if (StatusConclusao && PercentualConcluido != 100)
            erros.Add("Uma tarefa concluída deve ter o \"Percentual Concluído\" igual a 100.");

        return erros;
    }

    public override void Atualizar(Tarefa entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        PrioridadeTarefa = entidadeAtualizada.PrioridadeTarefa;
        DataConclusao = entidadeAtualizada.DataConclusao;
        StatusConclusao = entidadeAtualizada.StatusConclusao;
        PercentualConcluido = entidadeAtualizada.PercentualConcluido;
        Itens = entidadeAtualizada.Itens;
    }

    internal void AdicionarItem(ItensDeTarefas novoItem)
    {
        throw new NotImplementedException();
    }
}