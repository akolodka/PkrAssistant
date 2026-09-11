using PkrAssistant.Domain.Personnel;
using System;
using System.Collections.Generic;

namespace PkrAssistant.Domain.Tests;

public class VerifierTests
{
    public static IEnumerable<object[]> VerifierTestData()
    {
        yield return new object[] { "Иванов", "Иван", "Инженер", "Иванович" };
        yield return new object[] { "Петров", "Пётр", "Техник", null };
    }

    public static IEnumerable<object[]> InvalidTestData()
    {
        yield return new object[] { "" };
        yield return new object[] { " " };
        yield return new object[] { null };
    }

    [Theory]
    [MemberData(nameof(VerifierTestData))]
    public void Constructor_WithValidData_CreatesVerifier(
            string lastName, 
            string firstName, 
            string position, 
            string patronymic)
    {
        // Act
        var verifier = new Verifier(lastName, firstName, position, patronymic);

        // Assert
        Assert.NotNull(verifier);
        Assert.NotEqual(Guid.Empty, verifier.Id);

        Assert.Equal(firstName, verifier.FirstName);
        Assert.Equal(lastName, verifier.LastName);

        Assert.Equal(patronymic, verifier.Patronymic);
        Assert.Equal(position, verifier.Position);
    }

    [Theory]
    [MemberData(nameof(InvalidTestData))]
    public void Constructor_WithInvalidLastName_ThrowsArgumentException(string lastName)
    {
        // Arrange
        var firstName = "Иван";

        var patronymic = "Иванович";
        var position = "Инженер";

        // Act + Assert
        Assert.Throws<ArgumentException>(
            () => new Verifier(lastName, firstName, position, patronymic));
    }

    [Theory]
    [MemberData(nameof(InvalidTestData))]
    public void Constructor_WithInvalidFirstName_ThrowsArgumentException(string firstName)
    {
        // Arrange
        var lastName = "Иванов";

        var patronymic = "Иванович";
        var position = "Инженер";

        // Act + Assert
        Assert.Throws<ArgumentException>(
            () => new Verifier(lastName, firstName, position, patronymic));
    }

    [Theory]
    [MemberData(nameof(InvalidTestData))]
    public void Constructor_WithInvalidPosition_ThrowsArgumentException(string position)
    {
        // Arrange
        var lastName = "Иванов";
        var firstName = "Иван";

        var patronymic = "Иванович";

        // Act + Assert
        Assert.Throws<ArgumentException>(
            () => new Verifier(lastName, firstName, position, patronymic));
    }

    [Fact]
    public void Constructor_WithWhiteSpacedData_CreatesVerifierWithTrimmedValues()
    {
        // Arrange
        var lastName = " Иванов";
        var firstName = "Иван ";

        var patronymic = " Иванович  ";
        var position = "  Инженер     ";

        // Act
        var verifier = new Verifier(lastName, firstName, position, patronymic);

        // Assert
        Assert.NotNull(verifier);

        Assert.Equal(firstName.Trim(), verifier.FirstName);
        Assert.Equal(lastName.Trim(), verifier.LastName);

        Assert.Equal(patronymic.Trim(), verifier.Patronymic);
        Assert.Equal(position.Trim(), verifier.Position);
    }

    [Theory]
    [MemberData(nameof(InvalidTestData))]
    public void Constructor_WithInvalidPatronymic_CreatesVerifierWithNullPatronymic(string patronymic)
    {
        // Arrange
        var lastName = "Иванов";
        var firstName = "Иван";

        var position = "Инженер";

        // Act
        var verifier = new Verifier(lastName, firstName, position, patronymic);

        // Assert
        Assert.NotNull(verifier);
        Assert.Null(verifier.Patronymic);
    }

    [Theory]
    [MemberData(nameof(VerifierTestData))]
    public void FromPersistence_RestoresVerifierWithSameId(
        string lastName, 
        string firstName, 
        string position, 
        string patronymic)
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var varifier = Verifier.FromPersistence(id, lastName, firstName, position, patronymic);

        // Assert
        Assert.Equal(id, varifier.Id);
        Assert.Equal(lastName, varifier.LastName);

        Assert.Equal(firstName, varifier.FirstName);
        Assert.Equal(position, varifier.Position);

        Assert.Equal(patronymic, varifier.Patronymic);
    }

    [Fact]
    public void GetShortName_WithPatronymic_ReturnsShortNameWithPatronymicInitial()
    {
        // Arrange
        var verifier = new Verifier("Иванов", "Иван", "Инженер", "Иванович");

        // Act
        var shortName = verifier.GetShortName();

        // Assert
        Assert.Equal("Иванов И.И.", shortName);
    }

    [Fact]
    public void GetShortName_WithoutPatronymic_ReturnsShortNameWithFirstNameInitial()
    {
        // Arrange
        var verifier = new Verifier("Иванов", "Иван", "Инженер", null);

        // Act
        var shortName = verifier.GetShortName();

        // Assert
        Assert.Equal("Иванов И.", shortName);
    }
}
