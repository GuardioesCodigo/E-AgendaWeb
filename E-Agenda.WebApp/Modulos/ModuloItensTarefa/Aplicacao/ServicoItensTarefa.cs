using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloItensTarefa.Aplicacao;

public class ServicoItensTarefa
{
    private readonly IRepositorioTarefa _repositorioTarefa;

    // 1. Você precisa injetar o repositório pelo construtor!
    public ServicoItensTarefa(IRepositorioTarefa repositorioTarefa)
    {
        _repositorioTarefa = repositorioTarefa;
    }

    public void AdicionarItem(Guid tarefaId, string titulo)
    {
        var tarefa = _repositorioTarefa.SelecionarPorId(tarefaId);
        
        // 2. Sempre bom verificar se a tarefa existe antes de manipular
        if (tarefa == null) return; 

        var novoItem = new ItensDeTarefas(titulo);
        
        tarefa.AdicionarItem(novoItem); // Recomendo usar um método na classe Tarefa
        _repositorioTarefa.Editar(tarefaId, tarefa); 
    }

    public void ConcluirItem(Guid tarefaId, Guid itemId)
    {
        var tarefa = _repositorioTarefa.SelecionarPorId(tarefaId);
        if (tarefa == null) return;

        var item = tarefa.Itens.FirstOrDefault(i => i.Id == itemId);
        
        if (item != null)
        {
            item.Concluir();
            _repositorioTarefa.Editar(tarefaId, tarefa); 
        }
    }
}