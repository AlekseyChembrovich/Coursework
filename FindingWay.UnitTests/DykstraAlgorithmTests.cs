using Xunit;
using FindingWay.Common;
using FindingWay.DykstraAlgorithm;

namespace FindingWay.UnitTests;

/// <summary>
/// Набор тестов для алгоритма Дейкстры
/// </summary>
public class DykstraAlgorithmTests
{
    /// <summary>
    /// Тестовый случай проверяет правильность расчёта кратчайшего пути для обычного ориентированного графа.
    /// </summary>
    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathFromTheGraph1()
    {
        // Arrange
        var graph = await DataLoader.LoadUlongSerratedArrayAsync("standard_oriented_graph.txt");
        const int targetNodeIndex = 4; // node #5
        const ulong rightShortestPath = 6;
        
        // Act
        var nodes = graph.GetShortestPath(targetNodeIndex);

        // Assert
        var shortestPath = nodes[targetNodeIndex].Value;
        Assert.Equal(rightShortestPath, shortestPath);
    }

    /// <summary>
    /// Тестовый случай проверяет правильность расчёта кратчайшего пути для неориентированного графа.
    /// </summary>
    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathFromTheGraph2()
    {
        // Arrange
        var graph = await DataLoader.LoadUlongSerratedArrayAsync("standard_undirected_graph.txt");
        const int targetNodeIndex = 3; // node #4
        const ulong rightShortestPath = 5;
        
        // Act
        var nodes = graph.GetShortestPath(targetNodeIndex);

        // Assert
        var shortestPath = nodes[targetNodeIndex].Value;
        Assert.Equal(rightShortestPath, shortestPath);
    }

    /// <summary>
    /// Тестовый случай проверяет правильность обработки случая, когда был указан номер вершины, которой не существует.
    /// </summary>
    [Fact]
    public async Task GetShortestPath_ThrowArgumentException_WhenTargetNodeIndexIsInvalid()
    {
        // Arrange
        var graph = await DataLoader.LoadUlongSerratedArrayAsync("standard_undirected_graph.txt");
        const int invalidTargetNodeIndex = 100; // node #101
        
        // Act
        var action = () =>
        {
            var nodes = graph.GetShortestPath(invalidTargetNodeIndex);
        };
        
        // Assert
        Assert.Throws<ArgumentException>(action);
    }

    /// <summary>
    /// Тестовый случай проверяет правильность обработки графа с вершиной, которую невозможно достичь.
    /// </summary>
    [Fact]
    public async Task GetShortestPath_ThrowArgumentException_WhenImpossibleToReachTargetNode()
    {
        // Arrange
        var graph = await DataLoader.LoadUlongSerratedArrayAsync("unlinked_graph.txt");
        const int targetNodeIndex = 3; // node #4
        
        // Act
        var action = () =>
        {
            var nodes = graph.GetShortestPath(targetNodeIndex);
        };
        
        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
    /// <summary>
    /// Тестовый случай проверяет правильность расчёта кратчайшего пути между штатами Канзас и Вирджиния.
    /// </summary>
    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathBetweenKansasAndVirginia()
    {
        // Arrange
        var graph = await DataLoader.LoadUlongSerratedArrayAsync("united_states_america.txt");
        const int targetNodeIndex = 17; // node #18
        const ulong rightShortestPath = 1575;
        
        // Act
        var nodes = graph.GetShortestPath(targetNodeIndex); // get the shortest route between Kansas and Virginia
        
        // Assert
        var shortestPath = nodes[targetNodeIndex].Value;
        Assert.Equal(rightShortestPath, shortestPath);
    }
}
