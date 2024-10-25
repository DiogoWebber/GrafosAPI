using System;
using System.Collections.Generic;

namespace GrafosAPI3._0.Services
{
    public class Graph
    {
        public Dictionary<string, List<string>> AdjacencyList { get; private set; }

        public Graph()
        {
            AdjacencyList = new Dictionary<string, List<string>>();
        }

        public void AddVertex(string vertex)
        {
            if (!AdjacencyList.ContainsKey(vertex))
            {
                AdjacencyList[vertex] = new List<string>();
            }
        }

        public void AddEdge(string vertex1, string vertex2)
        {
            if (!AdjacencyList.ContainsKey(vertex1)) AddVertex(vertex1);
            if (!AdjacencyList.ContainsKey(vertex2)) AddVertex(vertex2);
            AdjacencyList[vertex1].Add(vertex2);
            AdjacencyList[vertex2].Add(vertex1);
        }

        public int Size()
        {
            return AdjacencyList.Count;
        }

        public List<List<string>> bfsAllPaths(string actor1, string actor2)
        {
            var queue = new Queue<List<string>>();
            queue.Enqueue(new List<string> { actor1 });
            var visited = new HashSet<string> { actor1 };
            var paths = new List<List<string>>();

            while (queue.Count > 0)
            {
                var currentPath = queue.Dequeue();
                var currentVertex = currentPath[^1];

                if (currentPath.Count > 9)
                    continue;

                if (currentVertex == actor2)
                {
                    paths.Add(new List<string>(currentPath));
                    continue;
                }

                foreach (var neighbor in AdjacencyList[currentVertex])
                {
                    if (!currentPath.Contains(neighbor))
                    {
                        var newPath = new List<string>(currentPath) { neighbor };
                        queue.Enqueue(newPath);

                        if (!visited.Contains(neighbor) && newPath.Count < 9)
                        {
                            visited.Add(neighbor);
                        }
                    }
                }
            }

            return paths;
        }
        
        public List<string> bfsShortestPath(string start, string goal)
        {
            var queue = new Queue<List<string>>();
            var visited = new HashSet<string>();
    
            queue.Enqueue(new List<string> { start });
            visited.Add(start);

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var current = path.Last();

                if (current == goal)
                {
                    return path; // Retorna o caminho mais curto
                }

                foreach (var neighbor in GetNeighbors(current))
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        var newPath = new List<string>(path) { neighbor };
                        queue.Enqueue(newPath);
                    }
                }
            }

            return new List<string>(); // Retorna uma lista vazia se não encontrar o caminho
        }
        public List<string> GetNeighbors(string vertex)
        {
            if (AdjacencyList.ContainsKey(vertex))
            {
                return AdjacencyList[vertex]; // Retorna a lista de vizinhos do vértice
            }
            return new List<string>(); // Retorna uma lista vazia se o vértice não existir
        }


    }
}
