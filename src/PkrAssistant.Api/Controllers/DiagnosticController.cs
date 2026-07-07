using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PkrAssistant.Application.Repositories;
using System.Threading.Tasks;

namespace PkrAssistant.Api.Controllers;

/// <summary>
/// Временный диагностический контроллер для проверки подключения к базе данных.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DiagnosticController : ControllerBase
{
    private readonly IVerifierRepository _repository;

    public DiagnosticController(IVerifierRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Проверяет подключение к базе данных.
    /// </summary>
    /// <returns>Количество поверителей в базе.</returns>
    /// <response code="200">Подключение к базе данных успешно.</response>
    /// <response code="500">Ошибка подключения к базе данных.</response>
    [HttpGet]
    [ProducesResponseType(typeof(DiagnosticResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        var verifiers = await _repository.GetAllAsync();

        var response = new DiagnosticResponse
        {
            Status = "Successful database connection",
            RecordsCount = verifiers.Count
        };

        return Ok(response);
    }
}

/// <summary>
/// Ответ диагностического эндпоинта.
/// </summary>
public record DiagnosticResponse
{
    /// <summary>
    /// Статус подключения.
    /// </summary>
    public string Status { get; init; } = string.Empty;

    /// <summary>
    /// Количество записей в базе данных.
    /// </summary>
    public int RecordsCount { get; init; }
}
