using Dapper;
using E_Agenda.WebApp.Modulos.ModuloItensTarefa.Dominio;
using E_Agenda.WebApp.Modulos.ModuloTarefa.Dominio;
using Microsoft.Data.SqlClient;

public sealed class RepositorioTarefaEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioTarefa
{
    private const string InserirTarefaSql = """
        INSERT INTO dbo.TBTarefa (Id, Titulo, PrioridadeTarefa, DataCriacao, DataConclusao, StatusConclusao, PercentualConcluido)
        VALUES (@Id, @Titulo, @PrioridadeTarefa, @DataCriacao, @DataConclusao, @StatusConclusao, @PercentualConcluido);
    """;

    private const string InserirItemTarefaSql = """
        INSERT INTO dbo.TBItemTarefa (Id, Titulo, StatusConclusao, TarefaId)
        VALUES (@Id, @Titulo, @StatusConclusao, @TarefaId);
    """;

    private const string ExcluirItensTarefaSql = """
        DELETE FROM dbo.TBItemTarefa
        WHERE TarefaId = @TarefaId;
    """;

    private const string AtualizarTarefaSql = """
        UPDATE dbo.TBTarefa
        SET Titulo = @Titulo,
            PrioridadeTarefa = @PrioridadeTarefa,
            DataCriacao = @DataCriacao,
            DataConclusao = @DataConclusao,
            StatusConclusao = @StatusConclusao,
            PercentualConcluido = @PercentualConcluido
        WHERE Id = @Id;
    """;

    private const string ExcluirTarefaSql = """
        DELETE FROM dbo.TBTarefa
        WHERE Id = @Id;
    """;

    private const string SelecionarTodasTarefasSql = """
        SELECT
            Id,
            Titulo,
            PrioridadeTarefa,
            DataCriacao,
            DataConclusao,
            StatusConclusao,
            PercentualConcluido
        FROM dbo.TBTarefa
        ORDER BY DataCriacao DESC;
    """;

    private const string SelecionarTarefaPorIdComItensSql = """
        SELECT
            t.Id            AS Id,
            t.Titulo        AS Titulo,
            t.PrioridadeTarefa AS PrioridadeTarefa,
            t.DataCriacao   AS DataCriacao,
            t.DataConclusao AS DataConclusao,
            t.StatusConclusao AS StatusConclusao,
            t.PercentualConcluido AS PercentualConcluido,
            i.Id            AS ItemId,
            i.Titulo        AS ItemTitulo,
            i.StatusConclusao AS ItemStatusConclusao
        FROM dbo.TBTarefa t
        LEFT JOIN dbo.TBItemTarefa i ON i.TarefaId = t.Id
        WHERE t.Id = @Id;
    """;

    public void Cadastrar(Tarefa entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(
            InserirTarefaSql,
            new
            {
                Id = entidade.Id,
                Titulo = entidade.Titulo,
                PrioridadeTarefa = entidade.PrioridadeTarefa,
                DataCriacao = entidade.DataCriacao,
                DataConclusao = entidade.DataConclusao,
                StatusConclusao = entidade.StatusConclusao,
                PercentualConcluido = entidade.PercentualConcluido
            }
        );

        InserirItens(conexao, entidade.Id, entidade.ItemTarefa);
    }

    public bool Editar(Guid idSelecionado, Tarefa entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        bool atualizou = conexao.Execute(
            AtualizarTarefaSql,
            new
            {
                Id = entidadeAtualizada.Id,
                Titulo = entidadeAtualizada.Titulo,
                PrioridadeTarefa = entidadeAtualizada.PrioridadeTarefa,
                DataCriacao = entidadeAtualizada.DataCriacao,
                DataConclusao = entidadeAtualizada.DataConclusao,
                StatusConclusao = entidadeAtualizada.StatusConclusao,
                PercentualConcluido = entidadeAtualizada.PercentualConcluido
            }
        ) == 1;

        conexao.Execute(ExcluirItensTarefaSql, new { TarefaId = idSelecionado });

        InserirItens(conexao, idSelecionado, entidadeAtualizada.ItemTarefa);

        return atualizou;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(ExcluirItensTarefaSql, new { TarefaId = idSelecionado });

        return conexao.Execute(ExcluirTarefaSql, new { Id = idSelecionado }) == 1;
    }

    public Tarefa? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        Dictionary<Guid, Tarefa> tarefasPorId = new();

        conexao.Query<TarefaRow, ItemTarefaRow?, Tarefa>(
            SelecionarTarefaPorIdComItensSql,
            (tarefaRow, itemRow) =>
            {
                if (!tarefasPorId.TryGetValue(tarefaRow.Id, out Tarefa? tarefa))
                {
                    tarefa = MapearTarefa(tarefaRow);
                    tarefasPorId[tarefaRow.Id] = tarefa;
                }

                if (itemRow is not null && itemRow.ItemId.HasValue)
                    tarefa.ItemTarefa.Add(MapearItem(itemRow));

                return tarefa;
            },
            new { Id = idSelecionado },
            splitOn: "ItemId"
        );

        return tarefasPorId.Values.SingleOrDefault();
    }

    public List<Tarefa> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao
            .Query<Tarefa>(SelecionarTodasTarefasSql)
            .ToList();
    }

    public List<Tarefa> Filtrar(Predicate<Tarefa> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }

    private static void InserirItens(SqlConnection conexao, Guid tarefaId, List<ItensDeTarefas> itens)
    {
        foreach (ItensDeTarefas item in itens)
        {
            conexao.Execute(
                InserirItemTarefaSql,
                new
                {
                    Id = item.Id,
                    Titulo = item.Titulo,
                    StatusConclusao = item.StatusConclusao,
                    TarefaId = tarefaId
                }
            );
        }
    }

    private static Tarefa MapearTarefa(TarefaRow row)
    {
        return new Tarefa
        {
            Id = row.Id,
            Titulo = row.Titulo,
            PrioridadeTarefa = row.PrioridadeTarefa,
            DataCriacao = row.DataCriacao,
            DataConclusao = row.DataConclusao,
            StatusConclusao = row.StatusConclusao,
            PercentualConcluido = row.PercentualConcluido,
            ItemTarefa = new List<ItensDeTarefas>()
        };
    }

    private static ItensDeTarefas MapearItem(ItemTarefaRow row)
    {
        return new ItensDeTarefas
        {
            Id = row.ItemId!.Value,
            Titulo = row.ItemTitulo!,
            StatusConclusao = row.ItemStatusConclusao!.Value
        };
    }

    private sealed class TarefaRow
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public PrioridadeTarefa PrioridadeTarefa { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public bool StatusConclusao { get; set; }
        public decimal PercentualConcluido { get; set; }
    }

    private sealed class ItemTarefaRow
    {
        public Guid? ItemId { get; set; }
        public string? ItemTitulo { get; set; }
        public bool? ItemStatusConclusao { get; set; }
    }
}