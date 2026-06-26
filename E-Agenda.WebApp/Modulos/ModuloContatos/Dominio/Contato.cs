using System;
using System.Linq; // Necessário para o .All()
using System.Text.RegularExpressions;
using E_Agenda.WebApp.Compartilhado.Dominio;
namespace E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
public class Contatos : EntidadeBase<Contatos>
{
    public string Nome {get; set;}
    public string Email {get ;set;}
    public string Telefone {get; set;}
    public string Cargo {get; set;}
    public string Empresa {get; set;}
    public Contatos(string nome, string telefone, string email, string cargo, string empresa)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        Cargo = cargo;
        Empresa = empresa;
    }

    public override void Atualizar(Contatos entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Telefone = entidadeAtualizada.Telefone;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        // Validação de Nome
        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" deve ser preenchido.");
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        // Validação de Telefone (Regex para 11 dígitos)
        if (string.IsNullOrWhiteSpace(Telefone))
        {
            erros.Add("O campo \"Telefone\" deve ser preenchido.");
        }
        else if (!Regex.IsMatch(Telefone, @"^\d{11}$"))
        {
            erros.Add("O campo \"Telefone\" deve conter exatamente 11 dígitos.");
        }

        return erros;
    }
}