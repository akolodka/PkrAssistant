using PkrAssistant.Application.ProtocolAssembly;
using PkrAssistant.Domain.Templates;
using PkrAssistant.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace PkrAssistant.Tests;

public class ProtocolAssemblyServiceTests
{
    private readonly ITestOutputHelper _output;

    public ProtocolAssemblyServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task AssembleAsync_WithAllPartsPresent_ReturnsSuccessWithMergedContent()
    {
        // Assert: настройка зависимостей
        var provider = new InMemoryTemplatePartProvider();

        var header = new HeaderTemplatePart(
            departmentId: Guid.NewGuid(), 
            fileName: "Шапка шаблона", 
            fileContent: new byte[] { 1, 2 });

        provider.AddPart(header);

        var neck = new NeckTemplatePart(
            departmentId: header.DepartmentId,
            measuringInstrumentId: Guid.NewGuid(),
            headerTemplatePartId: header.Id,
            fileName: "Список эталонов шаблона", 
            fileContent: new byte[] { 3, 4 });

        provider.AddPart(neck);

        var assembler = new FakeFileAssembler();

        var service = new ProtocolAssemblyService(provider, assembler);

        IReadOnlyList<Guid> parts = new List<TemplatePart>()
        {
            header,
            neck
        }
        .Select(p => p.Id)
        .ToArray();

        var request = new AssemblyRequest(Guid.NewGuid(), parts);

        //Act
        var result = await service.AssembleAsync(request);

        // Assert
        Assert.True(result.IsSuccess);

        Assert.Null(result.ErrorMessage);

        Assert.Equal(
            new byte[] { 1, 2, 3, 4, }, 
            result.FileContent);
    }

    [Fact]
    public async Task AssembleAsync_WithoutAllParts_ReturnsFailure()
    {
        var provider = new InMemoryTemplatePartProvider();

        var header = new HeaderTemplatePart(
            departmentId: Guid.NewGuid(),
            fileName: "Шапка шаблона",
            fileContent: new byte[] {1,2});

        provider.AddPart(header);

        var neck = new NeckTemplatePart(
            departmentId: header.DepartmentId,
            measuringInstrumentId: Guid.NewGuid(),
            headerTemplatePartId: header.Id,
            fileName: "Список эталонов шаблона",
            fileContent: new byte[] { 3, 4 });

        var assembler = new FakeFileAssembler();

        var service = new ProtocolAssemblyService(provider, assembler);

        IReadOnlyList<Guid> parts = new List<TemplatePart>()
        {
            header,
            neck
        }
        .Select(p => p.Id)
        .ToArray();

        var request = new AssemblyRequest(Guid.NewGuid(), parts);

        //Act
        var result = await service.AssembleAsync(request);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.NotNull(result.ErrorMessage);

        Assert.Contains(
            neck.Id.ToString(), 
            result.ErrorMessage);

        Assert.Null(result.FileContent);

        Assert.Contains("Не найдены части", result.ErrorMessage);
    }

    [Fact]
    public async Task AssembleAsync_WithMixedDepartmentIds_ReturnsFailure()
    {
        var provider = new InMemoryTemplatePartProvider();

        var header = new HeaderTemplatePart(
            departmentId: Guid.NewGuid(),
            fileName: "Шапка шаблона",
            fileContent: new byte[] { 1, 2 });

        provider.AddPart(header);

        var neck = new NeckTemplatePart(
            departmentId: Guid.NewGuid(),
            measuringInstrumentId: Guid.NewGuid(),
            headerTemplatePartId: header.Id,
            fileName: "Список эталонов шаблона",
            fileContent: new byte[] { 3, 4 });

        provider.AddPart(neck);

        var assembler = new FakeFileAssembler();

        var service = new ProtocolAssemblyService(provider, assembler);

        IReadOnlyList<Guid> parts = new List<TemplatePart>()
        {
            header,
            neck
        }
        .Select(p => p.Id)
        .ToArray();

        var request = new AssemblyRequest(Guid.NewGuid(), parts);

        //Act
        var result = await service.AssembleAsync(request);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.NotNull(result.ErrorMessage);

        Assert.Contains("разным отделам", result.ErrorMessage);

        Assert.Null(result.FileContent);
    }
}