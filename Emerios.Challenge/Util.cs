namespace Emerios.Challenge;

internal static class Util
{
    internal static async Task<string[][]> ReadFromFile(string path, string separator = ",")
    {
        if (!File.Exists(path))
            throw new Exception("File doesn't exist in file provided");
        var allLines = await File.ReadAllLinesAsync(path);
        
        if(allLines.Length == 0)
            throw new Exception("File is empty");
        
        var allItems = allLines.Select(x => x.Split(separator).Select(ii => ii.Trim()));
        var columnCount = allItems.First().Count();
        if (allItems.Skip(1).Any(ii => ii.Count() != columnCount))
            throw new Exception("Not all lines contain same column length");

        return allItems.Select(ii => ii.ToArray()).ToArray();
    }

    internal static (List<string>, int) Process(string[][] data, int rowCount, int columnCount)
    {
        List<string> values = new List<string>();
        int maxCount = 0;
        int[][] horizontalValues = CreateZeroMatrix(rowCount, columnCount);
        int[][] verticalValues = CreateZeroMatrix(rowCount, columnCount);
        int[][] diagonalLeftValues = CreateZeroMatrix(rowCount, columnCount);
        int[][] diagonalRightValues = CreateZeroMatrix(rowCount, columnCount);
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                // horizontal
                if (j > 0)
                {
                    if (data[i][j] == data[i][j - 1])
                        horizontalValues[i][j] = horizontalValues[i][j - 1] + 1;
                    maxCount = CheckAdd(horizontalValues, data, i, j, maxCount, values);
                }

                // vertical
                if (i > 0)
                {
                    if (data[i][j] == data[i - 1][j])
                        verticalValues[i][j] = verticalValues[i - 1][j] + 1;
                    maxCount = CheckAdd(verticalValues, data, i, j, maxCount, values);
                }

                // diagonal left
                if (i > 0 && j < columnCount - 1)
                {
                    if (data[i][j] == data[i - 1][j + 1])
                        diagonalLeftValues[i][j] = diagonalLeftValues[i - 1][j + 1] + 1;
                    maxCount = CheckAdd(diagonalLeftValues, data, i, j, maxCount, values);
                }

                // diagonal right
                if (i > 0 && j > 0)
                {
                    if (data[i][j] == data[i - 1][j - 1])
                        diagonalRightValues[i][j] = diagonalRightValues[i - 1][j - 1] + 1;
                    maxCount = CheckAdd(diagonalRightValues, data, i, j, maxCount, values);
                }
            }
        }
        return (values, maxCount + 1);
    }

    private static int CheckAdd(int[][] mat, string[][] data, int i, int j, int maxCount, List<string> values)
    {
        if (mat[i][j] >= maxCount)
        {
            if(mat[i][j] > maxCount)
                values.Clear();
            if(!values.Any(item => item == data[i][j]))
                values.Add(data[i][j]);
            return mat[i][j];
        }
        return maxCount;
    }

    private static int[][] CreateZeroMatrix(int rowCount, int columnCount)
    {
        int[][] matrix = new int[rowCount][];
        for (int i = 0; i < rowCount; i++)
            matrix[i] = new int[columnCount];
        return matrix;
    }

    #region First Approach
    /*
    internal static int? Max(params int[] numbers)
    {
        if (numbers != null && numbers.Any())
        {
            int max = numbers.First();
            foreach (var number in numbers.Skip(1))
            {
                max = number > max ? number : max;
            }
            return max;
        }
        return null;
    }

    internal static int CountRight(this string[][] input, int i, int j, int rowCount, int columnCount)
    {
        if (i >= 0 && i < rowCount && j >= 0 &&
            j < columnCount - 1 && input[i][j + 1] == input[i][j])
            return 1 + CountRight(input, i, j + 1, rowCount, columnCount);
        return 1;
    }

    internal static int CountDown(this string[][] input, int i, int j, int rowCount, int columnCount)
    {
        if (j >= 0 && j < columnCount && i >= 0 &&
            i < rowCount - 1 && input[i + 1][j] == input[i][j])
            return 1 + CountDown(input, i + 1, j, rowCount, columnCount);
        return 1;
    }

    internal static int CountDiagonalDownRight(this string[][] input, int i, int j, int rowCount, int columnCount)
    {
        if (j >= 0 && j < columnCount - 1 && i >= 0 && i < rowCount - 1 && input[i + 1][j + 1] == input[i][j])
            return 1 + CountDiagonalDownRight(input, i + 1, j + 1, rowCount, columnCount);
        return 1;
    }

    internal static int CountDiagonalDownLeft(this string[][] input, int i, int j, int rowCount, int columnCount)
    {
        if (j > 0 && j < columnCount && i >= 0 && i < rowCount - 1 && input[i + 1][j - 1] == input[i][j])
            return 1 + CountDiagonalDownLeft(input, i + 1, j - 1, rowCount, columnCount);
        return 1;
    }
    */
    #endregion

}
