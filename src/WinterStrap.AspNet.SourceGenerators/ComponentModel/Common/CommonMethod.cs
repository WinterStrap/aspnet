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
            var data=ns.GetMembers();
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

    private static IEnumerable<INamedTypeSymbol>? GetExternAssemblyTypesWithAttribute(Compilation compilation,
        INamedTypeSymbol attributeSymbol)
    {
        //get assembly names of referenced assemblies of referenced projet without public key
        var assemblyNames= compilation.ReferencedAssemblyNames.Where(x=>!x.HasPublicKey).Select(x=>x.Name);

        foreach (var assemblyName in assemblyNames)
        {
            var assembly = compilation.References
                .Select(x => compilation.GetAssemblyOrModuleSymbol(x) as IAssemblySymbol)
                .Where(x => x != null)
                .FirstOrDefault(x => x!.Name == assemblyName);
            if (assembly != null)
            {
                foreach (var typeSymbol in GetAllTypes(assembly.GlobalNamespace))
                {
                    if (HasAttribute(typeSymbol, attributeSymbol))
                    {
                        yield return typeSymbol;
                    }
                }
            }
        }
    }
    
    internal static IEnumerable<INamedTypeSymbol> GetTypesWithAttribute(Compilation compilation,
        INamedTypeSymbol attributeSymbol)
    {
        var types = GetAllTypes(compilation.Assembly.GlobalNamespace)
            .Where(x => HasAttribute(x, attributeSymbol));

        if (attributeSymbol.Name == "ModuleAttribute")
        {
            var externTypes = GetExternAssemblyTypesWithAttribute(compilation, attributeSymbol);
            if (externTypes.Any())
            {
                types = types.Concat(externTypes);
            }
        }

        return types;
    }


}
