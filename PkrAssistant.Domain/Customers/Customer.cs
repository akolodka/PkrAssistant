using System;
using System.Collections.Generic;
using System.Linq;

namespace PkrAssistant.Domain.Customers;

public class Customer
{
    public Guid Id { get; private set; }
    public string ShortName { get; private set; }
    public string FullName { get; private set; }
    public string Inn { get; private set; }
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

        var cleanInn = inn.Trim();

        if ((cleanInn.All(char.IsDigit) == false) || (cleanInn.Length != 10 && cleanInn.Length != 12))
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

        FullName = string.IsNullOrWhiteSpace(fullname)
            ? shortName.Trim()  
            : fullname.Trim();

        Inn = cleanInn;
        LegalAddress = legalAddress.Trim();

        ContactPersons = new List<ContactPerson>();
    }

    public void AddContactPerson(ContactPerson contactPerson)
    {
        ContactPersons.Add(contactPerson);
    }
}
