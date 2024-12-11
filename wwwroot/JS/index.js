// barre de recherche créée lors du premier ap
document.addEventListener("DOMContentLoaded", () => {
    const input = document.querySelector("#search");
    const rows = document.querySelectorAll("tbody tr");

    input.addEventListener("input", (e) => {
        let content = e.target.value.toLowerCase();

        rows.forEach((row) => {
            let nomColonne = row.querySelector("td:first-child"); // Cible la première colonne
            let prenomColonne = row.querySelector("td:nth-child(2)");
            let sexeColonne = row.querySelector("td:nth-child(3)");
            let numSecuColonne = row.querySelector("td:nth-child(4)");

            // Vérifie si l'une des colonnes contient le texte recherché
            if (
                (nomColonne && nomColonne.textContent.toLowerCase().includes(content)) ||
                (prenomColonne && prenomColonne.textContent.toLowerCase().includes(content)) ||
                (sexeColonne && sexeColonne.textContent.toLowerCase().includes(content)) ||
                (numSecuColonne && numSecuColonne.textContent.toLowerCase().includes(content))
            ) {
                row.style.display = ""; // Affiche la ligne si correspondance
            } else {
                row.style.display = "none"; // Masque la ligne si aucune correspondance
            }
        });
    });
});
