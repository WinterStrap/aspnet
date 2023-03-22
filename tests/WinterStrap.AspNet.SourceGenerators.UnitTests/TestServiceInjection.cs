using Microsoft.CodeAnalysis;
using WinterStrap.AspNet.SourceGenerators.ComponentModel;

namespace WinterStrap.AspNet.SourceGenerators.UnitTests;

[TestClass]
public class TestServiceInjection
{
    [TestMethod]
    public void ServiceInjectionTest()
    {
        var source = @"
using System;
using WinterStrap.AspNet.ComponentModel.Attributes;

namespace WinterStrap.AspNet.UnitTests
{
    [Service]
    public class Test_ServiceInjection: ITest_ServiceInjection
    {
        public Test_ServiceInjection()
        {
        }
    }
    [Service]
    public class Test_ServiceInjection2: ITest_ServiceInjection2
    {
        public Test_ServiceInjection()
        {
        }
    }


    public interface ITest_ServiceInjection
    {
    }
    public interface ITest_ServiceInjection2
    {
    }
}
";
        //value to check
        string[] valueToCheck ={
            "public static IServiceCollection AddServices(this IServiceCollection services)",
            "services.AddScoped<WinterStrap.AspNet.UnitTests.ITest_ServiceInjection, WinterStrap.AspNet.UnitTests.Test_ServiceInjection>();",
            "services.AddScoped<WinterStrap.AspNet.UnitTests.ITest_ServiceInjection2, WinterStrap.AspNet.UnitTests.Test_ServiceInjection2>();",
            "return services;"
        };

        CommonTestMethode.VerifyGeneratedCode(source, new ISourceGenerator[] { new ServiceGenerator() },
            "ServiceDependencyInjection.generated.cs", valueToCheck);
    }
}
