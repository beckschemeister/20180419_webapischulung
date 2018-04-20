using System;
using System.Reflection;

namespace Api.AdventureWorks2012.Productmanagement.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}