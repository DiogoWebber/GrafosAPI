namespace GrafosAPI3._0.Models
{
    public class PathResult
    {
        public List<string> Path { get; set; }
        
        // Calcula o tamanho como o número de arestas (que é o número de vértices - 1)
        public int Length => Path.Count > 0 ? Path.Count - 1 : 0; 
    }
}