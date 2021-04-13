using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using KiwilandService.Models;

namespace KiwilandService.RailRoad
{
	public class KiwilandService : IKiwilandService
	{
		#region helpers
		private string[] GetWorkingRoutes(string routes)
		{
			return routes.Split(",").Select(r=>r.Trim()).ToArray();
		}
		private string[] GetRoute(string route)
		{
			return route.Split("-").Select(r => r.Trim()).ToArray();
		}

		private string GetNextRoute(string[] inputRoutes, string route)
		{
			return inputRoutes.Where(r => r.StartsWith(route.Substring(1, 1))).FirstOrDefault();
		}
		private string CreateRoute(string[] inputRoutes, string startRoute, string[] EndRoutes)
		{			
			var currentRoute = startRoute.Substring(0,2);
			var result = currentRoute;
			var nextRoute = GetNextRoute(inputRoutes, currentRoute);

			do
			{
				result += nextRoute.Substring(1, 1);
				currentRoute = nextRoute;
				nextRoute = GetNextRoute(inputRoutes, currentRoute);
			} while (EndRoutes.Any(er => er.Substring(0, 2).Equals(currentRoute)));
			//missing last Route 
			if (result.Length>3)
				result += nextRoute.Substring(1, 1);

			return result;
		}

		private static void BuildGraphInstance(string routes)
		{
			GraphInstance graph = GraphInstance.Instance;
			graph.BuildGraph(routes);
		}
		#endregion
		public int NumberOfDifferentRoutes(string inputRoutes, string route, int maxDistance)
		{
			GraphInstance graph = GraphInstance.Instance;
			if (!graph.IsGraphBuilded())
				BuildGraphInstance(inputRoutes);
			var nodes = route.ToCharArray();
			var graphResult = graph.AllRoutesMaxDistance(nodes[0], nodes[1], maxDistance);
			return graphResult;
		}

		public int NumberOfTripsExactStops(string inputRoutes, string route, int exactStops)
		{
			GraphInstance graph = GraphInstance.Instance;
			if (!graph.IsGraphBuilded())
				BuildGraphInstance(inputRoutes);
			var nodes = route.ToCharArray();
			var graphResult = graph.AllRoutesNumStops(nodes[0], nodes[1], exactStops);
			return graphResult.Count;
		}

		public int NumberOfTripsMaxStops(string inputRoutes, string route, int maxStops)
		{
			GraphInstance graph = GraphInstance.Instance;
			if (!graph.IsGraphBuilded())
				BuildGraphInstance(inputRoutes);
			var nodes = route.ToCharArray();
			var graphResult = graph.AllRoutesMaxStops(nodes[0], nodes[1], maxStops);
			return graphResult.Count;
		}

		/// <summary>
		/// calculates the Route distance
		/// </summary>
		/// <param name="inputRoutes"></param>
		/// <param name="route"></param>
		/// <returns></returns>
		public int RouteDistance(string inputRoutes, string route)
		{
			int result = 0;
			var routes = GetWorkingRoutes(inputRoutes);
			var arrRoute = GetRoute(route);
			if (arrRoute.Length == 2)
			{
				var theRoute = routes.Where(r => r.StartsWith(route.Replace("-", string.Empty))).FirstOrDefault();
				result = Convert.ToInt32(theRoute.Substring(2));
			}
			else
			{
				for(var counter = 0; counter<arrRoute.Length; counter++)
				{
					if (counter == arrRoute.Length - 1)
						break;
					var currentRoute = arrRoute[counter] + arrRoute[counter + 1];
					var theRoute = routes.Where(r => r.StartsWith(currentRoute)).FirstOrDefault();
					if (theRoute == null)
					{
						result = -1;
						break;
					}
					result += Convert.ToInt32(theRoute.Substring(2));
				}
			}
			return result;
		}

		public int ShortestRoute(string inputRoutes, string route)
		{
			GraphInstance graph = GraphInstance.Instance;
			if (!graph.IsGraphBuilded())
				BuildGraphInstance(inputRoutes);
			var nodes = route.ToCharArray();
			var graphResult = graph.ShortestRoute(nodes[0], nodes[1]);
			return graphResult.Value;
		}
	}
}
