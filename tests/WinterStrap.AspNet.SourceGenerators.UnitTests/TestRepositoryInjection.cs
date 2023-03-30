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
    public class Test_RepositoryInjection: ITest_RepositoryInjection
    {
        public Test_RepositoryInjection()
        {
        }
    }

    public interface ITest_RepositoryInjection
    {
    }
}
";
        //value to check
        string[] valueToCheck ={
            "public static IServiceCollection AddRepositories(this IServiceCollection services)",
            "services.AddScoped<WinterStrap.AspNet.SourceGenerators.UnitTests.ITest_RepositoryInjection, WinterStrap.AspNet.SourceGenerators.UnitTests.Test_RepositoryInjection>();",
            "return services;"
        };

        CommonTestMethode.VerifyGeneratedCode(source, new ISourceGenerator[] { new RepositoryGenerator() },
            "RepositoryDependencyInjection.generated.cs", valueToCheck);
    }

    
    
}
