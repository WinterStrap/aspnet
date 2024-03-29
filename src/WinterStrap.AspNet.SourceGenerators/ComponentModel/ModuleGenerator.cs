﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using WinterStrap.AspNet.SourceGenerators.ComponentModel.Common;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

/// <summary>
/// Class who generate a module for external libraries.
/// </summary>
[Generator]
public class ModuleGenerator : ISourceGenerator
{
    /// <summary>
    /// Method to execute the generator
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="Exception"></exception>
    public void Execute(GeneratorExecutionContext context)
    {
        var compilation = context.Compilation;
        var attributeSymbol = compilation.GetTypeByMetadataName("WinterStrap.AspNet.ComponentModel.Attributes.ModuleAttribute");
        var sourceBuilder = new StringBuilder();
        sourceBuilder.AppendLine("// <auto-generated/>");
        sourceBuilder.AppendLine("#pragma warning disable");
        sourceBuilder.AppendLine("using System;");
        sourceBuilder.AppendLine("using System.Collections.Generic;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.Configuration;");
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            out var projectNamespace);
        sourceBuilder.AppendLine($"namespace {projectNamespace!}");
        sourceBuilder.AppendLine("{");
        sourceBuilder.AppendLine("    public static class ModuleInjection");
        sourceBuilder.AppendLine("    {");
        //var lastPart = projectNamespace!.Split('.').Last();
        sourceBuilder.AppendLine(
            $"        public static IServiceCollection AddModule(this IServiceCollection services,IConfiguration configuration)");
        sourceBuilder.AppendLine("        {");
        foreach (var typeSymbol in CommonMethod.GetTypesWithAttribute(compilation, attributeSymbol!))
        {
            var classNamespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
            var className = typeSymbol.Name;

            sourceBuilder.AppendLine($"            {classNamespaceName}.DependencyInjection.AddDependencies(services,configuration);");
        }

        sourceBuilder.AppendLine("              return services;");
        sourceBuilder.AppendLine("        }");
        sourceBuilder.AppendLine("    }");
        sourceBuilder.AppendLine("}");
        context.AddSource("ModuleInjection.generated.cs",
            SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    /// <summary>
    /// Initialize the generator.
    /// </summary>
    /// <param name="context">The generator context.</param>
    public void Initialize(GeneratorInitializationContext context)
    {
    }
}
