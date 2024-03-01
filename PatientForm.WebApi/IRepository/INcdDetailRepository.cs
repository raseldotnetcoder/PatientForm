using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface INcdDetailRepository
{
    Task<List<NcdDetail>> GetNcdDetailsByPatientIdAsync(int id);
}
