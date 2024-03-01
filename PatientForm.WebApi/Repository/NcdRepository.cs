using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Repository;

public class NcdRepository : INcdRepository
{
    private readonly PatientDbContext _context;

    public NcdRepository(PatientDbContext _context)
    {
        this._context = _context;
    }

    public async Task<List<Ncd>> GetAllNcdsAsync()
    {
        return await _context.Ncds.ToListAsync();
    }
}
