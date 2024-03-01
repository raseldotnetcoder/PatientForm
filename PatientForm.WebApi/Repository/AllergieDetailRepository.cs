using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Repository;

public class AllergieDetailRepository : IAllergieDetailRepository
{
    private readonly PatientDbContext _context;

    public AllergieDetailRepository(PatientDbContext _context)
    {
        this._context = _context;
    }

    public async Task<List<AllergiesDetail>> GetAllergieDetailsByPatientIdAsync(int id)
    {
        return await _context.AllergiesDetails.Where(x => x.PatientId == id).ToListAsync();
    }
}
