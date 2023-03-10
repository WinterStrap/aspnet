using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using WinterStrap.AspNet.SourceGenerators.ComponentModel.Common;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

/// <summary>
/// 
/// </summary>
[Generator]
public class RepositoryInjectGenerator:ISourceGenerator
{
    /// <summary>
    /// Method to initialize the generator
    /// </summary>
    /// <param name="context"></param>
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    /// <summary>
    /// Method to execute the generator
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="Exception"></exception>
    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;
        var attributeSymbol = compilation.GetTypeByMetadataName(
            "Accolades.Pixelies.Identity.RepositoryDependencyInjectionSourceGenerator.RepositoryInjectAttribute");
        var sourceBuilder = new StringBuilder();
        sourceBuilder.AppendLine("using System;");
        sourceBuilder.AppendLine("using System.Collections.Generic;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            out var projectNamespace);
        sourceBuilder.AppendLine($"namespace {projectNamespace!}");
        sourceBuilder.AppendLine("{");
        sourceBuilder.AppendLine("    public static class RepositoryDependencyInjection");
        sourceBuilder.AppendLine("    {");
        var lastPart = projectNamespace!.Split('.').Last();
        sourceBuilder.AppendLine(
            $"        public static IServiceCollection AddRepositories(this IServiceCollection services)");
        sourceBuilder.AppendLine("        {");
        foreach (var typeSymbol in CommonMethod.GetTypesWithAttribute(compilation, attributeSymbol!))
        {
            var classNamespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
            var className = typeSymbol.Name;
            var interfaceName = typeSymbol.Interfaces.FirstOrDefault(x => x.Name == $"I{className}");
            if (interfaceName == null)
            {
                throw new Exception($"Interface I{className} not found for class {className}");
            }

            sourceBuilder.AppendLine(
                $"            services.AddScoped<{interfaceName}, {classNamespaceName}.{className}>();");
        }

        sourceBuilder.AppendLine("              return services;");
        sourceBuilder.AppendLine("        }");
        sourceBuilder.AppendLine("    }");
        sourceBuilder.AppendLine("}");
        context.AddSource("RepositoryDependencyInjection.generated.cs",
            SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }
}
