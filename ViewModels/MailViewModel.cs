using System;
using System.ComponentModel.DataAnnotations;

namespace ASPBookProject.ViewModels;

public class MailViewModel
{
    [Required(ErrorMessage ="Le nom est requis")]
    public string Nom { get; set; }

    [Required(ErrorMessage ="Le prénom est requis")]
    public string Prenom { get; set; }

    [StringLength(40)]
    [Required(ErrorMessage = "Le champ email est requis.")]
    [DataType(DataType.EmailAddress)]
    public string email { get; set; }

    [StringLength(10, MinimumLength = 10, ErrorMessage = "Le téléphone doit avoir 10 de longs")]
    [Required(ErrorMessage = "Le champ téléphone est requis.")]
    [RegularExpression(@"^[0-9]+$", ErrorMessage = "Le télephone doit être dans le format")]
    [DataType(DataType.PhoneNumber)]
    public String telephone { get; set; }

    [StringLength(200)]
    [Required(ErrorMessage = "Le champ message est requis.")]
    public string message {get;set;}
}