using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests
{
    public sealed class Commandetest()
    {
        [Fact]
        public void simulacre_ServiceTransmission_Valide()
        {
            //arranger
            ExpediteurCuisineSimulacre mock = new ExpediteurCuisineSimulacre("C-1042", 3, 35.00m, 1);

            LigneCommande ligne1 = new LigneCommande("Poutine", "Saucisse", 14.50m, 2, 0.00m);
            LigneCommande ligne2 = new LigneCommande("Soupe", "Onion", 6.00m, 1, 0.00m);

            Commande commande = new Commande("C-1042");
            commande.AjouterLigne(ligne1);
            commande.AjouterLigne(ligne2);

            ServiceTransmissionCuisine transmission = new ServiceTransmissionCuisine(mock);

            //agir
            transmission.Transmettre(commande);

            //auditer
            mock.VerifierAttentes();
        }

        [Fact]
        public void simulacre_CommandeVide_Exception()
        {
            //arranger
            ExpediteurCuisineSimulacre mock = new ExpediteurCuisineSimulacre("C-1042", 1, 6.00m, 0);

            Commande commande = new Commande("C-1042");

            ServiceTransmissionCuisine transmission = new ServiceTransmissionCuisine(mock);

            Action transmettre = () => transmission.Transmettre(commande);

            //agir et auditer
            Assert.Throws<InvalidOperationException>(transmettre);

            mock.VerifierAttentes();
        }

        [Fact]
        public void simulacre_CommandeNull_Exception()
        {
            //arranger
            ExpediteurCuisineSimulacre mock = new ExpediteurCuisineSimulacre("C-1042", 1, 6.00m, 0);

            //Commande commande = new Commande("C-1042");

            ServiceTransmissionCuisine transmission = new ServiceTransmissionCuisine(mock);

            Action transmettre = () => transmission.Transmettre(null);

            //agir et auditer
            Assert.Throws<ArgumentNullException>(transmettre);

            mock.VerifierAttentes();
        }
    }
}

