using System;
using ASPBookProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASPBookProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<Medecin>
    {
        public DbSet<Student> Roster { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Allergie> Allergies => Set<Allergie>();
        public DbSet<Ordonnance> Ordonnances => Set<Ordonnance>();
        public DbSet<Medicament> Medicaments => Set<Medicament>();
        public DbSet<Antecedent> Antecedents => Set<Antecedent>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Allergies)
                .WithMany(a => a.Patients)
                .UsingEntity(j => j.ToTable("AllergiePatient"));

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Antecedents)
                .WithMany(a => a.Patients)
                .UsingEntity(j => j.ToTable("AntecedentPatient"));

            modelBuilder.Entity<Medicament>()
                .HasMany(m => m.Antecedents)
                .WithMany(a => a.Medicaments)
                .UsingEntity(j => j.ToTable("AntecedentMedicament"));

            modelBuilder.Entity<Medicament>()
                .HasMany(m => m.Allergies)
                .WithMany(a => a.Medicaments)
                .UsingEntity(j => j.ToTable("AllergieMedicament"));

            modelBuilder.Entity<Ordonnance>()
                .HasMany(o => o.Medicaments)
                .WithMany(m => m.Ordonnances)
                .UsingEntity(j => j.ToTable("MedicamentOrdonnance"));

            modelBuilder.Entity<Ordonnance>()
                .HasOne(o => o.Patient)
                .WithMany(p => p.Ordonnances)
                .HasForeignKey(o => o.PatientId);

            modelBuilder.Entity<Ordonnance>()
                .HasOne(o => o.Medecin)
                .WithMany(m => m.Ordonnances)
                .HasForeignKey(o => o.MedecinId);

            modelBuilder.Entity<Medicament>()
                .HasMany(m => m.Allergies)
                .WithMany(a => a.Medicaments)
                .UsingEntity(j => j.ToTable("AllergieMedicament"));

            modelBuilder.Entity<Medicament>()
                .HasMany(m => m.Antecedents)
                .WithMany(a => a.Medicaments)
                .UsingEntity(j => j.ToTable("AntecedentMedicament"));

            modelBuilder.Entity<Student>().HasData(
                new Student()
                {
                    StudentId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    AdmissionDate = DateTime.Parse("1990-01-01"),
                    GPA = 3.5,
                    Major = Major.IT,
                    IsVeteran = false
                });

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    InstructorId = 1,
                    FirstName = "Jane",
                    LastName = "Doe",
                    IsTenured = true,
                    HiringDate = DateTime.Parse("2010-01-01"),
                    Rank = Ranks.AssociateProfessor
                },
                new Instructor()
                {
                    InstructorId = 2,
                    FirstName = "John",
                    LastName = "Smith",
                    IsTenured = false,
                    HiringDate = DateTime.Parse("2015-01-01"),
                    Rank = Ranks.Instructor
                },
                new Instructor()
                {
                    InstructorId = 3,
                    FirstName = "Jane",
                    LastName = "Smith",
                    IsTenured = true,
                    HiringDate = DateTime.Parse("2012-01-01"),
                    Rank = Ranks.FullProfessor
                }
            );
        }
    }
}
