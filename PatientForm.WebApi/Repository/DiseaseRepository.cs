using Microsoft.EntityFrameworkCore;
using PatientForm.EntityModel;
using PatientForm.WebApi.IRepository;

namespace PatientForm.WebApi.Repository;

public class DiseaseRepository : IDiseaseRepository
{
    private readonly PatientDbContext _diseaseRepository;

    public DiseaseRepository(PatientDbContext _diseaseRepository)
    {
        this._diseaseRepository = _diseaseRepository;
    }

    public async Task<List<Disease>> GetAllDiseasesAsync()
    {
        return await _diseaseRepository.Diseases.ToListAsync();
    }
}
