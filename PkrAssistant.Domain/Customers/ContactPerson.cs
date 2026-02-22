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
    /// Является ли контактное лицо приоритетным для связи.
    /// </summary>
    public bool IsPriorityContact { get; private set; }

    // Для EF
    private ContactPerson() {}

    public ContactPerson(
        string name, 
        string phone, 
        string? email = null, 
        bool isPriorityContact = false)
    {
        if (string.IsNullOrWhiteSpace(name) == true)
        {
            throw new ArgumentException("Имя контактного лица не может быть пустым", nameof(name));
        }
        
        if (string.IsNullOrWhiteSpace(phone) == true)
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

        if (IsPhoneValid(cleanPhone) == false)
        {
            throw new ArgumentException("Номер телефона должен быть в формате +7XXXXXXXXXX или 8XXXXXXXXXX", nameof(phone));
        }

        // Нормализация: привести номер к +7
        Phone = (cleanPhone.StartsWith("8") == true)
            ? "+7" + cleanPhone.Substring(1) 
            : cleanPhone;

        Email = (string.IsNullOrWhiteSpace(email) == true)
            ? null 
            : email.Trim();

        IsPriorityContact = isPriorityContact;
    }

    /// <summary>
    /// Проверяет, что номер телефона соответствует формату +7XXXXXXXXXX или 8XXXXXXXXXX.
    /// </summary>
    /// <param name="phone">Номер телефона после нормализации (без пробелов и дефисов).</param>
    /// <returns>True, если формат корректен.</returns>
    private static bool IsPhoneValid(string phone)
    {
        var hasValidPrefix = (phone.StartsWith("+7") == true) || (phone.StartsWith("8") == true);

        if (hasValidPrefix == false)
        {
            return false;
        }

        var hasValidLength = (phone.Replace("+", string.Empty).Length == 11);

        if (hasValidLength == false)
        {
            return false;
        }

        return true;
    }
}
