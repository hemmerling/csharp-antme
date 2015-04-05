using System.Collections.Generic;

// F�ge hier hinter AntMe.Spieler einen Punkt und deinen Namen ohne Leerzeichen
// ein! Zum Beispiel "AntMe.Spieler.AntonM�ller".
namespace AntMe.Spieler.DotNetBS1
{

	// Das Spieler-Attribut erlaubt das Festlegen des Volk-Names und von Vor-
	// und Nachname des Spielers. Der Volk-Name mu� zugewiesen werden, sonst wird
	// das Volk nicht gefunden.
    [Spieler(
		Volkname = "DotNetBS1 Ant",
		Vorname = "DotNetBS1",
		Nachname = "Ameise"
    )]

    // Das Typ-Attribut erlaubt das �ndern der Ameisen-Eigenschaften. Um den Typ
    // zu aktivieren mu� ein Name zugewiesen und dieser Name in der Methode 
    // BestimmeTyp zur�ckgegeben werden. Das Attribut kann kopiert und mit
    // verschiedenen Namen mehrfach verwendet werden.
    // Eine genauere Beschreibung gibts in Lektion 6 des Ameisen-Tutorials
    [Typ(
        Name = "Obstameise",
        GeschwindigkeitModifikator = 0,
        DrehgeschwindigkeitModifikator = 0,
        LastModifikator = 2,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = -1,
        EnergieModifikator = -1,
        AngriffModifikator = 0
    )]
    [Typ(
        Name = "Zuckerameise",
        GeschwindigkeitModifikator = 2,
        DrehgeschwindigkeitModifikator = -1,
        LastModifikator = 2,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = -1,
        EnergieModifikator = -1,
        AngriffModifikator = -1
    )]
    [Typ(
        Name = "Kampfameise",
        GeschwindigkeitModifikator = 0,
        DrehgeschwindigkeitModifikator = 0,
        LastModifikator = 0,
        ReichweiteModifikator = 0,
        SichtweiteModifikator = 0,
        EnergieModifikator = 0,
        AngriffModifikator = 0
    )]

	public class HABS : Ameise
	{

		/// <summary>
		/// Der Konstruktor.
		/// </summary>
		/// <param name="volk">Das Volk zu dem die neue Ameise geh�rt.</param>
		/// <param name="anzahl">Die Anzahl der von jedem Typ bereits vorhandenen
		/// Ameisen.</param>
        public HABS(Volk volk, Dictionary<string, int> anzahl)
			: base(volk, anzahl) { }

		/// <summary>
		/// Bestimmt den Typ einer neuen Ameise.
		/// </summary>
		/// <param name="anzahl">Die Anzahl der von jedem Typ bereits vorhandenen
		/// Ameisen.</param>
		/// <returns>Der Name des Typs der Ameise.</returns>
		public override string BestimmeTyp(Dictionary<string, int> anzahl)
		{
            int AnzZA = anzahl["Zuckerameise"];
            int AnzOA = anzahl["Obstameise"];
            int AnzKA = anzahl["Kampfameise"];

            if (AnzZA < 3 * AnzKA)
            {
                if (2 * AnzZA < AnzOA)
                {
                    return "Zuckerameise";
                }
                else
                {
                    return "Obstameise";
                }
            }
            else
            {
                if (3 * AnzKA < AnzOA)
                {
                    return "Kampfameise";
                }
                else
                {
                    return "Obstameise";
                }
            }

            
		}

        public Ameisentypen Type
        {
            get { return Helper.GetType(this); }
        }

		#region Fortbewegung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn der die Ameise nicht weiss wo sie
		/// hingehen soll.
		/// </summary>
		public override void Wartet() 
        {
            DreheUmWinkel(Zufall.Zahl(-30, 30));
            GeheGeradeaus(50); 
        }

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Drittel ihrer maximalen
		/// Reichweite �berschritten hat.
		/// </summary>
		public override void WirdM�de() 
        {
            GeheZuBau();
        }

		#endregion

