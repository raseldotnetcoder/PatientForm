using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PatientForm.EntityModel;

public class PatientDbContext : DbContext
{
    public PatientDbContext()
    { }

    public PatientDbContext(DbContextOptions<PatientDbContext> options)
    : base(options)
    { }

    public virtual DbSet<Allergie> Allergies { get; set; } = null!;
    public virtual DbSet<AllergiesDetail> AllergiesDetails { get; set; } = null!;
    public virtual DbSet<Disease> Diseases { get; set; } = null!;
    public virtual DbSet<Ncd> Ncds { get; set; } = null!;
    public virtual DbSet<NcdDetail> NcdDetails { get; set; } = null!;
    public virtual DbSet<PatientInfo> Patients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disease>().HasData(
            new Disease() { DiseaseId = 1, DiseaseName = "Fever" },
            new Disease() { DiseaseId = 2, DiseaseName = "Brain" },
            new Disease() { DiseaseId = 3, DiseaseName = "Heart" },
            new Disease() { DiseaseId = 4, DiseaseName = "Kidney" });

        modelBuilder.Entity<Ncd>().HasData(
            new Ncd() { Id = 1, Name = "Asthma" },
            new Ncd() { Id = 2, Name = "Cancer" },
            new Ncd() { Id = 3, Name = "Disorders of ear" },
            new Ncd() { Id = 4, Name = "Disorder of eye" },
            new Ncd() { Id = 5, Name = "Mental illness" },
            new Ncd() { Id = 6, Name = "Oral health problems" });

        modelBuilder.Entity<Allergie>().HasData(
            new Allergie() { Id = 1, Name = "Drugs - Penicillin" },
            new Allergie() { Id = 2, Name = "Drugs - Others" },
            new Allergie() { Id = 3, Name = "Animals" },
            new Allergie() { Id = 4, Name = "Food" },
            new Allergie() { Id = 5, Name = "Oinments" },
            new Allergie() { Id = 6, Name = "Plant" },
            new Allergie() { Id = 7, Name = "Sprays" },
            new Allergie() { Id = 8, Name = "Others" },
            new Allergie() { Id = 9, Name = "No Allergies" });
    }
}
