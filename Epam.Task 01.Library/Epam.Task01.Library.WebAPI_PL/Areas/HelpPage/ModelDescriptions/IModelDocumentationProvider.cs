using System;
using System.Reflection;

namespace Epam.Task01.Library.WebAPI_PL.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}