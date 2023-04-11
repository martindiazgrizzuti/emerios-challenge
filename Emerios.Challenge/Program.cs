using Emerios.Challenge;
using System.Collections;
using System.Diagnostics;

if (args.Length == 1)
{
    try
    {
        /*string[] input =
        new string[]
        {
            "B, B, B, B, D, L, F, D, L, 2",
            "B, X, C, 7, D, J, K, D, 2, K",
            "H, Y, 7, 3, E, D, 3, 2, K, 3",
            "R, 7, O, Ñ, G, D, 2, K, D, 2",
            "X, N, S, Ñ, E, 0, K, E, 0, D",
            "A, 9, C, Ñ, D, E, F, D, E, F",
            "A, X, D, Ñ, D, J, E, D, J, E"
        };*/

        // Read file
        var input = Util.ReadFromFile(args[0]).Result;
        
        int columnCount = input.First().Length, rowCount = input.Length;
        List<string> characters = new List<string>();
        
        var watch = Stopwatch.StartNew();
        var res = Util.Process(input, rowCount, columnCount);
        watch.Stop();

        Console.WriteLine($"Finished process. Time elapsed: {watch.ElapsedMilliseconds} ms");
        Console.WriteLine("Result:");
        foreach (var value in res.Item1)
        {
            var text = String.Join(", ", ArrayList.Repeat(value, res.Item2).ToArray());
            Console.WriteLine($"{text}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Ocurrió un error: {e.Message}");
    }
}
else
{
    Console.WriteLine("Wrong args");
}


Console.ReadLine();