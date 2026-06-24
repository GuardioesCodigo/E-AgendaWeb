using System;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace E_Agenda.WebApp.Compartilhado.Apresentacao.Extensions;

public static class TempDataExtensions
{
    public static void AddErrorMessage(this ITempDataDictionary tempData, ResultBase result)
    {
        tempData["MensagemErro"] = result.Errors.First().Message;
    }
}
