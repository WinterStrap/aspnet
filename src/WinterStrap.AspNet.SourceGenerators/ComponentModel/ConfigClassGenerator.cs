using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.Configuration;
using WinterStrap.AspNet.SourceGenerators.ComponentModel.Common;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

//Classe pour générer une classe partielle avec les propriétés de configuration de l'application (appsettings.json) à partie d'un attribut [ConfigClass] contenant le nom de la section de configuration à générer
/// <summary>
/// Class to generate a partial class with the properties of the application configuration (appsettings.json) from an attribute [ConfigClass]
/// containing the name of the configuration section to generate
/// </summary>
[Generator(LanguageNames.CSharp)]
public class ConfigClassGenerator : ISourceGenerator
{
    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;
        var attributeSymbol = compilation.GetTypeByMetadataName("WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute.ConfigClassAttribute");



        var sourceBuilder = new StringBuilder();
        sourceBuilder.AppendLine("using System;");
        sourceBuilder.AppendLine("using System.Collections.Generic;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.Configuration;");
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            out var projectNamespace);
        sourceBuilder.AppendLine($"namespace {projectNamespace!}");
        sourceBuilder.AppendLine("{");
        sourceBuilder.AppendLine("    public static class CustomConfiguration");
        sourceBuilder.AppendLine("    {");
        sourceBuilder.AppendLine(
            $"        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services,IConfiguration configuration)");
        sourceBuilder.AppendLine("        {");
        foreach (var typeSymbol in CommonMethod.GetTypesWithAttribute(compilation, attributeSymbol!))
        {
            //get the SectionName attribute value
            var sectionName = typeSymbol.GetAttributes().FirstOrDefault(x => x.AttributeClass?.Name == "ConfigClassAttribute")?.ConstructorArguments.FirstOrDefault().Value.ToString();

            var classNamespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
            var className = typeSymbol.Name;

            sourceBuilder.AppendLine(
                $"            services.Configure<{classNamespaceName}.{className}>(configuration.GetSection(\"{sectionName}\"));");
        }

        sourceBuilder.AppendLine("              return services;");
        sourceBuilder.AppendLine("        }");
        sourceBuilder.AppendLine("    }");
        sourceBuilder.AppendLine("}");
        context.AddSource("Configuration.generated.cs",
            SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
    }
}


