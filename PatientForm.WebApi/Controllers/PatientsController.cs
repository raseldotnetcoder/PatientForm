using Microsoft.AspNetCore.Mvc;
using PatientForm.EntityModel;
using PatientForm.ViewModel;
using PatientForm.WebApi.IManager;

namespace PatientForm.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly IPatientsManager _patientsManager;
    private readonly PatientDbContext _context;

    public PatientsController(IPatientsManager _patientsManager, PatientDbContext context)
    {
        this._patientsManager = _patientsManager;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var data = await _patientsManager.GetPatientListAsync();
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var data = await _patientsManager.GetPatientByIdAsync(id.Value);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var data = await _patientsManager.GetPatientInfoForCreate();
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PatientInfoViewModel model)
    {
        bool result = await _patientsManager.CreatePatient(model);
        var json = result ? "Success" : "Error";
        return Ok(json);
    }

    [HttpPost]
    public async Task<IActionResult> MapNcdToNcdDetail(MapNcdToPatient model)
    {
        var data = _patientsManager.MapNcdToNcdDetail(model);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveNcdToNcdDetail(MapNcdToPatient model)
    {
        var data = _patientsManager.RemoveNcdToNcdDetail(model);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> MapAllergieToAllergieDetail(MapAllergieToAllergieDetail model)
    {
        var data = _patientsManager.MapAllergieToAllergieDetail(model);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveAllergieToAllergieDetail(MapAllergieToAllergieDetail model)
    {
        var data = _patientsManager.RemoveAllergieToAllergieDetail(model);
        return Ok(data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var data = await _patientsManager.GetPatientInfoForEdit(id.Value);
        return Ok(data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(PatientInfoViewModel model)
    {
        bool result = await _patientsManager.EditPatient(model);
        var json = result ? "Success" : "Error";
        return Ok(json);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _patientsManager.DeletePatient(id);
        return Ok(result);
    }
}
