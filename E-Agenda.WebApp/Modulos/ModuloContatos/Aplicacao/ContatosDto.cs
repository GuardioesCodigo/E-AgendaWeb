using System;

namespace E_Agenda.WebApp.Modulos.ModuloContatos.Aplicacao;

// DTO para cadastrar um novo paciente (sem Id, pois ele é gerado pelo sistema)
public class CadastrarContatosDTO
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}

// DTO para Editar (obrigatoriamente precisa do Id para localizar o registro no banco)
public class EditarContatosDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
}

// DTO para Listagens (Otimizado)
public class ListarContatosDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}

// DTO para confirmação de exclusão
public class ExcluirContatosDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}