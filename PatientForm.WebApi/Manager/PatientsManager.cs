using Microsoft.AspNetCore.Mvc.Rendering;
using PatientForm.EntityModel;
using PatientForm.ViewModel;
using PatientForm.WebApi.IManager;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Manager;

public class PatientsManager : IPatientsManager
{
    private readonly IPatientsRepository _patientsRepository;
    private readonly IDiseaseRepository _diseaseRepository;
    private readonly INcdRepository _ncdRepository;
    private readonly INcdDetailRepository _ncdDetailRepository;
    private readonly IAllergieRepository _allergieRepository;
    private readonly IAllergieDetailRepository _allergieDetailRepository;

    public PatientsManager(IPatientsRepository _patientsRepository
        , IDiseaseRepository _diseaseRepository
        , INcdRepository _ncdRepository
        , INcdDetailRepository _ncdDetailRepository
        , IAllergieRepository _allergieRepository
        , IAllergieDetailRepository _allergieDetailRepository)
    {
        this._patientsRepository = _patientsRepository;
        this._diseaseRepository = _diseaseRepository;
        this._ncdRepository = _ncdRepository;
        this._ncdDetailRepository = _ncdDetailRepository;
        this._allergieRepository = _allergieRepository;
        this._allergieDetailRepository = _allergieDetailRepository;

    }

    public async Task<bool> CreatePatient(PatientInfoViewModel model)
    {
        PatientInfo patientInfo = new PatientInfo()
        {
            PatientName = model.PatientName,
            Epilepsy = model.Epilepsy.Value,
            DiseaseId = model.DiseaseId.Value
        };
        bool isPatientCraete = await _patientsRepository.CreatePatientAsync(patientInfo);

        if (isPatientCraete)
        {
            var NcdDetails = new List<NcdDetail>();
            var AllergiesDetails = new List<AllergiesDetail>();
            if (model.SelectedNcdList != null && model.SelectedNcdList.Any())
            {
                NcdDetails = model.SelectedNcdList
                    .Select(item => new NcdDetail
                    {
                        NcdId = int.Parse(item.Value),
                        PatientId = patientInfo.PatientId
                    }).ToList();
            }
            if (model.SelectedAllergieList != null && model.SelectedAllergieList.Any())
            {
                AllergiesDetails = model.SelectedAllergieList
                    .Select(item => new AllergiesDetail
                    {
                        AllergiesID = int.Parse(item.Value),
                        PatientId = patientInfo.PatientId
                    }).ToList();
            }
            bool result = await _patientsRepository.CreatePatientMapping(NcdDetails, AllergiesDetails);
            if (result) return true;
        }
        return false;
    }

    public async Task<bool> DeletePatient(int id)
    {
        return await _patientsRepository.DeletePatientAsync(id);
    }

    public async Task<bool> EditPatient(PatientInfoViewModel model)
    {
        var oldNcdDetails = await _ncdDetailRepository.GetNcdDetailsByPatientIdAsync(model.PatientId);
        var oldAllergieDetails = await _allergieDetailRepository.GetAllergieDetailsByPatientIdAsync(model.PatientId);
        var isRemoveMapping = await _patientsRepository.RemovePatientMapping(oldNcdDetails, oldAllergieDetails);

        PatientInfo patientInfo = new PatientInfo()
        {
            PatientId = model.PatientId,
            PatientName = model.PatientName,
            Epilepsy = model.Epilepsy.Value,
            DiseaseId = model.DiseaseId.Value
        };
        var isUpdatePatient = await _patientsRepository.UpdatePatientAsync(patientInfo);

        if (isUpdatePatient)
        {
            var NcdDetails = new List<NcdDetail>();
            var AllergiesDetails = new List<AllergiesDetail>();
            if (model.SelectedNcdList != null && model.SelectedNcdList.Any())
            {
                NcdDetails = model.SelectedNcdList
                    .Select(item => new NcdDetail
                    {
                        NcdId = int.Parse(item.Value),
                        PatientId = patientInfo.PatientId
                    }).ToList();
            }
            if (model.SelectedAllergieList != null && model.SelectedAllergieList.Any())
            {
                AllergiesDetails = model.SelectedAllergieList
                    .Select(item => new AllergiesDetail
                    {
                        AllergiesID = int.Parse(item.Value),
                        PatientId = patientInfo.PatientId
                    }).ToList();
            }
            await _patientsRepository.CreatePatientMapping(NcdDetails, AllergiesDetails);
            return true;
        }
        return false;
    }

