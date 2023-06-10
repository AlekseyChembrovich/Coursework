namespace FindingWay.DykstraAlgorithm;

/// <summary>
/// Реализация алгоритма Дейкстры для нахождения кратчайших путей между всеми вершинами.
/// </summary>
public static class Algorithm
{
    /// <summary>
    /// Метод находит кратчайший путь до целевой вершины через алгоритм Дейкстры.
    /// </summary>
    /// <param name="graph">Исходная матрица смежности.</param>
    /// <param name="target">Номер целевой вершины, до которой нужно рассчитать путь.</param>
    /// <returns>Коллекция узлов графа, которая содержит целевой узел с рассчитанной дистанцией.</returns>
    /// <exception cref="ArgumentException">
    /// Если номер целевой вершины превышает размерность массива или меньше нуля - параметр имеет некорректное значение.
    /// Для предотвращения этой ситуации, используется исключение, которое выбрасывается из метода.
    /// </exception>
    public static IReadOnlyList<Node> GetShortestPath(this ulong[][] graph, int target)
    {
        var size = graph.GetLength(0); // Получаем одну из размерностей квадратной матрицы
        if (target >= size || target < 0)
            throw new ArgumentException("Passed target node index is not valid.");
        
        var nodes = new Node[size]; // Инициализация коллекции вершин графа и установка значений по умолчанию
        nodes[0] = new Node(0);
        for (var i = 1; i < nodes.Length; i++)
            nodes[i] = new Node(ulong.MaxValue);

        for (var i = 0; i <= target; i++)
        {
            var minIndex = GetMinNodeIndex(nodes); // Определение индекса минимальной вершины
            ref var currentNode = ref nodes[minIndex]; // Получаем минимальную вершину
            currentNode.IsChecked = true; // Помечаем данную вершину как посещённую
            var relatedNodes = graph[minIndex]; // Для данной вершины получаем связи с остальными вершинами графа
            for (var j = 0; j < size; j++) // Выполняем пересчёт дистанции для каждой вершины графа, через минимальную
            {
                if (minIndex == j) // Если минимальная и есть выбранная - пропускаем
                    continue;

                ref var nextNode = ref nodes[j];
                if (nextNode.IsChecked) // Если выбранная вершина уже посещена - пропускаем
                    continue;

                var nodeValue = relatedNodes[j];
                if (nodeValue == ulong.MaxValue) // Если между минимальной и выбранной вершинами отсутствует связь - пропускаем
                    continue;

                // Сложение дистанции до минимальной вершины и выбранной, если сумма меньше чем та,
                // которая уже хранится в результирующей коллекции - сохраняем
                var sum = currentNode.Value + nodeValue;
                if (sum < nextNode.Value)
                    nextNode.Value = sum;
            }
        }

        return nodes;
    }

    /// <summary>
    /// Метод выполняет поиск наименьшей непосещенной вершины графа.
    /// </summary>
    /// <param name="nodes">Исходная коллекция вершин, в которой проводится поиск.</param>
    /// <returns>Индекс найденной вершины в исходной коллекции.</returns>
    /// <exception cref="ArgumentException">
    /// Если после выполнения поиска значение индекса имеет значение исходное - исходная коллекция содержит некорректные вершины.
    /// Для предотвращения этой ситуации, используется исключение, которое выбрасывается из метода.
    /// </exception>
    private static int GetMinNodeIndex(IReadOnlyList<Node> nodes)
    {
        var minIndex = -1; // Исходное значение индекса
        var min = ulong.MaxValue; // Исходное значение минимального значения
        for (var i = 0; i < nodes.Count; i++)
        {
            // Если вершина уже посещена или имеет не подходящее значение
            if (nodes[i].IsChecked || min <= nodes[i].Value)
                continue;

            min = nodes[i].Value;
            minIndex = i;
        }

        if (minIndex == -1)
            throw new ArgumentException("It is impossible to determine minimum node in source graph.");
        
        return minIndex;
    }
}
