using Microsoft.CodeAnalysis;
using WinterStrap.AspNet.SourceGenerators.Helpers;
using WinterStrap.AspNet.SourceGenerators.Models;

namespace WinterStrap.AspNet.SourceGenerators.Extensions;

internal static class DiagnoticsExtensions
{
    /// <summary>
    /// Registers an output node into an <see cref="IncrementalGeneratorInitializationContext"/> to output diagnostics.
    /// </summary>
    /// <param name="context">The input <see cref="IncrementalGeneratorInitializationContext"/> instance.</param>
    /// <param name="diagnostics">The input <see cref="IncrementalValuesProvider{TValues}"/> sequence of diagnostics.</param>
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DiagnosticInfo> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, static (context, diagnostic) =>
        {
            context.ReportDiagnostic(diagnostic.ToDiagnostic());
        });
    }
    
    /// <summary>
    /// Registers an output node into an <see cref="IncrementalGeneratorInitializationContext"/> to output diagnostics.
    /// </summary>
    /// <param name="context">The input <see cref="IncrementalGeneratorInitializationContext"/> instance.</param>
    /// <param name="diagnostics">The input <see cref="IncrementalValuesProvider{TValues}"/> sequence of diagnostics.</param>
    public static void ReportDiagnostics(this IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<EquatableArray<DiagnosticInfo>> diagnostics)
    {
        context.RegisterSourceOutput(diagnostics, static (context, diagnostics) =>
        {
            foreach (DiagnosticInfo diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic.ToDiagnostic());
            }
        });
    }
}
