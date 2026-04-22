using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetRooms(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> rooms = InMemoryDataStore.Rooms;

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(room => room.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(room => room.HasProjector == hasProjector.Value);
        }

        if (activeOnly == true)
        {
            rooms = rooms.Where(room => room.IsActive);
        }

        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetRoomById([FromRoute] int id)
    {
        Room? room = InMemoryDataStore.Rooms.FirstOrDefault(item => item.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetRoomsByBuilding([FromRoute] string buildingCode)
    {
        List<Room> rooms = InMemoryDataStore.Rooms
            .Where(room => string.Equals(room.BuildingCode, buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }
}