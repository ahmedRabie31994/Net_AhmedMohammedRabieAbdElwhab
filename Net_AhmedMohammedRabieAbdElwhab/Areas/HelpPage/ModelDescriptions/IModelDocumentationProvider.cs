using System;
using System.Reflection;

namespace Net_AhmedMohammedRabieAbdElwhab.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}