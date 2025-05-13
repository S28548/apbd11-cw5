using ClinicApi.Data;
using ClinicApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Patient?> GetPatientByIdAsync(int id)
    {
        return await _context.Patients.FindAsync(id);
    }

    public async Task<Doctor?> GetDoctorByIdAsync(int id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task InsertPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckMedicamentsExistanceByMedicamentsIdListAsync(List<int> medicamentIds)
    {
        return await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .CountAsync() == medicamentIds.Count;
    }

    public async Task InsertPrescriptionAsync(Prescription prescription)
    {
        await _context.Prescriptions.AddAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task InsertPrescriptionMedicamentsAsync(List<PrescriptionMedicament> prescriptionMedicaments)
    {
        await _context.PrescriptionMedicaments.AddRangeAsync(prescriptionMedicaments);
        await _context.SaveChangesAsync();
    }
}