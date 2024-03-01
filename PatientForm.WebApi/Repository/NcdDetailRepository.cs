using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Repository;

public class NcdDetailRepository : INcdDetailRepository
{
    private readonly PatientDbContext _context;

    public NcdDetailRepository(PatientDbContext _context)
    {
        this._context = _context;
    }

    public async Task<List<NcdDetail>> GetNcdDetailsByPatientIdAsync(int id)
    {
        return await _context.NcdDetails.Where(x => x.PatientId == id).ToListAsync();
    }
}
