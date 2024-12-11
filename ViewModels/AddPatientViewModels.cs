using System;
using System.ComponentModel.DataAnnotations;
using ASPBookProject.Models;

namespace ASPBookProject.ViewModels;

public enum TypeSexe { Masculin, Féminin }

public class AddPatientViewModels
{
    public int PatientId { get; set; }
    [Required]
    public string Nom_p { get; set; }
    [Required]
    public string Prenom_p { get; set; }
    [Required]
    public TypeSexe Sexe_p { get; set; }
    [Required]
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Le numéro de sécurité sociale doit contenir exactement 7 chiffres.")]
    public string Num_secu { get; set; }
    public List<Antecedent> Antecedents { get; set; } = new();
    public List<Allergie> Allergies { get; set; } = new();
    public List<int> SelectedAntecedentIds { get; set; } = new List<int>();
    public List<int> SelectedAllergieIds { get; set; } = new List<int>();
    public AddPatientViewModels()
    {
        Nom_p = string.Empty;
        Prenom_p = string.Empty;
        Sexe_p = TypeSexe.Masculin; 
        Num_secu = string.Empty;
    }
}
