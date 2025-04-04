using Xunit;
using Moq;
using RoomRevAPI.Controllers;
using RoomRevAPI.Services;
using RoomRevAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ReserversControllerTests
{
    // Mock-Service und Mapper initialisieren
    private readonly Mock<IReserversService> _mockService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ReserversController _controller;

    public ReserversControllerTests()
    {
        // Konstruktor setzt die Mocks und erstellt den Controller
        _mockService = new Mock<IReserversService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new ReserversController(_mockService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetReservers_ReturnsOk_WithList()
    {
        // Rückgabe: leere Liste von Reservers
        var reservers = new List<Reservers>();
        _mockService.Setup(s => s.GetAllReserversAsync()).ReturnsAsync(reservers);
        _mockMapper.Setup(m => m.Map<List<ReserverDTO>>(reservers)).Returns(new List<ReserverDTO>());

        // Methode aufrufen
        var result = await _controller.GetReservers();

        // Erwartet: OK mit Liste
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<ReserverDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetReserver_NotFound_Returns404()
    {
        // Simuliere: kein Reserver gefunden
        _mockService.Setup(s => s.GetReserverByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Reservers)null);

        // Aufruf der API
        var result = await _controller.GetReserver(Guid.NewGuid());

        // Erwartet: NotFound
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateReserver_NullInput_ReturnsBadRequest()
    {
        // Übergabe von null sollte Fehler werfen
        var result = await _controller.CreateReserver(null);

        // Erwartet: BadRequest
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateReserver_NotFound_Returns404()
    {
        // Update schlägt fehl
        _mockService.Setup(s => s.UpdateReserverAsync(It.IsAny<Guid>(), It.IsAny<Reservers>()))
            .ReturnsAsync(false);

        // Methode aufrufen
        var result = await _controller.UpdateReserver(Guid.NewGuid(), new ReserverDTO());

        // Erwartet: NotFound
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteReserver_Success_ReturnsNoContent()
    {
        // Erfolgreiches Löschen simulieren
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteReserverAsync(id)).ReturnsAsync(true);

        // Methode ausführen
        var result = await _controller.DeleteReserver(id);

        // Erwartet: NoContent
        Assert.IsType<NoContentResult>(result);
    }
}
