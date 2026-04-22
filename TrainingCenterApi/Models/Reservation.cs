using System.ComponentModel.DataAnnotations;

namespace TrainingCenterApi.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    [Required(ErrorMessage = "OrganizerName is required.")]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Topic is required.")]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndTime <= StartTime)
        {
            yield return new ValidationResult(
                "EndTime must be later than StartTime.",
                new[] { nameof(EndTime), nameof(StartTime) });
        }
    }
}
