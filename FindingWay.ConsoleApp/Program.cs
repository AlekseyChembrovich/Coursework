using FindingWay.Common;
using FindingWay.ConsoleApp;
using BenchmarkDotNet.Running;
using FindingWay.FloydAlgorithm;
using FindingWay.DykstraAlgorithm;

#if RELEASE

BenchmarkRunner.Run<AppBenchmark>();

#else

while (true)
{
    // Отображение основной группы команд
    Console.WriteLine("1 - Выполнить алгоритм Дейкстры");
    Console.WriteLine("2 - Выполнить алгоритм Флойда");
    Console.WriteLine("Esc - Завершить программу");

    var commandKey = GetCommand();
    if (commandKey is ConsoleKey.Escape) // Необходимо завершить работу программы
    {
        break;
    }
    else if (commandKey is ConsoleKey.D1) // Операции с алгоритмом Дейкстры
    {
        var graph = await GetDataForDykstra(); // Получение матрицы смежности для выполнения алгоритма
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
            nodes = graph.GetShortestPath(node); // Выполнение основного алгоритма
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
    else if (commandKey is ConsoleKey.D2) // Операции с алгоритмом Флойда
    {
        var graph = await GetDataForFloyd(); // Получение матрицы смежности для выполнения алгоритма
        long[,] result = default;
        try
        {
            result = graph.GetShortestPaths(); // Выполнение основного алгоритма
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"В процессе работы возникла ошибка: {ex.Message}");
            Console.WriteLine(new string('-', 40));
            continue;
        }

        // Вывод результатов расчёта
        var size = result.GetLength(0);
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                if (result[i, j] == long.MaxValue)
                    Console.Write($"Inf | ");
                else
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

/// <summary>
/// Метод отображает операции с алгоритмом Дейкстры к выполнению, после чего пользователь выбирает необходимую операцию.
/// В итоге метод считывает матрицу смежности из файла и возвращает вызывающему методу.
/// </summary>
/// <returns>Считанная метрица смежности.</returns>
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

/// <summary>
/// Метод отображает операции с алгоритмом Флойда к выполнению, после чего пользователь выбирает необходимую операцию.
/// В итоге метод считывает матрицу смежности из файла и возвращает вызывающему методу.
/// </summary>
/// <returns>Считанная метрица смежности.</returns>
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

/// <summary>
/// Метод запрашивает у пользователя ввод команды с клавиатуры, после чего выполняет дополнительные консольные манипуляции.
/// </summary>
/// <returns>Код нажатой клавиши на клавиатуре.</returns>
static ConsoleKey GetCommand()
{
    Console.Write("\nВвод команды: ");
    var currentCommand = Console.ReadKey();
    Console.WriteLine();
    Console.WriteLine(new string('-', 40));

    return currentCommand.Key;
}

#endif
