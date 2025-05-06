using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace Orderly.Users.ArchitectureTests.Api;

public class ApiTests : BaseTest
{
    [Fact]
    public void ControllerClasses_Should_HaveNameEndingWithController()
    {
        var result = Types.InAssembly(PresentationAssembly)
            .That()
            .Inherit(typeof(ControllerBase))
            .Should()
            .HaveNameEndingWith("Controller")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
