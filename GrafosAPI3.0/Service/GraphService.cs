using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using GrafosAPI3._0.Models;

namespace GrafosAPI3._0.Services
{
    public class GraphService
    {
        // Instância da classe Graph que representa o grafo de filmes e atores.
        private readonly Graph _graph;

        // Construtor da classe GraphService.
        public GraphService()
        {
            // Inicializa o grafo e carrega os dados dos filmes para construir o grafo.
            _graph = new Graph();
            LoadMoviesAndBuildGraph();
        }

        // Método privado para carregar os filmes de um arquivo JSON e construir o grafo.
        private void LoadMoviesAndBuildGraph()
        {
            var jsonData = File.ReadAllText("./latest_movies.json");
            var movies = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
            
            // Para cada filme na lista de filmes.
            foreach (var movie in movies)
            {
                // Adiciona o título do filme como um vértice no grafo.
                _graph.AddVertex(movie.Title);
                // Para cada ator no elenco do filme.
                foreach (var actor in movie.Cast)
                {
                    // Adiciona o ator como um vértice no grafo.
                    _graph.AddVertex(actor);
                    // Cria uma aresta entre o filme e o ator.
                    _graph.AddEdge(movie.Title, actor);
                }
            }
        }

        // Método público que busca todos os caminhos entre dois atores usando BFS.
        public List<PathResult> bfsAllPaths(string actor1, string actor2)
        {
            // Obtém todos os caminhos possíveis entre os dois atores.
            var paths = _graph.bfsAllPaths(actor1, actor2);
            var pathResults = new List<PathResult>();

            // Para cada caminho encontrado, cria um novo PathResult e adiciona à lista.
            foreach (var path in paths)
            {
                pathResults.Add(new PathResult { Path = path });
            }

            return pathResults; // Retorna a lista de resultados de caminhos.
        }

        // Método público que busca o caminho mais curto entre dois atores usando BFS.
        public List<PathResult> bfsShortestPath(string actor1, string actor2)
        {
            // Obtém o caminho mais curto entre os dois atores.
            var shortestPath = _graph.bfsShortestPath(actor1, actor2);
            var pathResults = new List<PathResult>();

            // Se houver um caminho mais curto encontrado, cria um novo PathResult e adiciona à lista.
            if (shortestPath.Count > 0)
            {
                pathResults.Add(new PathResult { Path = shortestPath });
            }

            return pathResults; // Retorna a lista de resultados de caminhos.
        }
    }
}
