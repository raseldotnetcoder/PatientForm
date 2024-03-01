using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Repository;

public class AllergieRepository : IAllergieRepository
{
    private readonly PatientDbContext _context;

    public AllergieRepository(PatientDbContext _context)
    {
        this._context = _context;
    }

    public async Task<List<Allergie>> GetAllAllergiesAsync()
    {
        return await _context.Allergies.ToListAsync();
    }
}
