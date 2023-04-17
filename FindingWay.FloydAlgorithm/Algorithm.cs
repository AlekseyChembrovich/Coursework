namespace FindingWay.FloydAlgorithm;

public static class Algorithm
{
    public static ulong[,] GetShortestPath(this ulong[,] graph)
    {
        var dist = (ulong[,])graph.Clone();
        var n = dist.GetLength(0);
        
        for (var k = 0; k < n; k++)
        {
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    const ulong inf = ulong.MaxValue;
                    if (dist[i, k] == inf || dist[k, j] == inf)
                    {
                        continue;
                    }
                    
                    var sum = dist[i, k] + dist[k, j];
                    if (sum < dist[i, j])
                    {
                        dist[i, j] = sum;
                    }
                }
            }
        }

        return dist;
    }
}
