using WinterStrap.AspNet.SourceGenerators.ComponentModel.Attribute;

namespace Sample;

[RepositoryInject]
public class MyClasse: IMyClasse
{
    
}

[ServiceInject]
public class MyClasse2: IMyClasse2
{
    
}

public interface IMyClasse2
{

}
