using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface INcdRepository
{
    Task<List<Ncd>> GetAllNcdsAsync();
}