    public async Task<PatientInfoViewModel> GetPatientByIdAsync(int id)
    {
        PatientInfoViewModel model = new PatientInfoViewModel();

        var diseaseList = await _diseaseRepository.GetAllDiseasesAsync();
        var patient = await _patientsRepository.GetPatientByIdAsync(id);

        model.PatientId = patient.PatientId;
        model.PatientName = patient.PatientName;
        model.DiseaseId = patient.DiseaseId;
        model.DiseaseName = diseaseList.Where(x => x.DiseaseId == patient.DiseaseId).Select(x => x.DiseaseName).FirstOrDefault() ?? string.Empty;
        model.Epilepsy = patient.Epilepsy;

        var ncds = await _ncdRepository.GetAllNcdsAsync();
        var ncdDetails = await _ncdDetailRepository.GetNcdDetailsByPatientIdAsync(id);

        var rightNcds = ncds.Where(x => ncdDetails.Any(ncdDetail => ncdDetail.NcdId == x.Id)).ToList();
        model.RightNcdList = rightNcds.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        var allergies = await _allergieRepository.GetAllAllergiesAsync();
        var allergieDetails = await _allergieDetailRepository.GetAllergieDetailsByPatientIdAsync(id);

        var rightAllergies = allergies.Where(x => allergieDetails.Any(allergieDetail => allergieDetail.AllergiesID == x.Id)).ToList();
        model.RightAllergieList = rightAllergies.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        return model;
    }

    public async Task<PatientInfoViewModel> GetPatientInfoForCreate()
    {
        PatientInfoViewModel model = new PatientInfoViewModel();
        var diseases = await _diseaseRepository.GetAllDiseasesAsync();
        model.DiseaseList = diseases?.Select(item => new DiseaseViewModel
        {
            DiseaseId = item.DiseaseId,
            DiseaseName = item.DiseaseName
        }).ToList();

        var ncds = await _ncdRepository.GetAllNcdsAsync();
        model.LeftNcdList = ncds.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        var allergies = await _allergieRepository.GetAllAllergiesAsync();
        model.LeftAllergieList = allergies.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        return model;
    }

    public async Task<PatientInfoViewModel> GetPatientInfoForEdit(int id)
    {
        PatientInfoViewModel model = new PatientInfoViewModel();
        var patient = await _patientsRepository.GetPatientByIdAsync(id);
        model.PatientId = patient.PatientId;
        model.PatientName = patient.PatientName;
        model.DiseaseId = patient.DiseaseId;
        model.Epilepsy = patient.Epilepsy;

        var diseases = await _diseaseRepository.GetAllDiseasesAsync();
        model.DiseaseList = diseases?.Select(item => new DiseaseViewModel
        {
            DiseaseId = item.DiseaseId,
            DiseaseName = item.DiseaseName
        }).ToList();

        var ncds = await _ncdRepository.GetAllNcdsAsync();
        var ncdDetails = await _ncdDetailRepository.GetNcdDetailsByPatientIdAsync(id);

        var rightNcds = ncds.Where(x => ncdDetails.Any(ncdDetail => ncdDetail.NcdId == x.Id)).ToList();
        model.RightNcdList = rightNcds.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        var leftNcds = ncds.Except(rightNcds).ToList();
        model.LeftNcdList = leftNcds.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        var allergies = await _allergieRepository.GetAllAllergiesAsync();
        var allergieDetails = await _allergieDetailRepository.GetAllergieDetailsByPatientIdAsync(id);

        var rightAllergies = allergies.Where(x => allergieDetails.Any(allergieDetail => allergieDetail.AllergiesID == x.Id)).ToList();
        model.RightAllergieList = rightAllergies.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();

        var leftallergies = allergies.Except(rightAllergies).ToList();
        model.LeftAllergieList = leftallergies.Select(item => new SelectListItem
        {
            Value = item.Id.ToString(),
            Text = item.Name,
            Selected = false
        }).ToList();
        return model;
    }

