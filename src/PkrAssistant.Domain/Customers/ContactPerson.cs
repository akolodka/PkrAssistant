using System;

namespace PkrAssistant.Domain.Customers;

/// <summary>
/// Контактное лицо заказчика.
/// </summary>
public class ContactPerson
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    /// <summary>
    /// Номер телефона в международном формате +7XXXXXXXXXX (без пробелов и дефисов).
    /// </summary>
    public string Phone { get; private set; }

    public string? Email { get; private set; }

    /// <summary>
    /// Заметка о контактном лице.
    /// </summary>
    public string? Note { get; private set; }

    /// <summary>
    /// Является ли контактное лицо приоритетным для связи.
    /// </summary>
    public bool IsPriorityContact { get; private set; }

    // Для EF
    private ContactPerson() {}

    public ContactPerson(
        string name, 
        string phone, 
        string? email = null, 
        bool isPriorityContact = false,
        string? note = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя контактного лица не может быть пустым", nameof(name));
        }
        
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Номер телефона контактного лица не может быть пустым", nameof(phone));
        }

        Id = Guid.NewGuid();

        Name = name.Trim();

        var cleanPhone = phone.Trim()
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("(", string.Empty)
            .Replace(")", string.Empty);

        if (IsPhoneValid(cleanPhone) is false)
        {
            throw new ArgumentException("Номер телефона должен быть в формате +7XXXXXXXXXX или 8XXXXXXXXXX", nameof(phone));
        }

        // Нормализация: привести номер к +7
        Phone = cleanPhone.StartsWith("8")
            ? "+7" + cleanPhone.Substring(1) 
            : cleanPhone;

        Email = string.IsNullOrWhiteSpace(email)
            ? null 
            : email.Trim();

        IsPriorityContact = isPriorityContact;

        Note = string.IsNullOrWhiteSpace(note)
            ? null 
            : note.Trim();
    }

    /// <summary>
    /// Проверяет, что номер телефона соответствует формату +7XXXXXXXXXX или 8XXXXXXXXXX.
    /// </summary>
    /// <param name="phone">Номер телефона после нормализации (без пробелов и дефисов).</param>
    /// <returns>True, если формат корректен.</returns>
    private static bool IsPhoneValid(string phone)
    {
        var hasValidPrefix = phone.StartsWith("+7") || phone.StartsWith("8");

        var hasValidLength = phone.Replace("+", string.Empty).Length == 11;

        return hasValidPrefix && hasValidLength;
    }
}
