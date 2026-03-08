using System;

namespace PkrAssistant.Domain.Personnel;

/// <summary>
/// Сотрудник, проводящий поверку (поверитель).
/// </summary>
public class Verifier
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Отчество (при наличии).
    /// </summary>
    public string? Patronymic {  get; private set; }

    /// <summary>
    /// Должность.
    /// </summary>
    public string Position { get; private set; }

    // Для EF
    private Verifier() { }

    public Verifier(
        string lastName, 
        string firstName, 
        string position, 
        string? patronymic = null)
    {
        if (string.IsNullOrWhiteSpace(lastName) == true)
        {
            throw new ArgumentException("Фамилия не может быть пустой", nameof(lastName));
        }

        if (string.IsNullOrWhiteSpace(firstName) == true)
        {
            throw new ArgumentException("Имя не может быть пустым", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(position) == true)
        {
            throw new ArgumentException("Должность не может быть пустой", nameof(position));   
        }

        Id = Guid.NewGuid();

        LastName = lastName.Trim();
        FirstName = firstName.Trim();

        Patronymic = (string.IsNullOrWhiteSpace(patronymic) == true) 
            ? null 
            : patronymic.Trim();
        
        Position = position.Trim();
    }

    /// <summary>
    /// Возвращает Фамилию И.О. поверителя.
    /// </summary>
    public string GetShortName()
    {
        var firstNameInitial = $"{FirstName[0]}.";

        var patronymicInitial = (string.IsNullOrWhiteSpace(Patronymic) == true) 
            ? string.Empty 
            : $"{Patronymic[0]}.";

        return $"{LastName} {firstNameInitial}{patronymicInitial}";
    }

    /// <summary>
    /// Возвращает Фамилию Имя Отчество поверителя.
    /// </summary>
    public string GetFullName()
    {
        var patronymic = (string.IsNullOrWhiteSpace(Patronymic) == true)
            ? string.Empty
            : Patronymic;

        return $"{LastName} {FirstName} {patronymic}"
            .Trim();
    }
}
