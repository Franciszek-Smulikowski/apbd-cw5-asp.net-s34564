using System.ComponentModel.DataAnnotations;

namespace TrainingCenterApi.Models;

public class Room : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "BuildingCode is required.")]
    public string BuildingCode { get; set; } = string.Empty;

    public int Floor { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }

    public bool HasProjector { get; set; }

    public bool IsActive { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            yield return new ValidationResult(
                "Name must not be empty or whitespace.",
                new[] { nameof(Name) });
        }

        if (string.IsNullOrWhiteSpace(BuildingCode))
        {
            yield return new ValidationResult(
                "BuildingCode must not be empty or whitespace.",
                new[] { nameof(BuildingCode) });
        }
    }
}
