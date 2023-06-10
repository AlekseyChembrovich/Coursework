using BenchmarkDotNet.Order;
using FindingWay.FloydAlgorithm;
using BenchmarkDotNet.Attributes;
using FindingWay.DykstraAlgorithm;

namespace FindingWay.ConsoleApp;

/// <summary>
/// Реализация бенчмарка приложения для сравнения скорости работы алгоритмов
/// </summary>
[RankColumn]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class AppBenchmark
{
    #region Идентичные матрицы смежности

    private readonly ulong[][] _graphForDijkstra = new[]
    {
        new ulong[] { 0, 1, 4, ulong.MaxValue, ulong.MaxValue },
        new ulong[] { ulong.MaxValue, 0, 2, 1, ulong.MaxValue },
        new ulong[] { ulong.MaxValue, ulong.MaxValue, 0, ulong.MaxValue, 3 },
        new ulong[] { ulong.MaxValue, ulong.MaxValue, 9, 0, 9 },
        new ulong[] { ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, 0 },
    };

    private readonly long[,] _graphForFloyd = new[,]
    {
        { 0, 1, 4, long.MaxValue, long.MaxValue },
        { long.MaxValue, 0, 2, 1, long.MaxValue },
        { long.MaxValue, long.MaxValue, 0, long.MaxValue, 3 },
        { long.MaxValue, long.MaxValue, 9, 0, 9 },
        { long.MaxValue, long.MaxValue, long.MaxValue, long.MaxValue, 0 },
    };

    #endregion

    [Benchmark]
    public ulong GetShortestPathByDijkstra()
    {
        const int targetNodeIndex = 4;
        
        var nodes = _graphForDijkstra.GetShortestPath(targetNodeIndex);
        
        return nodes[targetNodeIndex].Value;
    }
    
    [Benchmark]
    public long GetShortestPathByFloyd()
    {
        const int targetNodeIndex = 4;
        
        var graph = _graphForFloyd.GetShortestPaths();
        
        return graph[0, targetNodeIndex];
    }
}
