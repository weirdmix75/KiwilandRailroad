using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwilandService.RailRoad
{
	public interface IKiwilandService
	{
		int RouteDistance(string inputRoutes, string route);
		int NumberOfTripsMaxStops(string inputRoutes, string route, int maxStops);
		int NumberOfTripsExactStops(string inputRoutes, string route, int exactStops);
		int ShortestRoute(string inputRoutes, string route);
		int NumberOfDifferentRoutes(string inputRoutes, string route, int maxDistance);
	}
}
