using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ASPBookProject.Models;

public class Medecin : IdentityUser
{
    public DateTime Date_naissance_m { get; set; }
    [StringLength(30)]
    public required string Role { get; set; }

    public List<Ordonnance> Ordonnances { get; set; } = new();
}
