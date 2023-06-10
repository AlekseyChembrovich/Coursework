namespace FindingWay.Common;

/// <summary>
/// Реализация функционала для чтения данных из файла
/// </summary>
public static class DataLoader
{
    /// <summary>
    /// Метод выполняет чтение матрицы смежности как зубчатого массива из файла в соответствии с указанным именем.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Матрица смежности в виде зубчатого массива.</returns>
    public static async Task<ulong[][]> LoadUlongSerratedArrayAsync(string fileName)
    {
        var matrixLines = await ReadFileAsync(fileName); // Читаем строки файла
        var size = matrixLines.Length; // Определяем размерность квадратной матрицы
        var result = new ulong[size][];
        for (var i = 0; i < size; i++)
        {
            var items = matrixLines[i].Split(' '); // Разбиваем отдельные строки на числа, при помощи пробела
            var newLine = new ulong[size];
            for (var j = 0; j < items.Length; j++)
            {
                // Если встречается аббревиатура "Inf", означающая бесконечность, назначить максимальное число
                if (items[j].Equals("Inf", StringComparison.OrdinalIgnoreCase))
                {
                    newLine[j] = ulong.MaxValue;
                    continue;
                }

                newLine[j] = ulong.Parse(items[j]);
            }

            result[i] = newLine; // Конвертация и назначение обычного числа
        }

        return result;
    }

    /// <summary>
    /// Метод выполняет чтение матрицы смежности как квадратной матрицы из файла в соответствии с указанным именем.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Матрица смежности в виде квадратной матрицы.</returns>
    public static async Task<long[,]> LoadLongMatrixAsync(string fileName)
    {
        var matrixLines = await ReadFileAsync(fileName); // Читаем строки файла
        var size = matrixLines.Length; // Определяем размерность квадратной матрицы
        var result = new long[size, size];
        for (var i = 0; i < size; i++)
        {
            var items = matrixLines[i].Split(' '); // Разбиваем отдельные строки на числа, при помощи пробела
            for (var j = 0; j < items.Length; j++)
            {
                // Если встречается аббревиатура "Inf", означающая бесконечность, назначить максимальное число
                if (items[j].Equals("Inf", StringComparison.OrdinalIgnoreCase))
                {
                    result[i, j] = long.MaxValue;
                    continue;
                }

                result[i, j] = long.Parse(items[j]); // Конвертация и назначение обычного числа
            }
        }

        return result;
    }

    /// <summary>
    /// Метод открывает соединение с файлом по имени и считывает данные построчно.
    /// </summary>
    /// <param name="fileName">Имя файла.</param>
    /// <returns>Коллекция строк в файле.</returns>
    private static async Task<string[]> ReadFileAsync(string fileName)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Data", fileName); // Формируем путь к месту хранения файла
        await using var fileStream = new FileStream(path, FileMode.Open);
        using var reader = new StreamReader(fileStream);

        var lines = new List<string>();
        while (await reader.ReadLineAsync() is { } line) // Построчное считывание всего содержимого
        {
            lines.Add(line);
        }

        return lines.ToArray();
    }
}
