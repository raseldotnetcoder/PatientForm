using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientForm.Core.Enums;

namespace PatientForm.ViewModel;

public class PatientInfoViewModel
{
    public PatientInfoViewModel()
    {
        DiseaseList = new List<DiseaseViewModel>();
        LeftNcdList = new List<SelectListItem>();
        RightNcdList = new List<SelectListItem>();
        SelectedNcdList = new List<SelectListItem>();
        LeftAllergieList = new List<SelectListItem>();
        RightAllergieList = new List<SelectListItem>();
        SelectedAllergieList = new List<SelectListItem>();
    }

    public int PatientId { get; set; }

    [DisplayName("Patient Name")]
    [Required(ErrorMessage = "Patient Name is required.")]
    public string? PatientName { get; set; }

    [Required(ErrorMessage = "Epilepsy is required.")]
    public int? Epilepsy { get; set; }

    [DisplayName("Disease Name")]
    [Required(ErrorMessage = "Disease Name is required.")]
    public int? DiseaseId { get; set; }
    public string? DiseaseName { get; set; }

    public List<DiseaseViewModel>? DiseaseList { get; set; }
    public Epilepsy? EpilepsyList { get; set; }

    [DisplayName("Other NCDs")]
    public IEnumerable<SelectListItem>? LeftNcdList { get; set; }
    public IEnumerable<SelectListItem>? RightNcdList { get; set; }
    public IEnumerable<SelectListItem>? SelectedNcdList { get; set; }

    [DisplayName("Allergies")]
    public IEnumerable<SelectListItem>? LeftAllergieList { get; set; }
    public IEnumerable<SelectListItem>? RightAllergieList { get; set; }
    public IEnumerable<SelectListItem>? SelectedAllergieList { get; set; }
}

public class MapNcdToPatient
{
    public MapNcdToPatient()
    {
        LeftNcdList = new List<NcdViewModel>();
        RightNcdList = new List<NcdViewModel>();
        SelectedNcdList = new List<NcdViewModel>();
    }

    public List<NcdViewModel> LeftNcdList { get; set; }
    public List<NcdViewModel> RightNcdList { get; set; }
    public List<NcdViewModel> SelectedNcdList { get; set; }
}

public class MapAllergieToAllergieDetail
{
    public MapAllergieToAllergieDetail()
    {
        LeftAllergieList = new List<AllergiesViewModel>();
        RightAllergieList = new List<AllergiesViewModel>();
        SelectedAllergieList = new List<AllergiesViewModel>();
    }

    public List<AllergiesViewModel> LeftAllergieList { get; set; }
    public List<AllergiesViewModel> RightAllergieList { get; set; }
    public List<AllergiesViewModel> SelectedAllergieList { get; set; }
}
