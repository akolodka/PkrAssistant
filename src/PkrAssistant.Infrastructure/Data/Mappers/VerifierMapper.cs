using PkrAssistant.Domain.Personnel;
using PkrAssistant.Infrastructure.Data.Entities;

namespace PkrAssistant.Infrastructure.Data.Mappers;

internal static class VerifierMapper
{
    public static Verifier ToDomain(this VerifierEntity entity)
    {
        return Verifier.Reconstruct(

            id: entity.Id,

            lastName: entity.LastName,
            firstName: entity.FirstName,

            position: entity.Position,
            patronymic: entity.Patronymic);
    }

    public static VerifierEntity ToEntity(this Verifier domain)
    {
        return new VerifierEntity
        {
            Id = domain.Id,
            LastName = domain.LastName,

            FirstName = domain.FirstName,
            Patronymic = domain.Patronymic,

            Position = domain.Position,
        };
    }
}
