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

public class MeetingRoomsControllerTests
{
    // Mocks für Service und Mapper
    private readonly Mock<IMeetingRoomsService> _mockService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly MeetingRoomsController _controller;

    public MeetingRoomsControllerTests()
    {
        // Initialisierung der Mocks und des Controllers
        _mockService = new Mock<IMeetingRoomsService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new MeetingRoomsController(_mockService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetMeetingRooms_ReturnsOk_WithList()
    {
        // Simuliere leere Liste von MeetingRooms
        var rooms = new List<MeetingRooms>();
        _mockService.Setup(s => s.GetAllMeetingRoomsAsync()).ReturnsAsync(rooms);
        _mockMapper.Setup(m => m.Map<List<MeetingRoomDTO>>(rooms)).Returns(new List<MeetingRoomDTO>());

        // API-Methode aufrufen
        var result = await _controller.GetMeetingRooms();

        // Erwartet: OK mit Liste
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<List<MeetingRoomDTO>>(okResult.Value);
    }

    [Fact]
    public async Task GetMeetingRoom_NotFound_Returns404()
    {
        // Kein Raum gefunden
        _mockService.Setup(s => s.GetMeetingRoomByIdAsync(It.IsAny<Guid>())).ReturnsAsync((MeetingRooms)null);

        // API-Methode aufrufen
        var result = await _controller.GetMeetingRoom(Guid.NewGuid());

        // Erwartet: NotFound
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateMeetingRoom_NullInput_ReturnsBadRequest()
    {
        // Ungültiger Body
        var result = await _controller.CreateMeetingRoom(null);

        // Erwartet: BadRequest
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateMeetingRoom_NotFound_Returns404()
    {
        // Update schlägt fehl
        _mockService.Setup(s => s.UpdateMeetingRoomAsync(It.IsAny<Guid>(), It.IsAny<MeetingRooms>()))
            .ReturnsAsync(false);

        // Methode aufrufen
        var result = await _controller.UpdateMeetingRoom(Guid.NewGuid(), new MeetingRoomDTO());

        // Erwartet: NotFound
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteMeetingRoom_Success_ReturnsNoContent()
    {
        // Simuliere erfolgreichen Löschvorgang
        var id = Guid.NewGuid();
        _mockService.Setup(s => s.DeleteMeetingRoomAsync(id)).ReturnsAsync(true);

        // Methode aufrufen
        var result = await _controller.DeleteMeetingRoom(id);

        // Erwartet: NoContent
        Assert.IsType<NoContentResult>(result);
    }
}
