using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APBD_zad9.Models;

public class PrescriptionMedicament
{
    [Key] [Column(Order = 1)] [ForeignKey(nameof(Medicament))]
    public int IdMedicament { get; set; }

    public Medicament Medicament { get; set; }

    [Key] [Column(Order = 2)] [ForeignKey(nameof(Prescription))]
    public int IdPrescription { get; set; }

    public Prescription Prescription { get; set; }

    public int Dose { get; set; }

    [MaxLength(100)] [Required]
    public string Details { get; set; }
}