using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using GrafosAPI3._0.Models;

namespace GrafosAPI3._0.Services
{
    public class GraphService
    {
        private readonly Graph _graph;

        public GraphService()
        {
            _graph = new Graph();
            LoadMoviesAndBuildGraph();
        }
        

        private void LoadMoviesAndBuildGraph()
        {
            var jsonData = File.ReadAllText("./latest_movies.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
            
            foreach (var movie in movies)
            {
                _graph.AddVertex(movie.Title);
                foreach (var actor in movie.Cast)
                {
                    _graph.AddVertex(actor);
                    _graph.AddEdge(movie.Title, actor);
                }
            }
        }

        public List<PathResult> bfsAllPaths(string actor1, string actor2)
        {
            var paths = _graph.bfsAllPaths(actor1, actor2);
            var pathResults = new List<PathResult>();

            foreach (var path in paths)
            {
                pathResults.Add(new PathResult { Path = path });
            }

            return pathResults;
        }
        public List<PathResult> bfsShortestPath(string actor1, string actor2)
        {
            var shortestPath = _graph.bfsShortestPath(actor1, actor2);
            var pathResults = new List<PathResult>();

            if (shortestPath.Count > 0)
            {
                pathResults.Add(new PathResult { Path = shortestPath });
            }

            return pathResults;
        }
    }
}
