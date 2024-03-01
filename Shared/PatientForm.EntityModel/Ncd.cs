using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(Ncd))]
public class Ncd
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
}
