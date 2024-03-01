using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PatientForm.EntityModel;

[Table(nameof(AllergiesDetail))]
public class AllergiesDetail
{
    [Key]
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int AllergiesID { get; set; }
}
