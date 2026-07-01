using System;
using System.Text.Json.Serialization;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;

public class ItensDeTarefas : EntidadeBase<ItensDeTarefas>
{
    public string Titulo { get; set; } = string.Empty;
    public bool StatusConclusao { get; set; } // Ajuste aqui: use Concluido ou StatusConclusao (padronize)
    
    [JsonIgnore]
    public Tarefa Tarefa { get; set; } = null!;
    public Guid ItensDeTarefaId { get; set; }

    public ItensDeTarefas() { }

    public ItensDeTarefas(string titulo)
    {
        Titulo = titulo;
        StatusConclusao = false;
    }

    // Métodos simples
    public void Concluir() => StatusConclusao = true;
    public void MarcarPendente() => StatusConclusao = false;

    public override List<string> Validar() 
    {
        // Sua validação de título aqui...
        return new List<string>(); 
    }

    public override void Atualizar(ItensDeTarefas entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        StatusConclusao = entidadeAtualizada.StatusConclusao;
    }
}