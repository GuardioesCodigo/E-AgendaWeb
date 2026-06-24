using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

public class Despesas : EntidadeBase<Despesas>
{
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataOcorrencia { get; set; } = DateTime.Now;
    public decimal Valor { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public List<Categoria> Categorias { get; set; } = new List<Categoria>();

    public Despesas() { }

    public Despesas(
        string descricao,
        decimal valor,
        FormaPagamento formaPagamento,
        List<Categoria> categorias,
        DateTime? dataOcorrencia = null
    )
    {
        Descricao = descricao;
        Valor = valor;
        FormaPagamento = formaPagamento;
        Categorias = categorias;
        DataOcorrencia = dataOcorrencia ?? DateTime.Now;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("O campo \"Descrição\" deve ser preenchido.");

        else if (Descricao.Length < 2 || Descricao.Length > 100)
            erros.Add("O campo \"Descrição\" deve conter entre 2 e 100 caracteres.");

        if (Valor <= 0)
            erros.Add("O campo \"Valor\" deve ser maior que zero.");

        if (!Enum.IsDefined(typeof(FormaPagamento), FormaPagamento))
            erros.Add("O campo \"Forma de Pagamento\" é inválido.");

        if (Categorias == null || Categorias.Count == 0)
            erros.Add("O campo \"Categorias\" deve conter ao menos uma categoria.");

        return erros;
    }

    public override void Atualizar(Despesas entidadeAtualizada)
    {
        Descricao = entidadeAtualizada.Descricao;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        Valor = entidadeAtualizada.Valor;
        FormaPagamento = entidadeAtualizada.FormaPagamento;
        Categorias = entidadeAtualizada.Categorias;
    }

}