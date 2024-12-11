using System;
using System.ComponentModel.DataAnnotations;

namespace ASPBookProject.Models;
public enum Sexe { Masculin, Féminin }

public class Patient
{
    [Key]
    public int PatientId { get; set; }
    [Required(ErrorMessage = "Le nom est requis")]
    public string Nom_p { get; set; }
    [Required(ErrorMessage = "Le prénom est requis")]
    public required string Prenom_p { get; set; }
    [Required(ErrorMessage = "Le sexe est requis")]
    public required string Sexe_p { get; set; }
    [RegularExpression(@"^\d{7}$", ErrorMessage = "Le numéro de sécurité dois posseder 7 chiffres")]
    [Required(ErrorMessage = "Le Numéro de sécurité est requis")]
    public required string Num_secu { get; set; }

    public List<Antecedent> Antecedents { get; set; } = new();
    public List<Allergie> Allergies { get; set; } = new();
    public List<Ordonnance>? Ordonnances { get; set; }

}
