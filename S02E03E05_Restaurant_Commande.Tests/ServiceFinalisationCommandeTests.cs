using System;
using System.Collections.Generic;
using System.Text;
using Moq;

namespace Restaurant.Tests
{
    public sealed class ServiceFinalisationCommandeTests
    {

        [Fact]
        public void Finaliser_CommandeValide_CommandeConfirmee()
        {
            //Arranger
            //On initie les Mocks.
            var mockDisponibilitePlats = new Mock<IDisponibilitePlats> ();
            var mockPaiementPasserelle = new Mock<IPasserellePaiement>();
            var mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //On dit quoi faire aux Mocks dans une situation précise.
            mockDisponibilitePlats.Setup(d => d.EstDisponible("POU-01", 2)).Returns(true);
            mockDisponibilitePlats.Setup(d => d.EstDisponible("SOU-02", 1)).Returns(true);
            mockPaiementPasserelle.Setup(d => d.Autoriser("C-1042", 30.00m)).Returns(true);
            //Nom du Mock touché   On le set    Quand on appelle ça         retourne vrai

            //On Fait une nouvlle commande
            var commande = new Commande("C-1042");
            commande.AjouterLigne(new LigneCommande("POU-01", "Poutine", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-02", "Soupe", 6.00m, 1, 0m));

            //On fait passer les Mocks pour des objets.
            var service = new ServiceFinalisationCommande(
                mockDisponibilitePlats.Object,
                mockPaiementPasserelle.Object,
                mockExpediteurCuisine.Object);

            //Agir
            ResultatFinalisationCommande resultat = service.Finaliser(commande);

            //Auditer
            Assert.Equal(ResultatFinalisationCommande.CommandeConfirmee, resultat);

            //On vérifie le nombre d'appel pour chaque Mock                  Seulement 1 Appel
            mockDisponibilitePlats.Verify(d => d.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlats.Verify(d => d.EstDisponible("SOU-02", 1), Times.Once);
            mockPaiementPasserelle.Verify(p => p.Autoriser("C-1042", 30.00m), Times.Once);
            mockExpediteurCuisine.Verify(e => e.Envoyer("C-1042", 3, 30.00m), Times.Once);

            mockDisponibilitePlats.VerifyNoOtherCalls();
            mockPaiementPasserelle.VerifyNoOtherCalls();
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_CommandeInvalide_CommandeNonConfirmee()
        {
            //Arranger
            //On initie les Mocks.
            var mockDisponibilitePlats = new Mock<IDisponibilitePlats>();
            var mockPaiementPasserelle = new Mock<IPasserellePaiement>();
            var mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //On dit quoi faire aux Mocks dans une situation précise.
            mockDisponibilitePlats.Setup(d => d.EstDisponible("POU-01", 2)).Returns(false);
            mockDisponibilitePlats.Setup(d => d.EstDisponible("SOU-02", 1)).Returns(true);
            mockPaiementPasserelle.Setup(d => d.Autoriser("C-1042", 30.00m)).Returns(false);
            //Nom du Mock touché   On le set    Quand on appelle ça         retourne false

            //On Fait une nouvlle commande
            var commande = new Commande("C-1042");
            commande.AjouterLigne(new LigneCommande("POU-01", "Poutine", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-02", "Soupe", 6.00m, 1, 0m));

            //On fait passer les Mocks pour des objets.
            var service = new ServiceFinalisationCommande(
                mockDisponibilitePlats.Object,
                mockPaiementPasserelle.Object,
                mockExpediteurCuisine.Object);

            //Agir
            ResultatFinalisationCommande resultat = service.Finaliser(commande);

            //Auditer
            Assert.Equal(ResultatFinalisationCommande.PlatIndisponible, resultat);

            //On vérifie le nombre d'appel pour chaque Mock                  Seulement 1 Appel
            mockDisponibilitePlats.Verify(d => d.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlats.Verify(d => d.EstDisponible("SOU-02", 1), Times.Never);
            mockPaiementPasserelle.Verify(p => p.Autoriser("C-1042", 30.00m), Times.Never);
            mockExpediteurCuisine.Verify(e => e.Envoyer("C-1042", 3, 30.00m), Times.Never);

            mockDisponibilitePlats.VerifyNoOtherCalls();
            mockPaiementPasserelle.VerifyNoOtherCalls();
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_PaiementInvalide_CommandeNonConfirmee()
        {
            //Arranger
            //On initie les Mocks.
            var mockDisponibilitePlats = new Mock<IDisponibilitePlats>();
            var mockPaiementPasserelle = new Mock<IPasserellePaiement>();
            var mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //On dit quoi faire aux Mocks dans une situation précise.
            mockDisponibilitePlats.Setup(d => d.EstDisponible("POU-01", 2)).Returns(true);
            mockDisponibilitePlats.Setup(d => d.EstDisponible("SOU-02", 1)).Returns(true);
            mockPaiementPasserelle.Setup(d => d.Autoriser("C-1042", 30.00m)).Returns(false);
            //Nom du Mock touché   On le set    Quand on appelle ça         retourne false

            //On Fait une nouvelle commande
            var commande = new Commande("C-1042");
            commande.AjouterLigne(new LigneCommande("POU-01", "Poutine", 12.00m, 2, 0m));
            commande.AjouterLigne(new LigneCommande("SOU-02", "Soupe", 6.00m, 1, 0m));

            //On fait passer les Mocks pour des objets.
            var service = new ServiceFinalisationCommande(
                mockDisponibilitePlats.Object,
                mockPaiementPasserelle.Object,
                mockExpediteurCuisine.Object);

            //Agir
            ResultatFinalisationCommande resultat = service.Finaliser(commande);

            //Auditer
            Assert.Equal(ResultatFinalisationCommande.PaiementRefuse, resultat);

            //On vérifie le nombre d'appel pour chaque Mock                  Seulement 1 Appel
            mockDisponibilitePlats.Verify(d => d.EstDisponible("POU-01", 2), Times.Once);
            mockDisponibilitePlats.Verify(d => d.EstDisponible("SOU-02", 1), Times.Once);
            mockPaiementPasserelle.Verify(p => p.Autoriser("C-1042", It.Is<decimal>(montant => montant == 30.00m)), Times.Once);
            mockExpediteurCuisine.Verify(e => e.Envoyer("C-1042", 3, 30.00m), Times.Never);

            mockDisponibilitePlats.VerifyNoOtherCalls();
            mockPaiementPasserelle.VerifyNoOtherCalls();
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }

        [Fact]
        public void Finaliser_CommandeVide_CommandeNonConfirmee()
        {
            //Arranger
            //On initie les Mocks.
            var mockDisponibilitePlats = new Mock<IDisponibilitePlats>();
            var mockPaiementPasserelle = new Mock<IPasserellePaiement>();
            var mockExpediteurCuisine = new Mock<IExpediteurCuisine>();

            //On dit quoi faire aux Mocks dans une situation précise.
            mockDisponibilitePlats.Setup(d => d.EstDisponible("POU-01", 2)).Returns(true);
            mockDisponibilitePlats.Setup(d => d.EstDisponible("SOU-02", 1)).Returns(true);
            mockPaiementPasserelle.Setup(d => d.Autoriser("C-1042", 30.00m)).Returns(true);
            //Nom du Mock touché   On le set    Quand on appelle ça         retourne false

            //On Fait une nouvlle commande
            var commande = new Commande("C-1042");

            //On fait passer les Mocks pour des objets.
            var service = new ServiceFinalisationCommande(
                mockDisponibilitePlats.Object,
                mockPaiementPasserelle.Object,
                mockExpediteurCuisine.Object);

            //Agir et Auditer
            Assert.Throws<InvalidOperationException>(() => service.Finaliser(commande));

            //On vérifie le nombre d'appel pour chaque Mock                  Seulement 1 Appel
            mockDisponibilitePlats.Verify(d => d.EstDisponible("POU-01", 2), Times.Never);
            mockDisponibilitePlats.Verify(d => d.EstDisponible("SOU-02", 1), Times.Never);
            mockPaiementPasserelle.Verify(p => p.Autoriser("C-1042", 30.00m), Times.Never);
            mockExpediteurCuisine.Verify(e => e.Envoyer("C-1042", 3, 30.00m), Times.Never);

            mockDisponibilitePlats.VerifyNoOtherCalls();
            mockPaiementPasserelle.VerifyNoOtherCalls();
            mockExpediteurCuisine.VerifyNoOtherCalls();
        }
    }
}
