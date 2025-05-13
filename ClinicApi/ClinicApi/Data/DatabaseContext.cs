using Microsoft.EntityFrameworkCore;
using ClinicApi.Models;

namespace ClinicApi.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@mail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Anna", LastName = "Nowak", Email = "anna.kowalska@mail.com" },
        });
        
        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "Apap", Description = "Lek przeciwbólowy", Type = "tabletka" },
            new Medicament() { IdMedicament = 2, Name = "Aspiryna", Description = "Wzmacniający odporność", Type = "rozpuszczalne" },
        });
        
        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient() { IdPatient = 1, FirstName = "Jolanta", LastName = "Pomorska", Birthdate = new DateOnly(1990, 5, 12) },
            new Patient() { IdPatient = 2, FirstName = "Mariusz", LastName = "Kamiński", Birthdate = new DateOnly(1980, 5, 12) },
        });
        
        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription() { IdPrescription = 1, Date = new DateOnly(2025, 2, 1), DueDate = new DateOnly(2025, 4, 1), IdPatient = 1, IdDoctor = 1 },
            new Prescription() { IdPrescription = 2, Date = new DateOnly(2024, 5, 23), DueDate = new DateOnly(2024, 9, 24), IdPatient = 2, IdDoctor = 2 },
        });
        
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament() { IdPrescription = 1, IdMedicament = 1, Dose = 100, Details = "Detale leku i recepty" },
            new PrescriptionMedicament() { IdPrescription = 2, IdMedicament = 2, Details = "abc xyz" },
        });
    }
}