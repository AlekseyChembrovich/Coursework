namespace FindingWay.DykstraAlgorithm;

/// <summary>
/// Структура представляет собой узел графа
/// </summary>
public struct Node
{
    /// <summary>
    /// Значение узла
    /// </summary>
    public ulong Value;

    /// <summary>
    /// Отметка сообщающая о посещении данной вершины
    /// </summary>
    public bool IsChecked = false;

    public Node(ulong value)
    {
        Value = value;
    }
}
