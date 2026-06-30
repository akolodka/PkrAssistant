using Microsoft.EntityFrameworkCore;
using PkrAssistant.Application.Repositories;
using PkrAssistant.Domain.Personnel;
using PkrAssistant.Infrastructure.Data;
using PkrAssistant.Infrastructure.Data.Entities;
using PkrAssistant.Infrastructure.Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PkrAssistant.Infrastructure.Repositories;

internal class VerifierRepository : IVerifierRepository
{
    private readonly AppDbContext _context;

    public VerifierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Verifier verifier, CancellationToken ct = default)
    {
        var entity = verifier.ToEntity();

        // синхронно потому, что Id генерится в домене
        _context.Set<VerifierEntity>().Add(entity);

        await _context.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<Verifier>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _context.Set<VerifierEntity>()
            .ToListAsync(ct);

        var domains = entities
            .Select(e => e.ToDomain())
            .ToList()
            .AsReadOnly();

        return domains;
    }

    public async Task<Verifier?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _context.Set<VerifierEntity>()
            .FirstOrDefaultAsync(e => e.Id == id, ct);

        if (entity is null)
        {
            return null;
        }

        var domain = entity.ToDomain();

        return domain;
    }
}
