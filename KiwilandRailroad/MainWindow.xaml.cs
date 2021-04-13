using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.DependencyInjection;
using KiwilandService.RailRoad;

namespace KiwilandRailroad
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		IKiwilandService _kiwilandService { get; set; }
		public MainWindow(IKiwilandService service)
		{
			InitializeComponent();
			_kiwilandService = service;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			var allResults = new StringBuilder();
			//all this answers is code made by me
			var result1 = _kiwilandService.RouteDistance(txtData.Text, "A-B-C");
			allResults.Append("The distance of the route A-B-C: " + EvaluateResult(result1) + "\n");
			var result2 = _kiwilandService.RouteDistance(txtData.Text, "A-D");
			allResults.Append("The distance of the route A-D: " + EvaluateResult(result2) + "\n");
			var result3 = _kiwilandService.RouteDistance(txtData.Text, "A-D-C");
			allResults.Append("The distance of the route A-D-C: " + EvaluateResult(result3) + "\n");
			var result4 = _kiwilandService.RouteDistance(txtData.Text, "A-E-B-C-D");
			allResults.Append("The distance of the route A-E-B-C-D: " + EvaluateResult(result4) + "\n");
			var result5 = _kiwilandService.RouteDistance(txtData.Text, "A-E-D");
			allResults.Append("The distance of the route A-E-D: " + EvaluateResult(result5) + "\n");

			//this code has just implemented
			var result6 = _kiwilandService.NumberOfTripsMaxStops(txtData.Text, "CC", 3);
			allResults.Append("The number of trips starting at C and ending at C with a maximum of 3 stops: " + result6.ToString() + "\n");
			var result7 = _kiwilandService.NumberOfTripsExactStops(txtData.Text, "AC", 4);
			allResults.Append("The number of trips starting at A and ending at C with exactly 4 stops.: " + result7.ToString() + "\n");
			var result8 = _kiwilandService.ShortestRoute(txtData.Text, "AC");
			allResults.Append("The length of the shortest route (in terms of distance to travel) from A to C: " + result8.ToString() + "\n");
			var result9 = _kiwilandService.ShortestRoute(txtData.Text, "BB");
			allResults.Append("The length of the shortest route (in terms of distance to travel) from B to B: " + result9.ToString() + "\n");
			var result10 = _kiwilandService.NumberOfDifferentRoutes(txtData.Text, "CC", 30);
			allResults.Append("The number of different routes from C to C with a distance of less than 30: " + result10.ToString() + "\n");

			txtResults.Text = allResults.ToString();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			txtData.Text = "AB5, BC4, CD8, DC8, DE6,AD5, CE2, EB3, AE7";
		}
		private string EvaluateResult(int result)
        {
			return result.Equals(-1) ? "NO SUCH ROUTE" : result.ToString();
        }
	}
}
