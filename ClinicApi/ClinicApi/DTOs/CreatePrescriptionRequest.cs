namespace ClinicApi.DTOs;

public class CreatePrescriptionRequest
{
    public PatientDto Patient { get; set; }
    public DoctorDto Doctor { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateOnly Date { get; set; }
    public DateOnly DueDate { get; set; }
}
