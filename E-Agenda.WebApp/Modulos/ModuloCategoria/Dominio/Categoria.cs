using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloDespesas.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCategoria.Dominio;

public class Categoria : EntidadeBase<Categoria>
{
    public string Titulo { get; set; } = string.Empty;
    public List<Despesa> Despesas { get; set; } = new List<Despesa>();

    public Categoria() { }

    public Categoria(string titulo)
    {
        Titulo = titulo;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Titulo))
            erros.Add("O campo \"Título\" deve ser preenchido.");
            
        else if (Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo \"Título\" deve conter entre 2 e 100 caracteres.");

        return erros;
    }

    public List<string> Validar(List<Categoria> categoriasExistentes)
    {
        List<string> erros = Validar();

        bool tituloDuplicado = categoriasExistentes
            .Any(c => c.Id != Id && c.Titulo.Trim().ToLower() == Titulo.Trim().ToLower());

        if (tituloDuplicado)
            erros.Add("Já existe uma categoria cadastrada com este título.");

        return erros;
    }

    public List<string> ValidarExclusao()
    {
        List<string> erros = new List<string>();

        if (Despesas != null && Despesas.Count > 0)
            erros.Add("Não é possível excluir a categoria pois há despesas vinculadas a ela.");

        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;
        Titulo = categoriaAtualizada.Titulo;
    }
}
