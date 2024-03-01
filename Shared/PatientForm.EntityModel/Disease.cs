using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(Disease))]
public class Disease
{
    [Key]
    public int DiseaseId { get; set; }
    public string? DiseaseName { get; set; }
}
