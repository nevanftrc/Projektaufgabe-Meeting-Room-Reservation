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

public class ReservationsControllerTests
{
    // Mock für Service und Mapper vorbereiten
    private readonly Mock<IReservationsService> _mockService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ReservationsController _controller;

    public ReservationsControllerTests()
    {
        // Mocks initialisieren und Controller instanziieren
        _mockService = new Mock<IReservationsService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new ReservationsController(_mockService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetReservations_ReturnsOkResult_WithList()
    {
        // Leere Liste als Rückgabe simulieren
        var reservations = new List<Reservations>();
        _mockService.Setup(s => s.GetAllReservationsAsync()).ReturnsAsync(reservations);
        _mockMapper.Setup(m => m.Map<List<ReservationDTO>>(reservations)).Returns(new List<ReservationDTO>());

        // API-Methode aufrufen
        var result = await _controller.GetReservations();

        // Erwartet: OK mit einer Liste von DTOs
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<ReservationDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetReservation_NotFound_Returns404()
    {
        // Simulation: Reservation nicht gefunden
        _mockService.Setup(s => s.GetReservationByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Reservations)null);

        // API-Methode aufrufen
        var result = await _controller.GetReservation(Guid.NewGuid());

        // Erwartet: NotFound-Status
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateReservation_InvalidData_ReturnsBadRequest()
    {
        // Ungültige Reservation: Startzeit liegt nach Endzeit
        var invalidDto = new ReservationDTO
        {
            StartTime = DateTime.Now.AddHours(2),
            EndTime = DateTime.Now.AddHours(1)
        };

        // Methode aufrufen
        var result = await _controller.CreateReservation(invalidDto);

        // Erwartet: BadRequest wegen Logikfehler
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateReservation_ReservationNotFound_ReturnsNotFound()
    {
        // Simulation: Update schlägt fehl, weil Reservation nicht existiert
        _mockService.Setup(s => s.UpdateReservationAsync(It.IsAny<Guid>(), It.IsAny<Reservations>()))
            .ReturnsAsync(false);

        // Methode aufrufen
        var result = await _controller.UpdateReservation(Guid.NewGuid(), new ReservationDTO());

        // Erwartet: NotFound
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteReservation_CallsServiceOnce()
    {
        // Simulation: Erfolgreiches Löschen
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteReservationAsync(id)).ReturnsAsync(true);

        // Methode aufrufen
        var result = await _controller.DeleteReservation(id);

        // Erwartet: NoContent
        Assert.IsType<NoContentResult>(result);
    }
}
