using TrainingCenterApi.Models;

namespace TrainingCenterApi.Data;

public static class InMemoryDataStore
{
    public static List<Room> Rooms { get; } = new()
    {
        new Room { Id = 1, Name = "A-101", BuildingCode = "A", Floor = 1, Capacity = 18, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "A-205", BuildingCode = "A", Floor = 2, Capacity = 30, HasProjector = false, IsActive = true },
        new Room { Id = 3, Name = "B-012", BuildingCode = "B", Floor = 0, Capacity = 12, HasProjector = true, IsActive = true },
        new Room { Id = 4, Name = "B-310", BuildingCode = "B", Floor = 3, Capacity = 45, HasProjector = true, IsActive = false },
        new Room { Id = 5, Name = "A-303", BuildingCode = "A", Floor = 3, Capacity = 24, HasProjector = false, IsActive = true },
        new Room { Id = 6, Name = "B-120", BuildingCode = "B", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true }
    };

    public static List<Reservation> Reservations { get; } = new()
    {
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Computer Science Department",
            Topic = "C# Basics Workshop",
            Date = new DateOnly(2026, 5, 6),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(12, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "HR Team",
            Topic = "Onboarding Training",
            Date = new DateOnly(2026, 5, 7),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(13, 0),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 1,
            OrganizerName = "Data Science Club",
            Topic = "Python for Analysts",
            Date = new DateOnly(2026, 5, 8),
            StartTime = new TimeOnly(14, 0),
            EndTime = new TimeOnly(16, 30),
            Status = "planned"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 3,
            OrganizerName = "External Partner",
            Topic = "Project Management Essentials",
            Date = new DateOnly(2026, 5, 10),
            StartTime = new TimeOnly(9, 30),
            EndTime = new TimeOnly(11, 30),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 5,
            OrganizerName = "QA Team",
            Topic = "Test Automation Introduction",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(11, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 6,
            RoomId = 2,
            OrganizerName = "Finance Department",
            Topic = "Excel Advanced Training",
            Date = new DateOnly(2026, 5, 15),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 30),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 7,
            RoomId = 6,
            OrganizerName = "Operations Team",
            Topic = "Safety and Procedures",
            Date = new DateOnly(2026, 5, 18),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 0),
            Status = "planned"
        }
    };
}
