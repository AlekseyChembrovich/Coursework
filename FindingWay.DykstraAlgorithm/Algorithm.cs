namespace FindingWay.DykstraAlgorithm;

public static class Algorithm
{
    public static IReadOnlyList<Node<ulong>> GetShortestPath(this ulong[][] graph)
    {
        var nodes = new Node<ulong>[5]
        {
            new(0),
            new(ulong.MaxValue),
            new(ulong.MaxValue),
            new(ulong.MaxValue),
            new(ulong.MaxValue)
        };

        var n = graph.GetLength(0);
        for (var i = 0; i < n; i++)
        {
            var minIndex = GetMinNodeIndex(nodes);
            var currentNode = nodes[minIndex];
            currentNode.IsChecked = true;
            var relatedNodes = graph[minIndex];
            
            for (var j = 0; j < n; j++)
            {
                if (minIndex == j)
                {
                    continue;
                }

                var targetNode = nodes[j];
                if (targetNode.IsChecked)
                {
                    continue;
                }

                var targetNodeValue = relatedNodes[j];
                if (targetNodeValue == ulong.MaxValue)
                {
                    continue;
                }

                var sum = currentNode.Value + targetNodeValue;
                if (sum < targetNode.Value)
                {
                    targetNode.Value = sum;
                }
            }
        }

        return nodes;
    }

    private static int GetMinNodeIndex(IReadOnlyList<Node<ulong>> array)
    {
        var minIndex = -1;
        var min = ulong.MaxValue;

        for (var i = 0; i < array.Count; i++)
        {
            if (array[i].IsChecked)
            {
                continue;
            }

            if (min <= array[i].Value)
            {
                continue;
            }

            min = array[i].Value;
            minIndex = i;
        }

        return minIndex;
    }
}
