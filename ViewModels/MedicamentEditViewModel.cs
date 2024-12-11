using System;
using System.ComponentModel.DataAnnotations;
using ASPBookProject.Models;

namespace ASPBookProject.ViewModels;

public class MedicamentEditViewModel
{
    [Required]
    public Medicament? Medicament { get; set; } // Le médicaments lui même
    public List<Antecedent>? Antecedents { get; set; } //La liste de tous les antécédents disponibles
    public List<Allergie>? Allergies { get; set; } //La liste de toutes les allergies disponibles

    // Les Id sélectionnés dans les listes déroulantes
    public List<int> SelectedAntecedentIds { get; set; } = new List<int>();
    public List<int> SelectedAllergieIds { get; set; } = new List<int>();
}
