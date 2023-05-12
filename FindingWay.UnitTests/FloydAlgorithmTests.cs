﻿using Xunit;
using Shouldly;
using FindingWay.Common;
using FindingWay.FloydAlgorithm;

namespace FindingWay.UnitTests;

public class FloydAlgorithmTests
{
    private const long Inf = long.MaxValue;

    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathFoundFromTheGraph1()
    {
        // Arrange
        var graph = await DataLoader.LoadLongMatrixAsync("standard_oriented_graph.txt");
        
        // Act
        var result = graph.GetShortestPath();

        // Assert
        result.ShouldBe(new[,]
        {
            { 0, 1, 3, 2, 6 },
            { Inf, 0, 2, 1, 5 },
            { Inf, Inf, 0, Inf, 3 },
            { Inf, Inf, 9, 0, 9 },
            { Inf, Inf, Inf, Inf, 0 },
        });
    }
    
    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathFoundFromTheGraph2()
    {
        // Arrange
        var graph = await DataLoader.LoadLongMatrixAsync("oriented_graph_with_negative_edges.txt");
        
        // Act
        var result = graph.GetShortestPath();

        // Assert
        result.ShouldBe(new long[,]
        {
            { 0, -1, -2, 0 },
            { 4, 0, 2, 4 },
            { 5, 1, 0, 2 },
            { 3, -1, 1, 0 }
        });
    }
    
    [Fact]
    public async Task GetShortestPath_ThrowArgumentException_WhenGraphContainsNegativeCycle()
    {
        // Arrange
        var graph = await DataLoader.LoadLongMatrixAsync("graph_with_negative_cycle.txt");
        
        // Act
        var action = () =>
        {
            var result = graph.GetShortestPath();  
        };

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
    [Fact]
    public async Task GetShortestPath_ReturnsTheShortestPathBetweenKansasAndVirginia()
    {
        // Arrange
        var graph = await DataLoader.LoadLongMatrixAsync("united_states_america.txt");
        const int targetNodeIndex = 17; // node #18
        const long rightShortestPath = 1575;
        
        // Act
        var result = graph.GetShortestPath();
        
        // Assert
        var shortestPath = result[0, targetNodeIndex]; // check the shortest route between Kansas and Virginia
        Assert.Equal(rightShortestPath, shortestPath);
    }
}
