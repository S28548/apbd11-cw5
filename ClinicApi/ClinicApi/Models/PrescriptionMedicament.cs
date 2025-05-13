using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClinicApi.Models;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [Required]
    public int IdMedicament { get; set; }
    
    [Required]
    public int IdPrescription { get; set; }
    
    public int? Dose { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Details { get; set; }
    
    
    [ForeignKey(nameof(IdPrescription))]
    public Prescription Prescription { get; set; }
    
    [ForeignKey(nameof(IdMedicament))]
    public Medicament Medicament { get; set; }
}