using Microsoft.AspNetCore.Mvc;
using PkrAssistant.Application.Repositories;
using System.Threading.Tasks;

namespace PkrAssistant.Api.Controllers;

/// <summary>
/// Временный диагностический контроллер для проверки подключения к БД.
/// Только для ручного тестирования через Postman или браузер.
/// Скрыт от Swagger. Может быть удалён после настройки интеграционных тестов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class DiagnosticController : ControllerBase
{
    private readonly IVerifierRepository _repository;

    public DiagnosticController(IVerifierRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        var verifiers = await _repository.GetAllAsync();

        return Ok(new
        {
            Status = "Successfull database connection",
            RecordsCount = verifiers.Count
        });
    }
}
