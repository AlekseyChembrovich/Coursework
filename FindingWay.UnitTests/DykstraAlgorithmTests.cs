using Xunit;
using Shouldly;
using FindingWay.DykstraAlgorithm;

namespace FindingWay.UnitTests;

public class DykstraAlgorithmTests
{
    [Fact]
    public void GetShortestPath_ReturnsTheShortestPathFoundFromTheGraph()
    {
        // Arrange
        var graph = new ulong[5][]
        {
            new ulong[] { 0, 1, 4, ulong.MaxValue, ulong.MaxValue },
            new ulong[] { ulong.MaxValue, 0, 2, 1, ulong.MaxValue },
            new ulong[] { ulong.MaxValue, ulong.MaxValue, 0, ulong.MaxValue, 3 },
            new ulong[] { ulong.MaxValue, ulong.MaxValue, 9, 0, 9 },
            new ulong[]  { ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, 0 }
        };

        // Act
        var nodes = graph.GetShortestPath();

        // Assert
        var rightPath = new ulong[] { 0, 1, 3, 2, 6 };
        nodes.Select(x => x.Value)
            .ToArray()
            .ShouldBe(rightPath);
    }
}
