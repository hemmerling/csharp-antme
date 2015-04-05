using AntMe.Deutsch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntMe.Player.Vorlage
{
    [Spieler(
        Volkname = "Vorlage",
        Vorname = "",
        Nachname = ""
    )]
    [Kaste(
        Name = "Standard",
        AngriffModifikator = 0,
        DrehgeschwindigkeitModifikator = 0,
        EnergieModifikator = 0,
        GeschwindigkeitModifikator = 0,
        LastModifikator = 0,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = 0
    )]
    public class VorlageClass : Basisameise
    {
        #region Kasten

        public override string BestimmeKaste(Dictionary<string, int> anzahl)
        {
            return "Standard";
        }

        #endregion

        #region Fortbewegung

        public override void Wartet()
        {
        }

        public override void WirdMüde()
        {
        }

        public override void IstGestorben(Todesart todesart)
        {
        }

        public override void Tick()
        {
        }

        #endregion

        #region Nahrung

        public override void Sieht(Obst obst)
        {
        }

        public override void Sieht(Zucker zucker)
        {
        }

        public override void ZielErreicht(Obst obst)
        {
        }

        public override void ZielErreicht(Zucker zucker)
        {
        }

        #endregion

        #region Kommunikation

        public override void RiechtFreund(Markierung markierung)
        {
        }

        public override void SiehtFreund(Ameise ameise)
        {
        }

        public override void SiehtVerbündeten(Ameise ameise)
        {
        }

        #endregion

        #region Kampf

        public override void SiehtFeind(Ameise ameise)
        {
        }

        public override void SiehtFeind(Wanze wanze)
        {
        }

        public override void WirdAngegriffen(Ameise ameise)
        {
        }

        public override void WirdAngegriffen(Wanze wanze)
        {
        }

        #endregion
    }
}