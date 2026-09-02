using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Restaurant.Tests
{
    public sealed class CommandeTest
    {
        [Fact]
        public void EstVide_CommandeVide_Valide()
        {
            //arranger
            Commande test = new Commande("1");

            //agir
            bool commandeEstVide = test.EstVide;
            decimal sousTotal = test.SousTotal;
            int nbArticles = test.NombreArticles;

            //auditer
            Assert.True(commandeEstVide);
            Assert.Equal(0.00m, sousTotal);
            Assert.Equal(0, nbArticles);
        }

        [Fact]
        public void Ctor_NumeroCommandeVide_Exception()
        {
            //arranger
            string numeroCommande = "";

            Action test = () => new Commande(numeroCommande);

            //agir
            ArgumentException exception = Assert.Throws<ArgumentException>(test);

            //auditer
            Assert.Equal("numero", exception.ParamName);
        }

        [Fact]
        public void SousTotalEtNbArticle_CommandeScenario_Valide()
        {
            //arranger
            LigneCommande test1 = new LigneCommande("Poutine", "Poutine Saucisse", 14.50m, 2, 10.00m);
            LigneCommande test2 = new LigneCommande("Soupe", "Soupe Onion", 6.00m, 1, 0.00m);

            Commande testCommande = new Commande("1");
            testCommande.AjouterLigne(test1);
            testCommande.AjouterLigne(test2);

            //agir
            decimal sousTotal1 = test1.CalculerTotal();
            decimal sousTotal2 = test2.CalculerTotal();

            int nbArticles = testCommande.NombreArticles;

            decimal grandTotal = sousTotal1 + sousTotal2;

            //auditer
            Assert.Equal(32.10m, grandTotal);
            Assert.Equal(3, nbArticles);
        }

        [Fact]
        public void AjouterLigne_LigneNull_Exception()
        {
            //arranger
            Commande testCommande = new Commande("1");
            Action action = () => testCommande.AjouterLigne(null);

            //agir

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(action);


            //auditer
            Assert.Equal("ligne", exception.ParamName);
        }
    }
}
