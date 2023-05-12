using FindingWay.Common;
using FindingWay.FloydAlgorithm;
using FindingWay.DykstraAlgorithm;

while (true)
{
    Console.WriteLine("1 - Выполнить алгоритм Дейкстры");
    Console.WriteLine("2 - Выполнить алгоритм Флойда");
    Console.WriteLine("Esc - Завершить программу");
    
    var commandKey = GetCommand();
    if (commandKey is ConsoleKey.Escape)
    {
        break;
    }
    else if (commandKey is ConsoleKey.D1)
    {
        var graph = await GetDataForDykstra();
        Console.Write("Введите номер вершины, которую необходимо достичь: ");
        var isParsed = int.TryParse(Console.ReadLine(), out var node);
        if (!isParsed)
        {
            Console.WriteLine("Номер не может быть распознан как число");
            Console.WriteLine(new string('-', 40));
            continue;
        }

        node--;
        IReadOnlyList<Node> nodes = default;
        try
        {
            nodes = graph.GetShortestPath(node);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"В процессе работы возникла ошибка: {ex.Message}");
            Console.WriteLine(new string('-', 40));
            continue;
        }

        Console.WriteLine($"Кратчайший путь = {nodes[node].Value}");
        Console.WriteLine(new string('-', 40));
    }
    else if (commandKey is ConsoleKey.D2)
    {
        var graph = await GetDataForFloyd();
        long[,] result = default;
        try
        {
            result = graph.GetShortestPath();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"В процессе работы возникла ошибка: {ex.Message}");
            Console.WriteLine(new string('-', 40));
            continue;
        }

        var size = result.GetLength(0);
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                if (result[i, j] == long.MaxValue)
                {
                    Console.Write($"Inf | ");
                    continue;
                }

                Console.Write($"{result[i, j]} | ");
            }

            Console.WriteLine();
        }
        
        Console.WriteLine(new string('-', 40));
    }
    else
    {
        throw new ArgumentException("Необрабатываемый тип команды.");
    }
}

Console.WriteLine("Программа завершила свою работу");

static async Task<ulong[][]> GetDataForDykstra()
{
    Console.WriteLine("1 - Cтандартный ориентированный граф");
    Console.WriteLine("2 - Cтандартный неориентированный граф");
    Console.WriteLine("3 - Граф cоединённых штатов Америки");

    var commandKey = GetCommand();
    var fileName = commandKey switch
    {
        ConsoleKey.D1 => "standard_oriented_graph.txt",
        ConsoleKey.D2 => "standard_undirected_graph.txt",
        ConsoleKey.D3 => "united_states_america.txt",
        _ => throw new ArgumentException("Необрабатываемый тип команды.")
    };

    var graph = await DataLoader.LoadUlongSerratedArrayAsync(fileName);

    return graph;
}

static async Task<long[,]> GetDataForFloyd()
{
    Console.WriteLine("1 - Cтандартный ориентированный граф");
    Console.WriteLine("2 - Ориентированный граф с отрицательными ребрами");
    Console.WriteLine("3 - Граф cоединённых штатов Америки");

    var commandKey = GetCommand();
    var fileName = commandKey switch
    {
        ConsoleKey.D1 => "standard_oriented_graph.txt",
        ConsoleKey.D2 => "oriented_graph_with_negative_edges.txt",
        ConsoleKey.D3 => "united_states_america.txt",
        _ => throw new ArgumentException("Необрабатываемый тип команды.")
    };

    var graph = await DataLoader.LoadLongMatrixAsync(fileName);

    return graph;
}

static ConsoleKey GetCommand()
{
    Console.Write("\nВвод команды: ");
    var currentCommand = Console.ReadKey();
    Console.WriteLine();
    Console.WriteLine(new string('-', 40));

    return currentCommand.Key;
}
