using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface IDiseaseRepository
{
    Task<List<Disease>> GetAllDiseasesAsync();
}
