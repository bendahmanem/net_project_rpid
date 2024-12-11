using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPBookProject.Models;

public class Ordonnance
{
    public int OrdonnanceId { get; set; }
    public required string  Posologie { get; set; }
    [DataType(DataType.Date)]
    public required DateTime Date_debut { get; set; }
    [DataType(DataType.Date)]
    public required DateTime Date_fin { get; set; }
    public string? Instructions_specifique { get; set; }
    public required string MedecinId { get; set; }
    public Medecin Medecin { get; set; }
    [Required(ErrorMessage = "La patient est requis")]
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    [Required(ErrorMessage ="Les médicaments sont obligatoires")]
    public List<Medicament> Medicaments { get; set; } = new();
}