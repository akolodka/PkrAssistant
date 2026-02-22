using System;
using System.Collections.Generic;
using System.Linq;

namespace PkrAssistant.Domain.Customers;

/// <summary>
/// Заказчик работ (юридическое лицо).
/// </summary>
public class Customer
{
    public Guid Id { get; private set; }

    /// <summary>
    /// Сокращённая форма наименования юридического лица: ООО «Рога и копыта».
    /// </summary>
    public string ShortName { get; private set; }

    /// <summary>
    /// Полная форма наименования юридического лица: Общество с ограниченной ответственностью «Рога и копыта».
    /// </summary>
    public string FullName { get; private set; }

    /// <summary>
    /// ИНН юридического лица (10 или 12 цифр).
    /// </summary>
    public string Inn { get; private set; }

    /// <summary>
    /// Юридический адрес.
    /// </summary>
    public string LegalAddress { get; private set; }

    // Навигационное свойство
    public ICollection<ContactPerson> ContactPersons { get; private set; }

    // Для EF
    private Customer() {}

    public Customer(
        string shortName, 
        string inn, 
        string legalAddress,
        string? fullname = null)
    {
        if (string.IsNullOrWhiteSpace(shortName) == true)
        {
            throw new ArgumentException("Сокращённое наименование заказчика не может быть пустым", nameof(shortName));
        }

        var cleanInn = inn.Trim()
                          .Replace(" ", string.Empty);

        if (IsInnValid(cleanInn) == false)
        {
            throw new ArgumentException("ИНН должен содержать 10 или 12 цифр", nameof(inn));
        }

        if (string.IsNullOrWhiteSpace(legalAddress) == true)
        {
            throw new ArgumentException("Юридический адрес заказчика не может быть пустым", nameof(legalAddress));
        }

        if (string.IsNullOrWhiteSpace(fullname) == true)
        {
            fullname = shortName;
        }

        Id = Guid.NewGuid();
        ShortName = shortName.Trim();

        FullName = (string.IsNullOrWhiteSpace(fullname) == true)
            ? shortName.Trim()  
            : fullname.Trim();

        Inn = cleanInn;
        LegalAddress = legalAddress.Trim();

        ContactPersons = new List<ContactPerson>();
    }

    /// <summary>
    /// Проверяет, что ИНН соответствует формату 10 или 12 цифр.
    /// </summary>
    /// <param name="inn">ИНН после нормализации (без пробелов).</param>
    /// <returns>True, если формат корректен.</returns>
    private static bool IsInnValid(string inn)
    {
        if (inn.All(char.IsDigit) == false)
        {
            return false;
        }

        var hasValidLength = (inn.Length == 10) || (inn.Length == 12);

        if (hasValidLength == false)
        {
            return false;
        }

        return true;
    }

    public void AddContactPerson(ContactPerson contactPerson)
    {
        ContactPersons.Add(contactPerson);
    }
}
