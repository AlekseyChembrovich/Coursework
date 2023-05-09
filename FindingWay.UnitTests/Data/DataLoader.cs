namespace FindingWay.UnitTests.Data;

public static class DataLoader
{
    public static async Task<ulong[][]> LoadUlongSerratedArrayAsync(string fileName)
    {
        var matrixLines = await ReadFileAsync(fileName);
        var size = matrixLines.Length;
        var result = new ulong[size][];
        for (var i = 0; i < size; i++)
        {
            var items = matrixLines[i].Split(' ');
            var newLine = new ulong[size];
            for (var j = 0; j < items.Length; j++)
            {
                if (items[j].Equals("Inf", StringComparison.OrdinalIgnoreCase))
                {
                    newLine[j] = ulong.MaxValue;
                    continue;
                }

                newLine[j] = ulong.Parse(items[j]);
            }
            
            result[i] = newLine;   
        }

        return result;
    }
    
    public static async Task<long[,]> LoadLongMatrixAsync(string fileName)
    {
        var matrixLines = await ReadFileAsync(fileName);
        var size = matrixLines.Length;
        var result = new long[size, size];
        for (var i = 0; i < size; i++)
        {
            var items = matrixLines[i].Split(' ');
            for (var j = 0; j < items.Length; j++)
            {
                if (items[j] ==  "Inf")
                {
                    result[i, j] = long.MaxValue;
                    continue;
                }
                
                result[i, j] = long.Parse(items[j]);
            }
        }

        return result;
    }

    private static async Task<string[]> ReadFileAsync(string fileName)
    {
        var path = Path.Combine(Environment.CurrentDirectory, "Data", fileName);
        await using var fileStream = new FileStream(path, FileMode.Open);
        using var reader = new StreamReader(fileStream);

        var lines = new List<string>();
        while (await reader.ReadLineAsync() is { } line)
        {
            lines.Add(line);
        }

        return lines.ToArray();
    }
}
