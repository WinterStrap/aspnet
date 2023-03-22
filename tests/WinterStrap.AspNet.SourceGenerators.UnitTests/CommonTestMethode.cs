using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using WinterStrap.AspNet.ComponentModel.Attributes;

namespace WinterStrap.AspNet.SourceGenerators.UnitTests;

public abstract class CommonTestMethode
{
    internal static void VerifyGeneratedCode(string source, ISourceGenerator[] generators,string filename,
         IEnumerable<string> results)
    {
        // Ensure CommunityToolkit.Mvvm and System.ComponentModel.DataAnnotations are loaded
        Type repositoryInjectAttribute = typeof(RepositoryAttribute);
        Type validationAttributeType = typeof(ValidationAttribute);

        // Get all assembly references for the loaded assemblies (easy way to pull in all necessary dependencies)
        IEnumerable<MetadataReference> references =
            from assembly in AppDomain.CurrentDomain.GetAssemblies()
            where !assembly.IsDynamic
            let reference = MetadataReference.CreateFromFile(assembly.Location)
            select reference;

        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source,
            CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp10));

        // Create a syntax tree with the input source
        CSharpCompilation compilation = CSharpCompilation.Create(
            "original",
            new SyntaxTree[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generators)
            .WithUpdatedParseOptions((CSharpParseOptions)syntaxTree.Options);

        // Run all source generators on the input source code
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        // Ensure that no diagnostics were generated
        CollectionAssert.AreEquivalent(Array.Empty<Diagnostic>(), diagnostics);

        foreach (string expected  in results)
        {
            SyntaxTree generatedTree =
                outputCompilation.SyntaxTrees.Single(tree => Path.GetFileName(tree.FilePath) == filename);

            Assert.IsTrue(generatedTree.ToString().Contains(expected));
        }

        GC.KeepAlive(repositoryInjectAttribute);
        GC.KeepAlive(validationAttributeType);
    }
}
