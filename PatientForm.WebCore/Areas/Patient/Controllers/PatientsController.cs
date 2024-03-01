using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using PatientForm.ViewModel;

namespace PatientForm.WebCore.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7109/api/Patients/");
        private readonly HttpClient _httpClient;

        public PatientsController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<PatientInfoViewModel> patientList = new List<PatientInfoViewModel>();

            var restUrl = _httpClient.BaseAddress + "Index";
            HttpResponseMessage response = await _httpClient.GetAsync(restUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patientList = JsonConvert.DeserializeObject<List<PatientInfoViewModel>>(data);
            }
            return View(patientList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();

            var restUrl = _httpClient.BaseAddress + $"Details?id={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(restUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return View(patient);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + "Create";
            HttpResponseMessage response = await _httpClient.GetAsync(restUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientInfoViewModel model)
        {
            string result = "";
            var restUrl = _httpClient.BaseAddress + "Create";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
            }
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + $"Edit?id={id}";
            HttpResponseMessage response = await _httpClient.GetAsync(restUrl);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PatientInfoViewModel model)
        {
            string result = "";
            var restUrl = _httpClient.BaseAddress + "Edit";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return Json(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restUrl = _httpClient.BaseAddress + $"DeleteConfirmed?id={id}";
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, null);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MapNcdToNcdDetail([FromBody] MapNcdToPatient model)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + "MapNcdToNcdDetail";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return PartialView("~/Areas/Patient/Views/Patients/_Ncd.cshtml", patient);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveNcdToNcdDetail([FromBody] MapNcdToPatient model)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + "RemoveNcdToNcdDetail";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return PartialView("~/Areas/Patient/Views/Patients/_Ncd.cshtml", patient);
        }

        [HttpPost]
        public async Task<IActionResult> MapAllergieToAllergieDetail([FromBody] MapAllergieToAllergieDetail model)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + "MapAllergieToAllergieDetail";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return PartialView("~/Areas/Patient/Views/Patients/_Allergie.cshtml", patient);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAllergieToAllergieDetail([FromBody] MapAllergieToAllergieDetail model)
        {
            PatientInfoViewModel patient = new PatientInfoViewModel();
            var restUrl = _httpClient.BaseAddress + "RemoveAllergieToAllergieDetail";
            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(restUrl, content);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                patient = JsonConvert.DeserializeObject<PatientInfoViewModel>(data);
            }
            return PartialView("~/Areas/Patient/Views/Patients/_Allergie.cshtml", patient);
        }
    }
}
