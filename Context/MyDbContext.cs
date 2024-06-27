using APBD_zad9.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_zad9.Context;

public class MyDbContext : DbContext
{
    protected MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
    }
}