using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel
{
    //[Generator]
    public class GoToGo// : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            //context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.RootNamespace",
            //out var projectNamespace);
            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine("using System;");
            sourceBuilder.AppendLine("using System.Collections.Generic;");
            sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine($"namespace go");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine("    public static class Dependency");
            sourceBuilder.AppendLine("    {");
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("}");
            context.AddSource("Dependency.generated.cs",sourceBuilder.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {

        }
    }
}
