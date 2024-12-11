using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPBookProject.Models
{
    public class AddOrdonnanceViewModel
    {
        public int PatientId { get; set; }
        public string NomPatient { get; set; } = string.Empty;

        [Required]
        public string Posologie { get; set; } = string.Empty;

        [Required]
        public string Duree_traitement { get; set; } = string.Empty;

        [Required]
        public string Instructions_specifique { get; set; } = string.Empty;

        public List<Antecedent> Antecedents { get; set; } = new();
        public List<Allergie> Allergies { get; set; } = new();
        public List<Medicament> Medicaments { get; set; } = new();  

        public List<int>? SelectedAntecedentIds { get; set; }
        public List<int>? SelectedAllergieIds { get; set; }
        public List<int>? SelectedMedicamentIds { get; set; }  
    }
}
