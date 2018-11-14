using System;
using System.IO;
using System.Linq;

public class Program
{

    static bool MyExists(string x)
    {
        Console.WriteLine($"{x}");
        return File.Exists(x);
    }
    public static void Main(string[] args)
    {
        var files = new[] { "file1.dll", "file2.dll" };
        var paths = Environment.GetEnvironmentVariable("PATH").Split(';');

        var existsList = from file in files
                         from path in paths
                         group MyExists($"{path}/{file}") by file into g
                         select new
                         {
                             Dll = g.Key,
                             Exists = g.Any(x => x)
                         };
        foreach (var entry in existsList)
        {
            Console.WriteLine($"dll {entry.Dll} {(entry.Exists ? "Exists" : "Does not exist")} on PATH");
        }

        var el2 = files.Select(file => new { file, exists = paths.Any(path => File.Exists(Path.Combine(path, file))) });

        Console.WriteLine();
    }
}

