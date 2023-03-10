using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

/// <summary>
/// Class to generate dependency injection
/// </summary>
[Generator]
public class DependencyInjectionGenerator:ISourceGenerator
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
    public void Execute(GeneratorExecutionContext context)
    {
        
        context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            out var projectNamespace);
        var sourceBuilder = new StringBuilder();
        sourceBuilder.AppendLine("using System;");
        sourceBuilder.AppendLine("using System.Collections.Generic;");
        sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
        sourceBuilder.AppendLine($"namespace {projectNamespace!}");
        sourceBuilder.AppendLine("{");
        sourceBuilder.AppendLine("    public static class DependencyInjection");
        sourceBuilder.AppendLine("    {");
        sourceBuilder.AppendLine(
            $"        public static IServiceCollection ConfigureServices(this IServiceCollection services)");
        
        sourceBuilder.AppendLine("        {");
        sourceBuilder.AppendLine("            services.AddRepositories();");
        sourceBuilder.AppendLine("            services.AddServices();");
        sourceBuilder.AppendLine("            return services;");
        sourceBuilder.AppendLine("        }");
        sourceBuilder.AppendLine("    }");
        sourceBuilder.AppendLine("}");
        context.AddSource("DependencyInjection.generated.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }
}
