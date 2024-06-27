using APBD_zad9.Models;

namespace APBD_zad9.DTOs;

public class PrescriptionRequestDto
{
    public Patient Patient { get; set; }
    public int IdDoctor { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}