using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(NcdDetail))]
public class NcdDetail
{
    [Key]
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int NcdId { get; set; }
}
