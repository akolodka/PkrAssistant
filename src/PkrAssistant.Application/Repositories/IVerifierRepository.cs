using PkrAssistant.Domain.Personnel;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PkrAssistant.Application.Repositories;
public interface IVerifierRepository
{
    Task<Verifier?> GetByIdAcync(Guid id, CancellationToken ct = default);

    Task<IReadOnlyList<Verifier>> GetAllAsync(CancellationToken ct = default);

    Task AddAsync(Verifier verifier, CancellationToken ct = default);
}
