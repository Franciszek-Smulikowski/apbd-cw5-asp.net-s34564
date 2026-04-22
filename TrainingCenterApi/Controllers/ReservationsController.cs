using Microsoft.AspNetCore.Mvc;
using TrainingCenterApi.Data;
using TrainingCenterApi.Models;

namespace TrainingCenterApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetReservations(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = InMemoryDataStore.Reservations;

        if (date.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(reservation =>
                string.Equals(reservation.Status, status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.RoomId == roomId.Value);
        }

        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetReservationById([FromRoute] int id)
    {
        Reservation? reservation = InMemoryDataStore.Reservations.FirstOrDefault(item => item.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> CreateReservation([FromBody] Reservation reservation)
    {
        Room? room = FindRoom(reservation.RoomId);

        if (room is null)
        {
            return BadRequest("Room does not exist.");
        }

        if (!room.IsActive)
        {
            return BadRequest("Room is inactive.");
        }

        if (HasTimeConflict(reservation))
        {
            return Conflict("Reservation time conflicts with an existing reservation.");
        }

        int newId = InMemoryDataStore.Reservations.Count == 0
            ? 1
            : InMemoryDataStore.Reservations.Max(item => item.Id) + 1;

        reservation.Id = newId;
        InMemoryDataStore.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> UpdateReservation([FromRoute] int id, [FromBody] Reservation reservation)
    {
        int reservationIndex = InMemoryDataStore.Reservations.FindIndex(item => item.Id == id);

        if (reservationIndex == -1)
        {
            return NotFound();
        }

        Room? room = FindRoom(reservation.RoomId);

        if (room is null)
        {
            return BadRequest("Room does not exist.");
        }

        if (!room.IsActive)
        {
            return BadRequest("Room is inactive.");
        }

        if (HasTimeConflict(reservation, id))
        {
            return Conflict("Reservation time conflicts with an existing reservation.");
        }

        reservation.Id = id;
        InMemoryDataStore.Reservations[reservationIndex] = reservation;

        return Ok(reservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteReservation([FromRoute] int id)
    {
        Reservation? reservation = InMemoryDataStore.Reservations.FirstOrDefault(item => item.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        InMemoryDataStore.Reservations.Remove(reservation);
        return NoContent();
    }

    private static Room? FindRoom(int roomId)
    {
        return InMemoryDataStore.Rooms.FirstOrDefault(room => room.Id == roomId);
    }

    private static bool HasTimeConflict(Reservation reservation, int? excludedReservationId = null)
    {
        return InMemoryDataStore.Reservations.Any(existing =>
            existing.RoomId == reservation.RoomId &&
            existing.Date == reservation.Date &&
            (!excludedReservationId.HasValue || existing.Id != excludedReservationId.Value) &&
            reservation.StartTime < existing.EndTime &&
            reservation.EndTime > existing.StartTime);
    }
}