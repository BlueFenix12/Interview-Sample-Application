using Ardalis.Result;
using TrucksManager.Trucks.CQRS.Commands;
using TrucksManager.Trucks.Infrastructure;

namespace TrucksManager.Trucks.UnitTests.Validators;

public class AddTruckCommandValidatorTests
{
    private readonly Mock<ITrucksRepository> mock;
    private readonly AddTruckCommandValidator validator;

    public AddTruckCommandValidatorTests()
    {
        this.mock = new Mock<ITrucksRepository>();
        this.validator = new AddTruckCommandValidator(this.mock.Object);
    }

    [Fact]
    public async Task Validate_CodeIsInvalid_ValidationFails()
    {
        this.mock
            .Setup(x => x.IsTruckCodeValid(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Result.Conflict());

        var command = new AddTruck.Command();

        var validatioResult = await this.validator.ValidateAsync(command);

        validatioResult.Should().NotBeNull();
        validatioResult.IsValid.Should().BeFalse();
        validatioResult.Errors.Should().NotBeEmpty();
        validatioResult.Errors.Should().HaveCount(3);
    }
    
    [Fact]
    public async Task Validate_CodeIsValid_HasBeenTakenByOtherTruck_ValidationFails()
    {
        this.mock
            .Setup(x => x.IsTruckCodeValid(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Result.Conflict());

        var command = new AddTruck.Command { Code = "bla bla"};

        var validatioResult = await this.validator.ValidateAsync(command);

        validatioResult.Should().NotBeNull();
        validatioResult.IsValid.Should().BeFalse();
        validatioResult.Errors.Should().NotBeEmpty();
        validatioResult.Errors.Should().HaveCount(2);
    }
    
    [Fact]
    public async Task Validate_CodeIsValidAndNotTaken_NameIsNotValid_ValidationFails()
    {
        this.mock
            .Setup(x => x.IsTruckCodeValid(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Result.Success());

        var command = new AddTruck.Command { Code = "bla bla" };

        var validatioResult = await this.validator.ValidateAsync(command);

        validatioResult.Should().NotBeNull();
        validatioResult.IsValid.Should().BeFalse();
        validatioResult.Errors.Should().NotBeEmpty();
        validatioResult.Errors.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Validate_CodeIsValidAndNotTaken_NameValid_ValidationSuccess()
    {
        this.mock
            .Setup(x => x.IsTruckCodeValid(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Result.Success());

        var command = new AddTruck.Command { Code = "bla bla", Name = "bla bla"};

        var validatioResult = await this.validator.ValidateAsync(command);

        validatioResult.Should().NotBeNull();
        validatioResult.IsValid.Should().BeTrue();
        validatioResult.Errors.Should().BeEmpty();
    }
}