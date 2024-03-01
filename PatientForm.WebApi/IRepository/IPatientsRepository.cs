using PatientForm.EntityModel;

namespace PatientForm.WebApi.IRepository;

public interface IPatientsRepository
{
    Task<List<PatientInfo>> GetAllPatientsAsync();
    Task<PatientInfo> GetPatientByIdAsync(int id);
    Task<bool> CreatePatientAsync(PatientInfo model);
    Task<bool> CreatePatientMapping(List<NcdDetail> NcdDetails, List<AllergiesDetail> AllergiesDetails);
    Task<bool> RemovePatientMapping(List<NcdDetail> NcdDetails, List<AllergiesDetail> AllergiesDetails);
    Task<bool> UpdatePatientAsync(PatientInfo model);
    Task<bool> DeletePatientAsync(int id);
}
