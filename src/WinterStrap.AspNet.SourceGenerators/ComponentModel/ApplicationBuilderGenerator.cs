using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel
{
    [Generator]
    public class ApplicationBuilderGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            out var projectNamespace);

            //get project type (web, console, etc)
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.usingmicrosoftnetsdkweb",out var isWebProject);

            
            if (isWebProject == "true")
            {
                var sourceBuilder = new StringBuilder();
                sourceBuilder.AppendLine("using System;");
                sourceBuilder.AppendLine("using System.Collections.Generic;");
                sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
                sourceBuilder.AppendLine($"namespace {projectNamespace!}");
                sourceBuilder.AppendLine("{");
                sourceBuilder.AppendLine($"    public static class WinterStrapApplicationBuilder");
                sourceBuilder.AppendLine("    {");
                sourceBuilder.AppendLine(
                    $"        public static WebApplicationBuilder CreateBuilder(string[] args)");

                sourceBuilder.AppendLine("        {");
                sourceBuilder.AppendLine("            var builder = WebApplication.CreateBuilder(args);");
                sourceBuilder.AppendLine("            builder.Services.AddConfigurations(builder.Configuration);");
                sourceBuilder.AppendLine("            builder.Services.AddRepositories();");
                sourceBuilder.AppendLine("            builder.Services.AddServices();");
                sourceBuilder.AppendLine("            builder.Services.AddModule(builder.Configuration);");
                sourceBuilder.AppendLine("            return builder;");
                sourceBuilder.AppendLine("        }");
                sourceBuilder.AppendLine("    }");
                sourceBuilder.AppendLine("}");

                context.AddSource("WinterStrapApplicationBuilder.generated.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
            }

        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
