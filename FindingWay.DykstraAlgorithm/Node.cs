using System.Collections.ObjectModel;

namespace FindingWay.DykstraAlgorithm;

public struct Node
{
    public ulong Value;
    public bool IsChecked = false;

    public Node(ulong value)
    {
        Value = value;
    }
}