    public async Task<List<PatientInfoViewModel>> GetPatientListAsync()
    {
        List<PatientInfoViewModel> model = new List<PatientInfoViewModel>();
        var diseaseList = await _diseaseRepository.GetAllDiseasesAsync();
        var patients = await _patientsRepository.GetAllPatientsAsync();
        model = patients.Join(diseaseList,
            patient => patient.DiseaseId,
            disease => disease.DiseaseId,
            (patient, disease) => new PatientInfoViewModel
            {
                PatientId = patient.PatientId,
                PatientName = patient.PatientName,
                DiseaseId = patient.DiseaseId,
                DiseaseName = disease.DiseaseName,
                Epilepsy = patient.Epilepsy
            }
        ).ToList();
        return model;
    }

    public PatientInfoViewModel MapAllergieToAllergieDetail(MapAllergieToAllergieDetail model)
    {
        PatientInfoViewModel patientInfo = new PatientInfoViewModel();
        if (model.SelectedAllergieList != null && model.SelectedAllergieList.Any())
        {
            model.LeftAllergieList = model.LeftAllergieList.Where(x => !model.SelectedAllergieList.Any(selectedAllergie => selectedAllergie.Id == x.Id)).ToList();
            model.RightAllergieList.AddRange(model.SelectedAllergieList);

            patientInfo.LeftAllergieList = model.LeftAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.RightAllergieList = model.RightAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.SelectedAllergieList = model.SelectedAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = true
            }).ToList();
        }
        return patientInfo;
    }

    public PatientInfoViewModel MapNcdToNcdDetail(MapNcdToPatient model)
    {
        PatientInfoViewModel patientInfo = new PatientInfoViewModel();
        if (model.SelectedNcdList != null && model.SelectedNcdList.Any())
        {
            model.LeftNcdList = model.LeftNcdList.Where(x => !model.SelectedNcdList.Any(selectedNcd => selectedNcd.Id == x.Id)).ToList();
            model.RightNcdList.AddRange(model.SelectedNcdList);

            patientInfo.LeftNcdList = model.LeftNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.RightNcdList = model.RightNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.SelectedNcdList = model.SelectedNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = true
            }).ToList();
        }
        return patientInfo;
    }

    public PatientInfoViewModel RemoveAllergieToAllergieDetail(MapAllergieToAllergieDetail model)
    {
        PatientInfoViewModel patientInfo = new PatientInfoViewModel();
        if (model.SelectedAllergieList != null && model.SelectedAllergieList.Any())
        {
            model.RightAllergieList = model.RightAllergieList.Where(x => !model.SelectedAllergieList.Any(selectedAllergie => selectedAllergie.Id == x.Id)).ToList();
            model.LeftAllergieList.AddRange(model.SelectedAllergieList);

            patientInfo.LeftAllergieList = model.LeftAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.RightAllergieList = model.RightAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.SelectedAllergieList = model.SelectedAllergieList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = true
            }).ToList();
        }
        return patientInfo;
    }

    public PatientInfoViewModel RemoveNcdToNcdDetail(MapNcdToPatient model)
    {
        PatientInfoViewModel patientInfo = new PatientInfoViewModel();
        if (model.SelectedNcdList != null && model.SelectedNcdList.Any())
        {
            model.RightNcdList = model.RightNcdList.Where(x => !model.SelectedNcdList.Any(selectedNcd => selectedNcd.Id == x.Id)).ToList();
            model.LeftNcdList.AddRange(model.SelectedNcdList);

            patientInfo.LeftNcdList = model.LeftNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.RightNcdList = model.RightNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = false
            }).ToList();

            patientInfo.SelectedNcdList = model.SelectedNcdList.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
                Selected = true
            }).ToList();
        }
        return patientInfo;
    }
}
