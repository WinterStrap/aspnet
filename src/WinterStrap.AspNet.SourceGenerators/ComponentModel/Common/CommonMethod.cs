using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel.Common;

/// <summary>
/// 
/// </summary>
internal abstract class CommonMethod
{
    private static IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceOrTypeSymbol namespaceSymbol)
    {
        if (namespaceSymbol is INamespaceSymbol ns)
        {
            foreach (var member in ns.GetMembers())
            {
                foreach (var type in GetAllTypes(member))
                {
                    yield return type;
                }
            }
        }
        else if (namespaceSymbol is INamedTypeSymbol namedTypeSymbol)
        {
            yield return namedTypeSymbol;
            foreach (var typeArgument in namedTypeSymbol.GetTypeMembers())
            {
                foreach (var type in GetAllTypes(typeArgument))
                {
                    yield return type;
                }
            }
        }
    }

    private static bool HasAttribute(INamedTypeSymbol typeSymbol, INamedTypeSymbol attributeSymbol)
    {
        return typeSymbol.GetAttributes()
            .Any(x => x.AttributeClass!.Equals(attributeSymbol, SymbolEqualityComparer.Default));
    }

    internal static IEnumerable<INamedTypeSymbol> GetTypesWithAttribute(Compilation compilation,
        INamedTypeSymbol attributeSymbol)
    {
        return GetAllTypes(compilation.Assembly.GlobalNamespace)
            .Where(x => HasAttribute(x, attributeSymbol));
    }
}
