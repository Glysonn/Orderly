using FluentValidation;
using NetArchTest.Rules;

namespace Orderly.Users.ArchitectureTests.Application;

public class ApplicationTests : BaseTest
{
    [Fact]
    public void Validators_ShouldHave_NameEndingWithValidator()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .HaveNameEndingWith("Validator")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Validators_ShouldNot_BePublic()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .ShouldNot()
            .BePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
   
}
