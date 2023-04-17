using Xunit;
using Shouldly;
using FindingWay.FloydAlgorithm;

namespace FindingWay.UnitTests;

public class FloydAlgorithmTests
{
    [Fact]
    public void GetShortestPath_ReturnsTheShortestPathFoundFromTheGraph()
    {
        // Arrange
        /*var graph = new ulong[5, 5]
        {
            { 0, 1, 4, ulong.MaxValue, ulong.MaxValue },
            { ulong.MaxValue, 0, 2, 1, ulong.MaxValue },
            { ulong.MaxValue, ulong.MaxValue, 0, ulong.MaxValue, 3 },
            { ulong.MaxValue, ulong.MaxValue, 9, 0, 9 },
            { ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, 0 }
        };*/

        var graph = new ulong[4, 4]
        {
            { 0, 3, 1, ulong.MaxValue },
            { 3, 0, ulong.MaxValue, 5 },
            { 1, ulong.MaxValue, 0, 9 },
            { ulong.MaxValue, 5, 9, 0 }
        };
        
        // Act
        var result = graph.GetShortestPath();

        // Assert
        var rightPaths = new ulong[,]
        {
            { 0, 1, 3, 2, 6 },
            { ulong.MaxValue, 0, 2, 1, 5 },
            { ulong.MaxValue, ulong.MaxValue, 0, ulong.MaxValue, 3 },
            { ulong.MaxValue, ulong.MaxValue, 9, 0, 9 },
            { ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, 0 },
        };

        result.ShouldBe(rightPaths);
    }
}
