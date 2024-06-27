using APBD_zad9.Context;
using APBD_zad9.DTOs;
using APBD_zad9.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APBD_zad9.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly MyDbContext _dbContext;

    public PrescriptionsController(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription(PrescriptionRequestDto prescriptionRequestDto)
    {
        if (prescriptionRequestDto.Medicaments.Count > 10) return BadRequest();
        if (prescriptionRequestDto.DueDate >= prescriptionRequestDto.Date) return BadRequest();
        var patient = await _dbContext.Patients.FindAsync(prescriptionRequestDto.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = prescriptionRequestDto.Patient.FirstName,
                LastName = prescriptionRequestDto.Patient.LastName,
                Birthdate = prescriptionRequestDto.Patient.Birthdate
            };
            await _dbContext.Patients.AddAsync(patient);
        }

        foreach (var medicamentId in prescriptionRequestDto.Medicaments.Select(m => m.IdMedicament).ToList())
            if (!await _dbContext.Medicaments.AnyAsync(m => m.IdMedicament == medicamentId))
                return BadRequest();

        var prescription = new Prescription
        {
            Date = prescriptionRequestDto.Date,
            DueDate = prescriptionRequestDto.DueDate,
            IdDoctor = prescriptionRequestDto.IdDoctor,
            IdPatient = patient.IdPatient
        };

        await _dbContext.Prescriptions.AddAsync(prescription);
        await _dbContext.SaveChangesAsync();

        foreach (var prescriptionMedicament in prescriptionRequestDto.Medicaments.Select(medicament =>
                     new PrescriptionMedicament
                     {
                         IdPrescription = prescription.IdPrescription,
                         IdMedicament = medicament.IdMedicament,
                         Dose = medicament.Dose,
                         Details = medicament.Description
                     }))
            await _dbContext.PrescriptionMedicaments.AddAsync(prescriptionMedicament);

        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}