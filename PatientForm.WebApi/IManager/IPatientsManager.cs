using PatientForm.ViewModel;

namespace PatientForm.WebApi.IManager;

public interface IPatientsManager
{
    Task<List<PatientInfoViewModel>> GetPatientListAsync();
    Task<PatientInfoViewModel> GetPatientByIdAsync(int id);
    Task<PatientInfoViewModel> GetPatientInfoForCreate();
    Task<bool> CreatePatient(PatientInfoViewModel model);
    PatientInfoViewModel MapNcdToNcdDetail(MapNcdToPatient model);
    PatientInfoViewModel RemoveNcdToNcdDetail(MapNcdToPatient model);
    PatientInfoViewModel MapAllergieToAllergieDetail(MapAllergieToAllergieDetail model);
    PatientInfoViewModel RemoveAllergieToAllergieDetail(MapAllergieToAllergieDetail model);
    Task<PatientInfoViewModel> GetPatientInfoForEdit(int id);
    Task<bool> EditPatient(PatientInfoViewModel model);
    Task<bool> DeletePatient(int id);
}
