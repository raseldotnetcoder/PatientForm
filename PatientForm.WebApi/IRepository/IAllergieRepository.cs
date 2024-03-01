
using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface IAllergieRepository
{
    Task<List<Allergie>> GetAllAllergiesAsync();
}
