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
    public async Task AssembleAsync_WithAllPartsPresent_ReturnsSuccessWithFileContent()
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

        var preliminary = new PreliminaryInspectionPart(
            departmentId: header.DepartmentId,
            fileName: "Операции опробования",
            fileContent: new byte[] { 5, 6 });

        provider.AddPart(preliminary);

        var metrological = new MetrologicalInspectionPart(
            departmentId: header.DepartmentId,
            fileName: "Метрологические характеристики",
            fileContent: new byte[] {7, 8});

        provider.AddPart(metrological);

        var footer = new FooterTemplatePart(
            departmentId: header.DepartmentId,
            fileName: "Подпись к шаблону поверки",
            fileContent: new byte[] {9, 10});

        provider.AddPart(footer);

        var assembler = new FakeFileAssembler();

        var service = new ProtocolAssemblyService(provider, assembler);

        IReadOnlyList<Guid> parts = new List<TemplatePart>()
        {
            header,
            neck,
            preliminary,
            metrological,
            footer
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
            new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10},
            result.FileContent);
    }

    [Fact]
    public async Task AssembleAsync_WithMissingTemplalteParts_ReturnsFailure()
    {
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
    public async Task AssembleAsync_WithEmptyFileConent_ReturnsFailure()
    {
        var provider = new InMemoryTemplatePartProvider();

        var header = new HeaderTemplatePart(
            departmentId: Guid.NewGuid(),
            fileName: "Шапка шаблона",
            fileContent: new byte[] {1,2});
        
        // Имитация "грязных" данных, пришедших из БД
        header.FileContent = Array.Empty<byte>();

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

        Assert.Contains("не содержат данные", result.ErrorMessage);

        Assert.Null(result.FileContent);
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

    [Fact]
    public async Task AssembleAsync_WithMissingRequiredPartTypes_ReturnsFailure()
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
        Assert.False(result.IsSuccess);

        Assert.NotNull(result.ErrorMessage);

        Assert.Contains("Недостаёт частей шаблона", result.ErrorMessage);
    }

    [Fact]
    public async Task AssembleAsync_WithDuplicatePartTypes_ReturnsFailure()
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

        var preliminary = new PreliminaryInspectionPart(
            departmentId: header.DepartmentId,
            fileName: "Операции опробования",
            fileContent: new byte[] { 5, 6 });

        provider.AddPart(preliminary);

        var metrological = new MetrologicalInspectionPart(
            departmentId: header.DepartmentId,
            fileName: "Метрологические характеристики",
            fileContent: new byte[] { 7, 8 });

        provider.AddPart(metrological);

        var footer = new FooterTemplatePart(
            departmentId: header.DepartmentId,
            fileName: "Подпись к шаблону поверки",
            fileContent: new byte[] { 9, 10 });

        provider.AddPart(footer);

        var duplicate = new FooterTemplatePart(
            departmentId: header.DepartmentId,
            fileName: "Подпись к шаблону поверки",
            fileContent: new byte[] { 9, 10 });

        provider.AddPart(duplicate);

        var assembler = new FakeFileAssembler();

        var service = new ProtocolAssemblyService(provider, assembler);

        IReadOnlyList<Guid> parts = new List<TemplatePart>()
        {
            header,
            neck,
            preliminary,
            metrological,
            footer,
            duplicate
        }
        .Select(p => p.Id)
        .ToArray();

        var request = new AssemblyRequest(Guid.NewGuid(), parts);

        //Act
        var result = await service.AssembleAsync(request);

        // Assert
        Assert.False(result.IsSuccess);

        Assert.NotNull(result.ErrorMessage);

        Assert.Contains("дубликаты", result.ErrorMessage);
    }
}