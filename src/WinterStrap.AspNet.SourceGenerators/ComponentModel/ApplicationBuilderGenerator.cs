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
            var projectType = context.Compilation.Assembly.Identity.Name;
            
            //get project sdk name
            var sdk = context.Compilation.Assembly.Identity.Version;

            //get project name
            var project = projectNamespace?.Split('.')[0];
            
            

            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("using System;");
            sourceBuilder.AppendLine("using System.Collections.Generic;");
            sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine($"namespace {projectNamespace!}");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine($"    public static class {project}ApplicationBuilder");
            sourceBuilder.AppendLine("    {");
            sourceBuilder.AppendLine(
                $"        public static WebApplicationBuilder CreateBuilder(string[] args)");

            sourceBuilder.AppendLine("        {");
            sourceBuilder.AppendLine("            var builder = WebApplication.CreateBuilder(args);");
            sourceBuilder.AppendLine("            builder.Services.AddConfigurations(builder.Configuration);");
            sourceBuilder.AppendLine("            builder.Services.AddRepositories();");
            sourceBuilder.AppendLine("            builder.Services.AddServices();");
            sourceBuilder.AppendLine("            return builder;");
            sourceBuilder.AppendLine("        }");
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("}");
            context.AddSource("DependencyInjection.generated.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}
