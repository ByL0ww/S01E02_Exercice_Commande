using Restaurant;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests;

    public sealed class UnitTest
    {
        [Fact]
        public void CalculerTotal_SansRabais_RetournePrixExact()
        {
            //Arranger
            decimal totalAttendu = 29.00m;

            //Nécessaire à la création de LigneCommande
            decimal rabais = 0.00m;
            int quantite = 2;
            decimal prixUnitaire = 14.50m;
            string description = "poutine saucisse";
            string codePlat = "1234";

            LigneCommande test = new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

            //Agir
            decimal total = test.CalculerTotal();

            //Auditer
            Assert.Equal(totalAttendu, total);
        }

        [Theory]
        [InlineData(10.00, 1, 0.00, 10.00)]
        [InlineData(12.50, 2, 10.00, 22.50)]
        [InlineData(3.25, 4, 20.00, 10.40)]
        [InlineData(10.00, 2, 100.00, 0.00)]
        public void Total_PrixDifferent_CalculPrix(
            double prix,
            int quantite,
            double rabais,
            double total)
        {
        //arranger
        LigneCommande test = new LigneCommande(
        "Plat","platTest", (decimal)prix, quantite, (decimal)rabais);

        //agir
        decimal totalReel = test.CalculerTotal();

        //auditer
        Assert.Equal((decimal)total, totalReel);
        }

    [Fact]
    public void Ctor_PlatVide_Exception()
    {
        //arranger
        decimal rabais = 0.00m;
        int quantite = 2;
        decimal prixUnitaire = 14.50m;
        string description = "poutine saucisse";
        string codePlat = "";

        Action test = () => new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

        //agir
        ArgumentException exception = Assert.Throws<ArgumentException>(test);

        //auditer
        Assert.Equal("codePlat", exception.ParamName);
    }

    [Fact]
    public void Ctor_DescriptionVide_Exception()
    {
        //arranger
        decimal rabais = 0.00m;
        int quantite = 2;
        decimal prixUnitaire = 14.50m;
        string description = "";
        string codePlat = "1234";

        Action test = () => new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

        //agir
        ArgumentException exception = Assert.Throws<ArgumentException>(test);

        //auditer
        Assert.Equal("description", exception.ParamName);
    }

    [Fact]
    public void Ctor_PrixUnitaireNegatif_Exception()
    {
        //arranger
        decimal rabais = 0.00m;
        int quantite = 2;
        decimal prixUnitaire = -14.50m;
        string description = "poutine saucisse";
        string codePlat = "1234";

        Action test = () => new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

        //agir
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(test);

        //auditer
        Assert.Equal("prixUnitaire", exception.ParamName);
    }

    [Fact]
    public void Ctor_QuantiteEgalAZero_Exception()
    {
        //arranger
        decimal rabais = 0.00m;
        int quantite = 0;
        decimal prixUnitaire = 14.50m;
        string description = "poutine saucisse";
        string codePlat = "1234";

        Action test = () => new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

        //agir
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(test);

        //auditer
        Assert.Equal("quantite", exception.ParamName);
    }

    [Fact]
    public void Ctor_RabaisSousZero_Exception()
    {
        //arranger
        decimal rabais = -1.00m;
        int quantite = 2;
        decimal prixUnitaire = 14.50m;
        string description = "poutine saucisse";
        string codePlat = "1234";

        Action test = () => new LigneCommande(codePlat, description, prixUnitaire, quantite, rabais);

        //agir
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(test);

        //auditer
        Assert.Equal("pourcentageRabais", exception.ParamName);
    }
}

