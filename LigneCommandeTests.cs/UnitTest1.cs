using Restaurant;

namespace LigneCommandeTests.cs
{
    public class UnitTest1
    {
        [Fact]
        public void CalculerTotal_SansRabais_RetournePrixExact()
        {
            //Arranger
            decimal totalAttendu = 29.00m;
            decimal rabais = 0.00m;
            int quantite = 2;
            decimal prixUnitaire = 14.50m;
            string description = "poutine saucisse";
            string codePlat = "1234";

            LigneCommande test = new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

            //Agir


            //Auditer
            Assert.Equal(total, );

        }
    }
}
