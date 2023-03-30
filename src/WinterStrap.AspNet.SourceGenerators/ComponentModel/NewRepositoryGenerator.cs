using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using WinterStrap.AspNet.SourceGenerators.Models;

namespace WinterStrap.AspNet.SourceGenerators.ComponentModel;

[Generator]
public class NewRepositoryGenerator : ServiceCollectionGenerator<int>
{
    public NewRepositoryGenerator() : base("WinterStrap.AspNet.ComponentModel.Attributes.RepositoryAttribute")
    {
    }

    private protected override int ValidateTargetTypeAndGetInfo(INamedTypeSymbol typeSymbol, AttributeData attributeData, Compilation compilation, out ImmutableArray<DiagnosticInfo> diagnostics)
    {
        diagnostics = ImmutableArray<DiagnosticInfo>.Empty;
        
        if (!typeSymbol.AllInterfaces.Any())
        {
            diagnostics = ImmutableArray.Create(DiagnosticInfo.Create(DuplicateINotifyPropertyChangedInterfaceForObservableObjectAttributeError, typeSymbol, typeSymbol));

            goto End;
        }

        End:
        return 0;
    }
}
