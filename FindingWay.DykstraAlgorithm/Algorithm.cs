namespace FindingWay.DykstraAlgorithm;

public static class Algorithm
{
    public static IReadOnlyList<Node> GetShortestPath(this ulong[][] graph, int targetI)
    {
        var count = graph.GetLength(0);
        if (targetI >= count || targetI < 0)
            throw new ArgumentException("Passed target node index is not valid.");
        
        var nodes = new Node[count];
        nodes[0] = new Node(0);
        for (var i = 1; i < nodes.Length; i++)
            nodes[i] = new Node(ulong.MaxValue);

        for (var i = 0; i <= targetI; i++)
        {
            var minIndex = GetMinNodeIndex(nodes);
            ref var currentNode = ref nodes[minIndex];
            currentNode.IsChecked = true;
            var relatedNodes = graph[minIndex];
            for (var j = 0; j < count; j++)
            {
                if (minIndex == j)
                    continue;

                ref var nextNode = ref nodes[j];
                if (nextNode.IsChecked)
                    continue;

                var targetNodeValue = relatedNodes[j];
                if (targetNodeValue == ulong.MaxValue)
                    continue;

                var sum = currentNode.Value + targetNodeValue;
                if (sum < nextNode.Value)
                    nextNode.Value = sum;
            }
        }

        return nodes;
    }
    
    private static int GetMinNodeIndex(IReadOnlyList<Node> nodes)
    {
        var minIndex = -1;
        var min = ulong.MaxValue;
        for (var i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].IsChecked || min <= nodes[i].Value)
                continue;

            min = nodes[i].Value;
            minIndex = i;
        }

        if (minIndex == -1)
            throw new ArgumentException("It is impossible to reach specified node.");
        
        return minIndex;
    }
}
