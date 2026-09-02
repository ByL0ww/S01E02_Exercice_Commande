using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Tests
{
    public sealed class ExpediteurCuisineSimulacre : IExpediteurCuisine
    {
        private readonly string m_numeroAttendu;
        private readonly int m_nombreArticlesAttendu;
        private readonly decimal m_montantAttendu;
        private int m_nombreAppelsAttendu;

        private int m_nombreAppelsReel;

        public ExpediteurCuisineSimulacre(
        string numeroAttendu,
        int nombreArticlesAttendu,
        decimal montantAttendu,
        int nombreAppelsAttendu)
        {
            if (string.IsNullOrWhiteSpace(numeroAttendu))
            {
                throw new ArgumentException(
                    "Le numéro attendu est obligatoire.",
                    nameof(numeroAttendu));
            }

            if (nombreArticlesAttendu <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(nombreArticlesAttendu));
            }

            if (montantAttendu <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(montantAttendu));
            }

            m_numeroAttendu = numeroAttendu;
            m_nombreArticlesAttendu = nombreArticlesAttendu;
            m_montantAttendu = montantAttendu;

            m_nombreAppelsAttendu = nombreAppelsAttendu;
        }

        public void Envoyer(string numeroCommande, int nombreArticles, decimal montantTotal)
        {
            m_nombreAppelsReel++;

            Assert.Equal(m_numeroAttendu, numeroCommande);
            Assert.Equal(m_nombreArticlesAttendu, nombreArticles);
            Assert.Equal(m_montantAttendu, montantTotal);
        }

        public void VerifierAttentes()
        {
            Assert.Equal(m_nombreAppelsAttendu, m_nombreAppelsReel);
        }
    }
}
