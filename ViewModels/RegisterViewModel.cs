using System;
using System.ComponentModel.DataAnnotations;

namespace ASPBookProject.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Votre nom est requis.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Le nom d'utilisateur est requis.")]
    public string Login_m { get; set; }

    [Required(ErrorMessage = "Le role est requis.")]
    public string Role { get; set; }

    [Required(ErrorMessage = "La date est requise.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Le mot de passe est requis.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }


}
