using System;
using System.Collections.Generic;

using AntMe.Deutsch;

/**
 *
 *  @package   antme
 *  @file      vorlage_dotnetbs2_antme16.cs
 *  @brief     Implementierung der AntMe! Datei "Vorlage.cs"
 *  @author    Rolf Hemmerling <hemmerling@gmx.net>
 *  @version   3.00, 
 *             Programmiersprache C#,
 *             Entwicklungswerkzeug "Microsoft Visual Studio 2013 Express"
 *  @date      2015-01-01
 *  @copyright AntMe! Community License
 *
 *  vorlage_dotnetbs2_antme16.cs - Implementierung der AntMe! 
 *                                 Datei "Vorlage.cs"
 *                                 fuer AntMe! 1.6 / 1.7
 *
 *  Copyright 2013-2015 Rolf Hemmerling
 *
 *  Licensed under the AntMe! Community License;
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *  http://www.antme.net/
 *
 *  Haupt-Entwicklungszeitraum am: 2013-12-16
 *
 */

// Füge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.WolfgangGallo".
namespace AntMe.Spieler.DotNetBS2
{

	// Das Spieler-Attribut erlaubt das Festlegen des Volk-Names und von Vor-
	// und Nachname des Spielers. Der Volk-Name muß zugewiesen werden, sonst wird
	// das Volk nicht gefunden.
	[Spieler(
		Volkname = "DotNetBS2 Ant",
		Vorname = "DotNetBS2",
		Nachname = "Ameise"
	)]

	// Das Typ-Attribut erlaubt das Ändern der Ameisen-Eigenschaften. Um den Typ
	// zu aktivieren muß ein Name zugewiesen und dieser Name in der Methode 
	// BestimmeTyp zurückgegeben werden. Das Attribut kann kopiert und mit
	// verschiedenen Namen mehrfach verwendet werden.
	// Eine genauere Beschreibung gibts in Lektion 6 des Ameisen-Tutorials.
    [Kaste(
        Name = "Standard",
        GeschwindigkeitModifikator = 0,
        DrehgeschwindigkeitModifikator = 0,
        LastModifikator = 0,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = 0,
        EnergieModifikator = 0,
        AngriffModifikator = 0
    )]

	[Kaste(
		Name = "Sammler",
		GeschwindigkeitModifikator = 0,
		DrehgeschwindigkeitModifikator = 0,
		LastModifikator = 2,
		ReichweiteModifikator = 0,
		SichtweiteModifikator = 0,
		EnergieModifikator = -1,
		AngriffModifikator = -1
	)]

    [Kaste(
        Name = "Waechter",
        GeschwindigkeitModifikator = 1,
        DrehgeschwindigkeitModifikator = 0,
        LastModifikator = -1,
        ReichweiteModifikator = -1,
        SichtweiteModifikator = -1,
        EnergieModifikator = 1,
        AngriffModifikator = 1
    )]

    [Kaste(
        Name = "Scout",
        GeschwindigkeitModifikator = 2,
        DrehgeschwindigkeitModifikator = -1,
        LastModifikator = -1,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = 2,
        EnergieModifikator = -1,
        AngriffModifikator = -1
    )]

	public class MeineAmeise : Basisameise
    {
        #region Flags
        private bool _nichtZumZuckerGehen;
        #endregion


        #region Kaste

        /// <summary>
		/// Bestimmt die Kaste einer neuen Ameise.
		/// </summary>
		/// <param name="anzahl">Die Anzahl der von jeder Kaste bereits vorhandenen
		/// Ameisen.</param>
		/// <returns>Der Name der Kaste der Ameise.</returns>
		public override string BestimmeKaste(Dictionary<string, int> anzahl)
		{
            //return "Scout";

            var count = anzahl["Scout"] + anzahl["Sammler"];

            if (count % 3 == 0)
            {
                return "Scout";
            }
            else
            {
                return "Sammler";
            }


			//return "Sammler";
            //if (anzahl["Sammler"] < 75)
            //{
            //    return "Sammler";
            //}
            //else
            //{
            //    return "Scout";
            //}
		}

		#endregion

		#region Fortbewegung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
		/// hingehen soll.
		/// </summary>
		public override void Wartet()
		{
            GeheGeradeaus();
		}

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen
		/// Reichweite überschritten hat.
		/// </summary>
		public override void WirdMüde()
		{
            GeheZuBau();
		}

		#endregion

