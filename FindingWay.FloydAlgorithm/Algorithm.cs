namespace FindingWay.FloydAlgorithm;

public static class Algorithm
{
    public static long[,] GetShortestPath(this long[,] graph)
    {
        var dist = (long[,])graph.Clone();
        var n = dist.GetLength(0);
        for (var k = 0; k < n; k++)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    const long inf = long.MaxValue;
                    if (dist[i, k] == inf || dist[k, j] == inf)
                        continue;
                    
                    var sum = dist[i, k] + dist[k, j];
                    if (sum < dist[i, j])
                        dist[i, j] = sum;
                }

                if (dist[i, i] < 0)
                    throw new ArgumentException("Graph contains negative cycle.");
            }
        }
        
        return dist;
    }
}
