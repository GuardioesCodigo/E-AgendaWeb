using System;
using System.Linq; // Necessário para o .All()
using System.Net.Mail;
using System.Text.RegularExpressions;
using E_Agenda.WebApp.Compartilhado.Dominio;
namespace E_Agenda.WebApp.Modulos.ModuloContatos.Dominio;
public class Contatos : EntidadeBase<Contatos>
{
    public string Nome {get; set;}
    public string Email {get ;set;}
    public string ValidarEmail()
    {
        try
        {
            // O MailAddress tenta analisar o formato
            var addr = new MailAddress(Email);
            
            // Verifica se o endereço analisado é exatamente o que foi enviado
            // Isso evita coisas como "nome@dominio" (sem .com) passarem
            if (addr.Address != Email)
                return "O e-mail informado não está em um formato válido.";

            return null; // Nulo significa que está ok
        }
        catch
        {
            return "O e-mail informado não está em um formato válido.";
        }
    }
    public string Telefone {get; set;}
    public string Cargo {get; set;}
    public string Empresa {get; set;}
    public Contatos(string nome, string telefone, string? email, string? cargo, string empresa)
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
        Email = entidadeAtualizada.Email;
        Cargo = entidadeAtualizada.Cargo;
        Empresa = entidadeAtualizada.Empresa;
    }

  public override List<string> Validar()
{
    List<string> erros = new List<string>();

    if (string.IsNullOrWhiteSpace(Nome))
        erros.Add("O nome é obrigatório.");

    // Esta Regex é mais rigorosa: exige @, um domínio e finaliza com letras (.com, .br, etc)
    // O $ no final é o que impede de você digitar "com22222"
    string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

    if (string.IsNullOrWhiteSpace(Email))
    {
        erros.Add("O e-mail é obrigatório.");
    }
    else if (!Regex.IsMatch(Email, emailRegex))
    {
        erros.Add("O formato do e-mail é inválido (ex: usuario@email.com).");
    }

    // Se passou na regex, aí sim podemos checar o MailAddress se quiser
    return erros;
}

public List<string> ValidarDuplicidade(List<Contatos> contatosExistentes)
{
    List<string> erros = new List<string>();

   bool emailExiste = contatosExistentes.Any(c => 
        c.Email != null && 
        c.Email.Equals(this.Email, StringComparison.OrdinalIgnoreCase) && 
        c.Id != this.Id 
    );

    if (emailExiste)
        erros.Add("Já existe um contato registrado com este E-mail.");

    // 2. Verifica se o TELEFONE já existe
    bool telefoneExiste = contatosExistentes.Any(c => 
        c.Telefone != null && 
        c.Telefone.Equals(this.Telefone) && 
        c.Id != this.Id 
    );

    if (telefoneExiste)
        erros.Add("Já existe um contato registrado com este Telefone.");

    return erros;
}
}