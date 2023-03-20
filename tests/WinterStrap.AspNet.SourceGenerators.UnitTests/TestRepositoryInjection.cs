using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using WinterStrap.AspNet.ComponentModel.Attributes;
using WinterStrap.AspNet.SourceGenerators.ComponentModel;

namespace WinterStrap.AspNet.SourceGenerators.UnitTests;

[TestClass]
public class TestRepositoryInjection
{
    [TestMethod]
    public void RepositoryInjectionTest()
    {
        var source = @"

using System;
using WinterStrap.AspNet.ComponentModel.Attributes;

namespace WinterStrap.AspNet.SourceGenerators.UnitTests
{
    [Repository]
    public class RepositoryDependencyInjection: IRepositoryDependencyInjection
    {
        public RepositoryDependencyInjection()
        {
        }
    }

    public interface IRepositoryDependencyInjection
    {
    }
}
";
        var result = @"
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace WinterStrap.AspNet.SourceGenerators.UnitTests
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<WinterStrap.AspNet.SourceGenerators.UnitTests.IRepositoryDependencyInjection, WinterStrap.AspNet.SourceGenerators.UnitTests.RepositoryDependencyInjection>();
              return services;
        }
    }
}
";
        VerifyGeneratedCode(source, new ISourceGenerator[] { new RepositoryGenerator() }, ("RepositoryDependencyInjection.generated.cs", result));
    }

    private static void VerifyGeneratedCode(string source, ISourceGenerator[] generators,
        params (string Filename, string Text)[] results)
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
        
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp10));

        // Create a syntax tree with the input source
        CSharpCompilation compilation = CSharpCompilation.Create("original",
            new SyntaxTree[] { syntaxTree },
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generators)
            .WithUpdatedParseOptions((CSharpParseOptions)syntaxTree.Options);

        // Run all source generators on the input source code
        _ = driver.RunGeneratorsAndUpdateCompilation(compilation, out Compilation outputCompilation,
            out ImmutableArray<Diagnostic> diagnostics);

        // Ensure that no diagnostics were generated
        //CollectionAssert.AreEquivalent(Array.Empty<Diagnostic>(), diagnostics);

        foreach ((string filename, string text) in results)
        {
            SyntaxTree generatedTree =
                outputCompilation.SyntaxTrees.Single(tree => Path.GetFileName(tree.FilePath) == filename);

            Assert.AreEqual(text, generatedTree.ToString());
        }

        GC.KeepAlive(repositoryInjectAttribute);
        GC.KeepAlive(validationAttributeType);
    }
}
