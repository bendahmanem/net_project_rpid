using System;
using System.ComponentModel.DataAnnotations;
using ASPBookProject.Models;

namespace ASPBookProject.ViewModels;

public class OrdonnanceEditViewModel
{

    public int? OrdonnanceId { get; set; }

    [StringLength(100)]
    [Required(ErrorMessage = "La posologie est requise")]
    public string Posologie { get; set; } 


    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Le date de début est obligatoire")]
    public  DateTime Date_debut { get; set; }

    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Le date de fin est obligatoire")]
    public  DateTime Date_fin { get; set; }

    public string? Instructions_specifique { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner un patient")]
     public  int? PatientId { get; set; }

     public Patient? Patient {get;set;}

     public Medecin? Medecin {get;set;}
    public List<Patient> Patients { get; set; } = new List<Patient>();
    public List<Medicament>? Medicaments { get; set; }
    public List<int> SelectedMedicamentId { get; set; } = new List<int>();

}
