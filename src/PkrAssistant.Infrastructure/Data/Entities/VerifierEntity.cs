using System;

namespace PkrAssistant.Infrastructure.Data.Entities;

internal class VerifierEntity
{
    public Guid Id { get; set; }

    public string LastName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? Patronymic { get; set; }

    public string Position {  get; set; } = string.Empty;
}
