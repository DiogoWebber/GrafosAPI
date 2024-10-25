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
        
       public List<List<string>> bfsAllPaths(string actor1, string actor2)
        {
            var queue = new Queue<List<string>>();
            // Inicializa a fila com um caminho contendo o vértice inicial.
            queue.Enqueue(new List<string> { actor1 });
            var visited = new HashSet<string> { actor1 }; // Conjunto de vértices visitados.
            var paths = new List<List<string>>(); // Lista para armazenar os caminhos encontrados.

            while (queue.Count > 0)
            {
                var currentPath = queue.Dequeue(); 
                var currentVertex = currentPath[^1]; // O último vértice do caminho atual.

                // Limita o comprimento do caminho a 9 vértices.
                if (currentPath.Count > 9)
                    continue;

                // Se o vértice atual é o vértice final, adiciona o caminho à lista de caminhos.
                if (currentVertex == actor2)
                {
                    paths.Add(new List<string>(currentPath));
                    continue;
                }

                // Para cada vizinho do vértice atual.
                foreach (var neighbor in AdjacencyList[currentVertex])
                {
                    // Se o vizinho não está no caminho atual.
                    if (!currentPath.Contains(neighbor))
                    {
                        var newPath = new List<string>(currentPath) { neighbor }; // Cria um novo caminho adicionando o vizinho.
                        queue.Enqueue(newPath); // Envia o novo caminho para a fila.

                        // Marca o vizinho como visitado se ele não tiver sido visitado ainda e o novo caminho tiver menos de 9 vértices.
                        if (!visited.Contains(neighbor) && newPath.Count < 9)
                        {
                            visited.Add(neighbor);
                        }
                    }
                }
            }

            return paths; 
        }
        
        // Método que encontra o caminho mais curto entre dois vértices usando BFS.
        public List<string> bfsShortestPath(string start, string goal)
        {
            var queue = new Queue<List<string>>();
            var visited = new HashSet<string>();

            // Inicializa a fila com um caminho contendo o vértice inicial.
            queue.Enqueue(new List<string> { start });
            visited.Add(start); // Marca o vértice inicial como visitado.

            // Enquanto houver caminhos na fila.
            while (queue.Count > 0)
            {
                var path = queue.Dequeue(); // Obtém o caminho atual da fila.
                var current = path.Last(); // O último vértice do caminho atual.

                // Se o vértice atual é o objetivo, retorna o caminho.
                if (current == goal)
                {
                    return path; // Retorna o caminho mais curto
                }

                // Para cada vizinho do vértice atual.
                foreach (var neighbor in GetNeighbors(current))
                {
                    // Se o vizinho não foi visitado.
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor); // Marca o vizinho como visitado.
                        var newPath = new List<string>(path) { neighbor }; // Cria um novo caminho adicionando o vizinho.
                        queue.Enqueue(newPath); // Envia o novo caminho para a fila.
                    }
                }
            }

            return new List<string>(); // Retorna uma lista vazia se não encontrar o caminho.
        }

        // Método para obter os vizinhos de um vértice.
        public List<string> GetNeighbors(string vertex)
        {
            // Se o vértice existe na lista de adjacência.
            if (AdjacencyList.ContainsKey(vertex))
            {
                return AdjacencyList[vertex]; // Retorna a lista de vizinhos do vértice.
            }
            return new List<string>(); // Retorna uma lista vazia se o vértice não existir.
        }
    }
}