		#region Nahrung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen
		/// Zuckerhaufen sieht.
		/// </summary>
		/// <param name="zucker">Der nächstgelegene Zuckerhaufen.</param>
		public override void Sieht(Zucker zucker)
		{
            //SprüheMarkierung(0,100);
            
            if (AktuelleLast == 0 && !_nichtZumZuckerGehen)
            {
                GeheZuZiel(zucker);
            }            
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens ein
		/// Obststück sieht.
		/// </summary>
		/// <param name="obst">Das nächstgelegene Obststück.</param>
		public override void Sieht(Obst obst)
		{
            if (Kaste == "Scout")
            {
                SprüheMarkierung(Richtung, 300);
                GeheWegVon(obst, 50);
            }
            else
            {
                SprüheMarkierung(Richtung, 150);
            }
            if (Kaste == "Sammler" && AktuelleLast == 0)
            {
                GeheZuZiel(obst);
            }
        }

		/// <summary>
        /// 
		/// Wird einmal aufgerufen, wenn die Ameise einen Zuckerhaufen als Ziel
		/// hat und bei diesem ankommt.
		/// </summary>
		/// <param name="zucker">Der Zuckerhaufen.</param>
		public override void ZielErreicht(Zucker zucker)
		{
            if (Kaste == "Sammler")
            {
                Nimm(zucker);
            }
            else if (Kaste == "Scout")
            {
                _nichtZumZuckerGehen = true;
            }
            SprüheMarkierung(Richtung, 200);
            GeheZuBau();
		}

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Obststück als Ziel hat und
		/// bei diesem ankommt.
		/// </summary>
		/// <param name="obst">Das Obstück.</param>
		public override void ZielErreicht(Obst obst)
		{
            if (Kaste == "Sammler")
            {
                Nimm(obst);
                GeheZuBau();
            }
        }

		#endregion

		#region Kommunikation

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise eine Markierung des selben
		/// Volkes riecht. Einmal gerochene Markierungen werden nicht erneut
		/// gerochen.
		/// </summary>
		/// <param name="markierung">Die nächste neue Markierung.</param>
		public override void RiechtFreund(Markierung markierung)
		{
            // GeheZuZiel(markierung);
            if (Kaste == "Scout")
            {
                return;
            }

            if (Kaste == "Waechter" && markierung.Information == 0)
            {
               GeheZuZiel(markierung);
               return;
            }
            if (Ziel == null)
            {
                DreheInRichtung(markierung.Information);
                GeheGeradeaus();
            }             
        }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des
		/// selben Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die nächstgelegene befreundete Ameise.</param>
		public override void SiehtFreund(Ameise ameise)
		{
		}

		/// <summary>
		/// Wird aufgerufen, wenn die Ameise eine befreundete Ameise eines anderen Teams trifft.
		/// </summary>
		/// <param name="ameise"></param>
		public override void SiehtVerbündeten(Ameise ameise)
		{
		}

		#endregion

		#region Kampf

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Wanze
		/// sieht.
		/// </summary>
		/// <param name="wanze">Die nächstgelegene Wanze.</param>
		public override void SiehtFeind(Wanze wanze)
		{
            // SprüheMarkierung(0, 200);
            if (Kaste == "Waechter")
            {
                SprüheMarkierung(0, 200);
                if (AnzahlFremderAmeisenInSichtweite * MaximaleEnergie > 
                    wanze.AktuelleEnergie)
                {
                    GreifeAn(wanze);
                }

            }

        }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines
		/// anderen Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die nächstgelegen feindliche Ameise.</param>
		public override void SiehtFeind(Ameise ameise)
		{
		}

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise von einer Wanze angegriffen
		/// wird.
		/// </summary>
		/// <param name="wanze">Die angreifende Wanze.</param>
		public override void WirdAngegriffen(Wanze wanze)
		{
            //GeheWegVon(wanze);
            GeheZuBau();

            //if (Kaste == "Sammler" || Kaste == "Scout") 
            // {
            //    LasseNahrungFallen();
            //    GeheWegVon(wanze);
            // }
            //else if (Kaste == "Waechter")
            //{
            //    if (AnzahlFremderAmeisenInSichtweite * MaximaleEnergie > 
            //        wanze.AktuelleEnergie)
            //    {
            //        GreifeAn(wanze);
            //    }
            //}
		}

		/// <summary>
		/// Wird wiederholt aufgerufen in der die Ameise von einer Ameise eines
		/// anderen Volkes Ameise angegriffen wird.
		/// </summary>
		/// <param name="ameise">Die angreifende feindliche Ameise.</param>
		public override void WirdAngegriffen(Ameise ameise)
		{
		}

		#endregion

		#region Sonstiges

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise gestorben ist.
		/// </summary>
		/// <param name="todesart">Die Todesart der Ameise</param>
		public override void IstGestorben(Todesart todesart)
		{
		}

		/// <summary>
		/// Wird unabhängig von äußeren Umständen in jeder Runde aufgerufen.
		/// </summary>
		public override void Tick()
		{
            if (Ziel is Bau)
            {
                if (_nichtZumZuckerGehen || AktuelleLast > 0)
                {
                    SprüheMarkierung(Richtung + 180);
                }
                //if(_nichtZumZuckerGehen || (AktuelleLast > 0 && GetragenesObst == null))
                //{
                //    SprüheMarkierung(Richtung + 180);
                //}
                //else if(AktuelleLast > 0 && GetragenesObst != null)
                //{
                //    SprüheMarkierung(Richtung, 200);
                //}
            }
            else
            {
                _nichtZumZuckerGehen = false;
            }
        }

		#endregion
		 
	}
}