using System.ComponentModel;
using Microsoft.CodeAnalysis;
using WinterStrap.AspNet.SourceGenerators.ComponentModel;

namespace WinterStrap.AspNet.SourceGenerators.Diagnotics;

internal static class DiagnosticsDescriptor
{
    /// <summary>
    /// Gets a <see cref="DiagnosticDescriptor"/> indicating when a duplicate declaration of <see cref="INotifyPropertyChanged"/> would happen.
    /// <para>
    /// Format: <c>"Cannot apply [ObservableObjectAttribute] to type {0}, as it already declares the INotifyPropertyChanged interface"</c>.
    /// </para>
    /// </summary>
    public static readonly DiagnosticDescriptor DuplicateINotifyPropertyChangedInterfaceForObservableObjectAttributeError = new DiagnosticDescriptor(
        id: "SCGEN0001",
        title: $"No interface to register",
        messageFormat: $"Cannot apply [ObservableObject] to type {{0}}, as it already declares the {nameof(INotifyPropertyChanged)} interface",
        category: typeof(ServiceCollectionGenerator<>).FullName,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: $"Cannot apply [ObservableObject] to a type that already declares the {nameof(INotifyPropertyChanged)} interface.",
        helpLinkUri: "https://www.github.com/WinterStrap/AspNet");
}
