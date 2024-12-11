using System;
using System.ComponentModel.DataAnnotations;

namespace ASPBookProject.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Un mot de passe est requis")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}