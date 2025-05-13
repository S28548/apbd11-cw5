using ClinicApi.DTOs;
using ClinicApi.Models;
using ClinicApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IDbService _dbService;
    public PrescriptionsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePrescription(CreatePrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            return BadRequest("Prescription cannot contain more than 10 medicaments.");

        if (request.DueDate < request.Date)
            return BadRequest("Due date must be greater than or equal to start date.");
        
        var doctor = await _dbService.GetDoctorByIdAsync(request.Doctor.IdDoctor);
        if (doctor == null)
        {
            return BadRequest("Doctor not found.");
        }

        var patient = await _dbService.GetPatientByIdAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                Birthdate = request.Patient.Birthdate
            };
            await _dbService.InsertPatientAsync(patient);
        }

        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).ToList();
        bool allExist = await _dbService.CheckMedicamentsExistanceByMedicamentsIdListAsync(medicamentIds);
        
        if (!allExist)
        {
            return BadRequest("One or more medicaments do not exist.");
        }
        
        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdPatient = patient.IdPatient,
            IdDoctor = doctor.IdDoctor,
        };

        await _dbService.InsertPrescriptionAsync(prescription);

        List<PrescriptionMedicament> prescriptionMedicaments = new List<PrescriptionMedicament>();
        foreach (var med in request.Medicaments)
        {
            prescriptionMedicaments.Add(new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = med.IdMedicament,
                Dose = med.Dose,
                Details = med.Description
            });
        }
        
        await _dbService.InsertPrescriptionMedicamentsAsync(prescriptionMedicaments);

        return CreatedAtAction(nameof(CreatePrescription), new { id = prescription.IdPrescription }, prescription);
    }
}