		#region Nahrung

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen
		/// Zuckerhaufen sieht.
		/// </summary>
		/// <param name="zucker">Der n�chstgelegene Zuckerhaufen.</param>
		public override void Sieht(Zucker zucker) 
        {
            if (AktuelleLast == 0)
            {
                GeheZuZiel(zucker);
            }
           
        }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens ein
		/// Obstst�ck sieht.
		/// </summary>
		/// <param name="obst">Das n�chstgelegene Obstst�ck.</param>
		public override void Sieht(Obst obst) 
        {
            if (AktuelleLast == 0)
            {
                Spr�heMarkierung(1);
                GeheZuZiel(obst);
            }
            //else if (AktuelleLast == 0)
            //{
            //    GeheZuZiel(obst);
            //}
        }

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise einen Zuckerhaufen als Ziel
		/// hat und bei diesem ankommt.
		/// </summary>
		/// <param name="zucker">Der Zuckerhaufen.</param>
		public override void ZielErreicht(Zucker zucker) 
        {
            Spr�heMarkierung(1);
            Nimm(zucker);
            GeheZuBau();
        }

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise ein Obstst�ck als Ziel hat und
		/// bei diesem ankommt.
		/// </summary>
		/// <param name="obst">Das Obst�ck.</param>
		public override void ZielErreicht(Obst obst) 
        {
            Spr�heMarkierung(1);
            Nimm(obst);
            GeheZuBau();
        }

		#endregion

		#region Kommunikation

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise eine Markierung des selben
		/// Volkes riecht. Einmal gerochene Markierungen werden nicht erneut
		/// gerochen.
		/// </summary>
		/// <param name="markierung">Die n�chste neue Markierung.</param>
		public override void RiechtFreund(Markierung markierung)
        {
            if (markierung.Information == 1 && AktuelleLast == 0)
            {
                this.GeheZuZiel(markierung);
            }
        }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindstens eine Ameise des
		/// selben Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die n�chstgelegene befreundete Ameise.</param>
		public override void SiehtFreund(Ameise ameise) { }

		#endregion

		#region Kampf

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens einen K�fer
		/// sieht.
		/// </summary>
		/// <param name="k�fer">Der n�chstgelegene K�fer.</param>
		public override void SiehtFeind(K�fer k�fer) { }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise mindestens eine Ameise eines
		/// anderen Volkes sieht.
		/// </summary>
		/// <param name="ameise">Die n�chstgelegen feindliche Ameise.</param>
		public override void SiehtFeind(Ameise ameise) { }

		/// <summary>
		/// Wird wiederholt aufgerufen, wenn die Ameise von einem K�fer angegriffen
		/// wird.
		/// </summary>
		/// <param name="k�fer">Der angreifende K�fer.</param>
		public override void WirdAngegriffen(K�fer k�fer) { }

		/// <summary>
		/// Wird wiederholt aufgerufen in der die Ameise von einer Ameise eines
		/// anderen Volkes Ameise angegriffen wird.
		/// </summary>
		/// <param name="ameise">Die angreifende feindliche Ameise.</param>
		public override void WirdAngegriffen(Ameise ameise) { }

		#endregion

		#region Sonstiges

		/// <summary>
		/// Wird einmal aufgerufen, wenn die Ameise gestorben ist.
		/// </summary>
		/// <param name="todesArt">Die Todesart der Ameise</param>
		public override void IstGestorben(TodesArt todesArt) { }

		/// <summary>
		/// Wird unabh�ngig von �u�eren Umst�nden in jeder Runde aufgerufen.
		/// </summary>
		public override void Tick() { }

		#endregion

	}

    public enum Ameisentypen
    {
        Kampf,
        Obst,
        Zucker
    }

    public static class Helper
    {
        public const string Obstameise = "Obstameise";
        public const string Zuckerameise = "Zuckerameise";
        public const string Kampfameise = "Kampfameise";

        public static Ameisentypen GetType(Ameise ameise)
        {
            if ((ameise as HABS).Typ == Obstameise)
                return Ameisentypen.Obst;
            else if ((ameise as HABS).Typ == Zuckerameise)
                return Ameisentypen.Zucker;
            else
                return Ameisentypen.Kampf;
        }
    }


}