using ClinicApi.Models;

namespace ClinicApi.Services;

public interface IDbService
{
    public Task<Patient?> GetPatientByIdAsync(int id);
    public Task<Doctor?> GetDoctorByIdAsync(int id);
    public Task InsertPatientAsync(Patient patient);
    public Task<bool> CheckMedicamentsExistanceByMedicamentsIdListAsync(List<int> id);
    public Task InsertPrescriptionAsync(Prescription prescription);
    public Task InsertPrescriptionMedicamentsAsync(List<PrescriptionMedicament> prescriptionMedicament);
}