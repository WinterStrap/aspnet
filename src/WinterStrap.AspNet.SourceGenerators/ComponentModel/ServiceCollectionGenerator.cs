using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WinterStrap.AspNet.SourceGenerators.Extensions;
using WinterStrap.AspNet.SourceGenerators.Models;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

/// <summary>
/// A base class to generate dependency injection based on an attribute.
/// </summary>
public abstract partial class ServiceCollectionGenerator<TInfo> : IIncrementalGenerator
    where TInfo : IEquatable<TInfo>
{
    /// <summary>
    /// The full qualified attribute name.
    /// </summary>
    private readonly string _fullQualifiedAttributeName;

    /// <summary>
    /// Initialize a new <see cref="ServiceCollectionGenerator{TInfo}"/>
    /// </summary>
    /// <param name="fullQualifiedAttributeName">The fully qualified attribute name (namespace + name)</param>
    protected ServiceCollectionGenerator(string fullQualifiedAttributeName)
    {
        _fullQualifiedAttributeName = fullQualifiedAttributeName;
    }

    /// <summary>
    /// Initialize the generator.
    /// </summary>
    /// <param name="context">The context generator.</param>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var generationInfoWithErrors =
            context.SyntaxProvider.ForAttributeWithMetadataName(
                    this._fullQualifiedAttributeName,
                    static (node, _) => node is ClassDeclarationSyntax classDeclaration &&
                                        classDeclaration.HasOrPotentiallyHasAttributes(),
                    (c, _) =>
                    {
                        if (!c.SemanticModel.Compilation.HasLanguageVersionAtLeastEqualTo(LanguageVersion.CSharp8))
                        {
                            return default;
                        }

                        INamedTypeSymbol typeSymbol = (INamedTypeSymbol)c.TargetSymbol;

                        // Gather all generation info, and any diagnostics
                        TInfo? info = ValidateTargetTypeAndGetInfo(typeSymbol, c.Attributes[0],
                            c.SemanticModel.Compilation, out ImmutableArray<DiagnosticInfo> diagnostics);

                        // If there are any diagnostics, there's no need to compute the hierarchy info at all, just return them
                        if (diagnostics.Length > 0)
                        {
                            return new Result<(HierarchyInfo, MetadataInfo?, TInfo?)>(default, diagnostics);
                        }

                        HierarchyInfo hierarchy = HierarchyInfo.From(typeSymbol);
                        MetadataInfo metadataInfo = new(typeSymbol.IsSealed,
                            Execute.IsNullabilitySupported(c.SemanticModel.Compilation));

                        return new Result<(HierarchyInfo, MetadataInfo?, TInfo?)>((hierarchy, metadataInfo, info),
                            diagnostics);
                    })
                .Where(static item => item is not null);

        // Emit the diagnostic, if needed
        context.ReportDiagnostics(generationInfoWithErrors.Select(static (item, _) => item.Errors));

        // Get the filtered sequence to enable caching
        IncrementalValuesProvider<(HierarchyInfo Hierarchy, MetadataInfo MetadataInfo, TInfo Info)> generationInfo =
            generationInfoWithErrors
                .Where(static item => item is { Errors.IsEmpty: true })
                .Select(static (item, _) => item.Value)!;

        context.RegisterSourceOutput(generationInfo, (context, item) =>
        {
        });
    }

    /// <summary>
    /// Validates the target type being processes, gets the info if possible and produces all necessary diagnostics.
    /// </summary>
    /// <param name="typeSymbol">The <see cref="INamedTypeSymbol"/> instance currently being processed.</param>
    /// <param name="attributeData">The <see cref="AttributeData"/> instance for the attribute used over <paramref name="typeSymbol"/>.</param>
    /// <param name="compilation">The compilation that <paramref name="typeSymbol"/> belongs to.</param>
    /// <param name="diagnostics">The resulting diagnostics, if any.</param>
    /// <returns>The extracted info for the current type, if possible.</returns>
    /// <remarks>If <paramref name="diagnostics"/> is empty, the returned info will always be ignored and no sources will be produced.</remarks>
    private protected abstract TInfo? ValidateTargetTypeAndGetInfo(INamedTypeSymbol typeSymbol,
        AttributeData attributeData, Compilation compilation, out ImmutableArray<DiagnosticInfo> diagnostics);

    /// <summary>
    /// A small record for metadata info on types to generate.
    /// </summary>
    /// <param name="IsSealed">Whether the target type is sealed.</param>
    /// <param name="IsNullabilitySupported">Whether nullability attributes are supported.</param>
    private sealed record MetadataInfo(bool IsSealed, bool IsNullabilitySupported);
}
