using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(Allergie))]
public class Allergie
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}
