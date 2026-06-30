using System;
using E_Agenda.WebApp.Compartilhado.Dominio;
using E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

namespace E_Agenda.WebApp.Modulos.ModuloCompromisso.Dominio;

public interface IRepositorioCompromisso : IRepositorio<Compromisso>
{
    bool ExisteVinculoComContato(Guid id);
}
