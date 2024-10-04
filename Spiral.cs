using System;
using System.Collections.Generic;
using System.Windows;

namespace PathBasedAnimation
{
	/// <summary>
	/// Archimédeszi spirál számítását támogató osztály.
	/// </summary>
	class ArhimédesziSpirál
	{
		/// <summary>
		/// Kiindulópont x koordinátája.
		/// </summary>
		private double x0;
		/// <summary>
		/// Kiindulópont y koordinátája.
		/// </summary>
		private double y0;
		/// <summary>
		/// Együttható;
		/// </summary>
		private double k;
		/// <summary>
		/// A spirálszámítás kezdőszöge radiánban.
		/// </summary>
		private double KezdőSzög;
		/// <summary>
		/// A spirálszámítás zárószöge radiánban.
		/// </summary>
		private double ZáróSzög;
		/// <summary>
		/// Két egymásutáni szögérték távolsága radiánban.
		/// </summary>
		private double Lépés;
		/// <summary>
		/// A pontokat tároló lista objektum.
		/// </summary>
		private List<Point> lpPontok;
		/// <summary>
		/// Konstruktor. Inicializálja az adatagokat.
		/// </summary>
		/// <param name="X0">Kiindulópont x koordinátája.</param>
		/// <param name="Y0">Kiindulópont y koordinátája.</param>
		/// <param name="K">Együttható;</param>
		/// <param name="KSz">A spirálszámítás kezdőszöge fokban.</param>
		/// <param name="ZSz">A spirálszámítás zárószöge fokban.</param>
		/// <param name="L">Két egymásutáni szögérték távolsága fokban.</param>
		public ArhimédesziSpirál(double X0, double Y0, double K, double KSz,
			double ZSz, double L)
		{
			x0 = X0;
			y0 = Y0;
			k = K;
			// A szögeket radiánba alakítjuk.
			KezdőSzög = KSz * Math.PI / 180;
			ZáróSzög = ZSz * Math.PI / 180;
			Lépés = L * Math.PI / 180;
		}
		/// <summary>
		/// Lehetővé teszi a görbét meghatározó pontok lekérdezését (kiszámítását).
		/// </summary>
		public List<Point> Pontok
		{
			get
			{
				// A pontokat tároló lista objektum létrehozása.
				lpPontok = new List<Point>();
				// Minden szöghöz kiszámítja a pont x,y koordinátáját.
				for (double fi = KezdőSzög; fi <= ZáróSzög; fi += Lépés)
				{
					double r = k * fi;
					lpPontok.Add(new Point(x0 + r * Math.Cos(fi), y0 - r * Math.Sin(fi)));
				}
				return lpPontok;
			}
		}
	}
}
