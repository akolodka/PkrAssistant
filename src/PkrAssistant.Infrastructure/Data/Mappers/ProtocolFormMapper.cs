using PkrAssistant.Domain.Protocols;
using PkrAssistant.Infrastructure.Data.Entities;

namespace PkrAssistant.Infrastructure.Data.Mappers;

internal static class ProtocolFormMapper
{
    public static ProtocolForm ToDomain(this ProtocolFormEntity entity)
    {
        return ProtocolForm.FromPersistence(
            
            id: entity.Id, 
            
            name: entity.Name, 
            templateFileId: entity.TemplateFileId,

            isActive: entity.IsActive,

            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt);
    }

    public static ProtocolFormEntity ToEntity(this ProtocolForm domain)
    {
        return new ProtocolFormEntity()
        {
            Id = domain.Id,
            Name = domain.Name,

            TemplateFileId = domain.TemplateFileId,
            IsActive = domain.IsActive,

            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt
        };
    }
}
