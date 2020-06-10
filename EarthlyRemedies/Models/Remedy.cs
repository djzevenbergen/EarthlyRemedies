using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace EarthlyRemedies.Models
{
  public class Remedy : IValidatableObject
  {

    public int RemedyId { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public string Details { get; set; }

    [StringLength(30)]
    public string Ailment { get; set; }
    [Required]
    public string Category { get; set; }
    public string Ingredients { get; set; }
    public int UserId { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (!EnvironmentVariables.Categories.Contains(Category))
      {
        yield return new ValidationResult(
            $"Category is not contained in Categories",
            new[] { "Category" });
      }
    }
  }

}