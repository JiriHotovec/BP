using System.Reflection;

namespace Hotovec.Orders.Architecture.Test;

public static class AssemblyConstants
{
    public const string ApiAssemblyName = "Hotovec.Orders.Api";
    public const string ApplicationAssemblyName = "Hotovec.Orders.Application";
    public const string DomainAssemblyName = "Hotovec.Orders.Domain";
    public const string InfrastructureAssemblyName = "Hotovec.Orders.Infrastructure";

    public static readonly Assembly ApiAssembly = Assembly.Load(ApiAssemblyName);
    public static readonly Assembly ApplicationAssembly = Assembly.Load(ApplicationAssemblyName);
    public static readonly Assembly DomainAssembly = Assembly.Load(DomainAssemblyName);
    public static readonly Assembly InfrastructureAssembly = Assembly.Load(InfrastructureAssemblyName);
}
