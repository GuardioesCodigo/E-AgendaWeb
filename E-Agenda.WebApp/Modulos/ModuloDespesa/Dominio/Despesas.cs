using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

public class Despesa : EntidadeBase<Despesa>
{
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataOcorrencia { get; set; } = DateTime.Now;
    public decimal Valor { get; set; }
    public FormaPagamento FormaPagamento { get; set; }
    public Categoria Categoria { get; set; } = null!;

    public Despesa() { }

    public Despesa(
        string descricao,
        decimal valor,
        FormaPagamento formaPagamento,
        Categoria categoria,
        DateTime? dataOcorrencia = null
    )
    {
        Descricao = descricao;
        Valor = valor;
        FormaPagamento = formaPagamento;
        Categoria = categoria;
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

        if (Categoria == null)
            erros.Add("O campo \"Categorias\" deve conter ao menos uma categoria.");

        return erros;
    }

    public override void Atualizar(Despesa entidadeAtualizada)
    {
        Descricao = entidadeAtualizada.Descricao;
        DataOcorrencia = entidadeAtualizada.DataOcorrencia;
        Valor = entidadeAtualizada.Valor;
        FormaPagamento = entidadeAtualizada.FormaPagamento;
        Categoria = entidadeAtualizada.Categoria;
    }

}