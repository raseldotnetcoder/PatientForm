using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientWebApi.Repository;

public class PatientsRepository : IPatientsRepository
{
    private readonly PatientDbContext _context;

    public PatientsRepository(PatientDbContext _context)
    {
        this._context = _context;
    }

    public async Task<bool> CreatePatientAsync(PatientInfo model)
    {
        await _context.Patients.AddAsync(model);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CreatePatientMapping(List<NcdDetail> NcdDetails, List<AllergiesDetail> AllergiesDetails)
    {
        await _context.NcdDetails.AddRangeAsync(NcdDetails);
        await _context.AllergiesDetails.AddRangeAsync(AllergiesDetails);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
            _context.Patients.Remove(patient);

        var ncdDetails = _context.NcdDetails.Where(x => x.PatientId == id).ToList();
        var allergieDetails = _context.AllergiesDetails.Where(x => x.PatientId == id).ToList();

        _context.NcdDetails.RemoveRange(ncdDetails);
        _context.AllergiesDetails.RemoveRange(allergieDetails);

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<List<PatientInfo>> GetAllPatientsAsync()
    {
        return await _context.Patients.ToListAsync();
    }

    public async Task<PatientInfo> GetPatientByIdAsync(int id)
    {
        return await _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id) ?? new PatientInfo();
    }

    public async Task<bool> RemovePatientMapping(List<NcdDetail> NcdDetails, List<AllergiesDetail> AllergiesDetails)
    {
        _context.NcdDetails.RemoveRange(NcdDetails);
        _context.AllergiesDetails.RemoveRange(AllergiesDetails);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdatePatientAsync(PatientInfo model)
    {
        _context.Patients.Update(model);
        return await _context.SaveChangesAsync() > 0;
    }
}
