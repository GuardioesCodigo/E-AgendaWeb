using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;
using Dapper;
using Microsoft.Data.SqlClient;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesa.Infra;

public sealed class RepositorioDespesaEmSql(ISqlConnectionFactory connectionFactory) : IRepositorioDepesa
{
    private const string InserirDespesaSql = """
        INSERT INTO dbo.TBDespesa (Id, Descricao, DataOcorrencia, Valor, FormaPagamento, CategoriaId)
        VALUES (@Id, @Descricao, @DataOcorrencia, @Valor, @FormaPagamento, @CategoriaId);
    """;

    private const string AtualizarDespesaSql = """
        UPDATE dbo.TBDespesa
        SET Descricao = @Descricao,
            DataOcorrencia = @DataOcorrencia,
            Valor = @Valor,
            FormaPagamento = @FormaPagamento,
            CategoriaId = @CategoriaId
        WHERE Id = @Id;
    """;

    private const string ExcluirDespesaSql = """
        DELETE FROM dbo.TBDespesa
        WHERE Id = @Id;
    """;

    private const string SelecionarTodasDespesasSql = """
        SELECT
            d.Id AS DespesaId,
            d.Descricao AS DespesaDescricao,
            d.DataOcorrencia AS DespesaDataOcorrencia,
            d.Valor AS DespesaValor,
            d.FormaPagamento AS DespesaFormaPagamento,
            c.Id AS CategoriaId,
            c.Titulo AS CategoriaTitulo
        FROM dbo.TBDespesa AS d
        JOIN dbo.TBCategoria AS c
            ON c.Id = d.CategoriaId
        ORDER BY d.DataOcorrencia DESC;
    """;

    private const string SelecionarDespesaPorIdSql = """
        SELECT
            d.Id AS DespesaId,
            d.Descricao AS DespesaDescricao,
            d.DataOcorrencia AS DespesaDataOcorrencia,
            d.Valor AS DespesaValor,
            d.FormaPagamento AS DespesaFormaPagamento,
            c.Id AS CategoriaId,
            c.Titulo AS CategoriaTitulo
        FROM dbo.TBDespesa AS d
        JOIN dbo.TBCategoria AS c
            ON c.Id = d.CategoriaId
        WHERE d.Id = @Id;
    """;

    public void Cadastrar(Despesa entidade)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        conexao.Execute(
            InserirDespesaSql,
            new
            {
                Id = entidade.Id,
                Descricao = entidade.Descricao,
                DataOcorrencia = entidade.DataOcorrencia,
                Valor = entidade.Valor,
                FormaPagamento = entidade.FormaPagamento,
                CategoriaId = entidade.Categoria.Id
            }
        );
    }

    public bool Editar(Guid idSelecionado, Despesa entidadeAtualizada)
    {
        entidadeAtualizada.Id = idSelecionado;

        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(
            AtualizarDespesaSql,
            new
            {
                Id = entidadeAtualizada.Id,
                Descricao = entidadeAtualizada.Descricao,
                DataOcorrencia = entidadeAtualizada.DataOcorrencia,
                Valor = entidadeAtualizada.Valor,
                FormaPagamento = entidadeAtualizada.FormaPagamento,
                CategoriaId = entidadeAtualizada.Categoria.Id
            }
        ) == 1;
    }

    public bool Excluir(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao.Execute(ExcluirDespesaSql, new { Id = idSelecionado }) == 1;
    }

    public Despesa? SelecionarPorId(Guid idSelecionado)
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        DespesaRow? despesaRow = conexao.QuerySingleOrDefault<DespesaRow>(
            SelecionarDespesaPorIdSql,
            new { Id = idSelecionado }
        );

        return despesaRow == null ? null : MapearDespesa(despesaRow);
    }

    public List<Despesa> SelecionarTodos()
    {
        using SqlConnection conexao = connectionFactory.CreateConnection();

        conexao.Open();

        return conexao
            .Query<DespesaRow>(SelecionarTodasDespesasSql)
            .Select(MapearDespesa)
            .ToList();
    }

    public List<Despesa> Filtrar(Predicate<Despesa> filtro)
    {
        return SelecionarTodos().FindAll(filtro);
    }

    private static Despesa MapearDespesa(DespesaRow despesaRow)
    {
        return new Despesa
        {
            Id = despesaRow.DespesaId,
            Descricao = despesaRow.DespesaDescricao,
            DataOcorrencia = despesaRow.DespesaDataOcorrencia,
            Valor = despesaRow.DespesaValor,
            FormaPagamento = despesaRow.DespesaFormaPagamento,
            Categoria = new Categoria
            {
                Id = despesaRow.CategoriaId,
                Titulo = despesaRow.CategoriaTitulo
            }
        };
    }
}

public sealed class DespesaRow
{
    public Guid DespesaId { get; set; }
    public string DespesaDescricao { get; set; } = string.Empty;
    public DateTime DespesaDataOcorrencia { get; set; }
    public decimal DespesaValor { get; set; }
    public FormaPagamento DespesaFormaPagamento { get; set; }
    public Guid CategoriaId { get; set; }
    public string CategoriaTitulo { get; set; } = string.Empty;
}