namespace FindingWay.DykstraAlgorithm;

public class Node<T>
{
    public T Value;
    public bool IsChecked = false;

    public Node(T value)
    {
        Value = value;
    }
}
