using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


namespace PathBasedAnimation
{
	/// Példaprogram C# kódból megvalósított útvonalkövető animációra.
	/// Egy négyzetet mozgatunk úgy, hogy a bal felső sarok kövessen
	/// egy archimédeszi spirálvonalat.
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Konstruktor.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
		}


		/// <summary>
		/// Animáció megvalósítása.
		/// </summary>
		/// <param name="sender">Indít nyomógomb.</param>
		/// <param name="e"></param>
		private void btIndít_Click(object sender, RoutedEventArgs e)
		{
			//
			// Az animáció és a megrajzolásra kerülő görbe útvonalának meghatározása.
			//
			// Útvonalgeometria objektum definiálása.
			PathGeometry pgÚtvonal = new PathGeometry();
			// Az útvonalgeometriát egy vagy több útvonalalak (Figure) alkotja.
			// Útvonalalak objektum definálása.
			PathFigure pfÚtvonal = new PathFigure();
			// Kiindulópont definiálása.
			pfÚtvonal.StartPoint = new Point(200, 200);
			// Kezdetben a négyzet bal felső sarka legyen a kiindulópontban.
			rcNégyzet.SetValue(Canvas.LeftProperty, pfÚtvonal.StartPoint.X);
			rcNégyzet.SetValue(Canvas.TopProperty, pfÚtvonal.StartPoint.Y);
			// A sprirált definiáló objektum létrehozása.
			
			var asSpirál = new ArhimédesziSpirál(pfÚtvonal.StartPoint.X, pfÚtvonal.StartPoint.Y,
				10, 0, 1080, 5);
			// A spirált meghatározó pontok lekérdezése (egy List<Point> objektum)+
			// A meghatározó pontokat egy egy egyenessel kösse össze az útvonal.
			PolyLineSegment plsVonal =
				new PolyLineSegment((System.Collections.Generic.IEnumerable<Point>)asSpirál.Pontok, true);
			// Szegmens hozzáadása az útvonalalakhoz.
			pfÚtvonal.Segments.Add(plsVonal);
			// Útvonalalak hozzáadása a geometriához.
			pgÚtvonal.Figures.Add(pfÚtvonal);
			// Útvonalgeometria rögzítése, ami teljesítményjavulást eredményez.
			pgÚtvonal.Freeze();
			//
			// A görbe megrajzolása.
			//
			// Útvonal objektum definiálása.
			Path ptVonal = new Path();
			// A görbe útvonalának megadása.
			ptVonal.Data = pgÚtvonal;
			// A görbe vonalszíne.
			ptVonal.Stroke = Brushes.Black;
			// A görbe vonalvastagsága.
			ptVonal.StrokeThickness = 2;
			// Görbe megjelenítése=felvétele a Canvasra
			cvLap.Children.Add(ptVonal);
			//
			// Animáció megvalósítása - 2 komponens lesz
			//
			// Animáció objektum definiálása a vízszintes irányú mozgatáshoz.
			DoubleAnimationUsingPath daxAnimáció =
					new DoubleAnimationUsingPath();
			// Az útvonal megadása.
			daxAnimáció.PathGeometry = pgÚtvonal;
			// Az animáció időtartama.
			daxAnimáció.Duration = TimeSpan.FromSeconds(10);
			// Az animáció az x koordinátára vonatkozzon.
			daxAnimáció.Source = PathAnimationSource.X;
			// Csinálja meg visszafele is.
			daxAnimáció.AutoReverse = true;
			// Animáció objektum definiálása a függőleges irányú mozgatáshoz.
			DoubleAnimationUsingPath dayAnimáció =
					new DoubleAnimationUsingPath();
			// Az útvonal megadása.
			dayAnimáció.PathGeometry = pgÚtvonal;
			// Az animáció időtartama.
			dayAnimáció.Duration = TimeSpan.FromSeconds(10);
			// Az animáció az y koordinátára vonatkozzon.
			dayAnimáció.Source = PathAnimationSource.Y;
			// Csinálja meg visszafele is.
			dayAnimáció.AutoReverse = true;
			// Animáció indítása.
			rcNégyzet.BeginAnimation(Canvas.TopProperty, dayAnimáció);
			rcNégyzet.BeginAnimation(Canvas.LeftProperty, daxAnimáció);
		}
	}
}
