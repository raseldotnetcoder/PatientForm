using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(PatientInfo))]
public class PatientInfo
{
    [Key]
    public int PatientId { get; set; }
    public string? PatientName { get; set; }
    public int Epilepsy { get; set; }
    public int DiseaseId { get; set; }

    public virtual ICollection<NcdDetail> NcdDetails { get; set; }
    public virtual ICollection<AllergiesDetail> AllergiesDetails { get; set; }
}
