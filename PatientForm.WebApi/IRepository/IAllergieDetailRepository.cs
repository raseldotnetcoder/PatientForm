
using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface IAllergieDetailRepository
{
    Task<List<AllergiesDetail>> GetAllergieDetailsByPatientIdAsync(int id);
}
